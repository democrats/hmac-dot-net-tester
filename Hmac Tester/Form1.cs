using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace HmacTester
{
    public partial class HmacTester : Form
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        const int BUFFER_SIZE = 1024;

        public DebugOutputter debugOut;

        public HmacTester()
        {
            InitializeComponent();
            debugOut = new DebugOutputter();
            debugOut.OutputTextBox = debugOutput;
        }

        private string computeMD5Hash(string strToHash)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = md5.ComputeHash(encoding.GetBytes(strToHash));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        private string getContentType(string requestMethod)
        {
            // hard-coding the content type for now
            string contentType;
            switch (requestMethod)
            {
                case "POST":
                case "PUT":
                    contentType = "application/x-www-form-urlencoded";
                    break;
                default:
                    contentType = "";
                    break;
            }
            return contentType;
        }

        private string computeHmacSignature(string apiKey, string url, string method, string requestBody, string httpDate)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(apiKey);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyBytes);
            
            Uri serviceUri = new Uri(url);
            string requestPath = serviceUri.AbsolutePath;

            string contentMD5Hash = "";
            if (requestBody.Length > 0)
            {
                contentMD5Hash = computeMD5Hash(requestBody);
            }

            string contentType = getContentType(method);
            
            string canonicalString = method + "\n" + contentType + "\n" + contentMD5Hash + "\n" + httpDate + "\n"
                + requestPath;
            string debugCanonicalString = canonicalString.Replace("\n", "\\n");
            debugOut.WriteLine("HMAC Canonical String: " + debugCanonicalString);

            string hmacSig = Convert.ToBase64String(hmacsha1.ComputeHash(encoding.GetBytes(canonicalString)));
            return hmacSig;
        }

        private void doHmacRequest(string method, string url, string apiUsername, string apiKey, string requestBody)
        {
            Uri serviceUri = new Uri(url);
            HttpWebRequest hmacRequest = (HttpWebRequest)WebRequest.Create(serviceUri);
            RequestState rs = new RequestState();
            rs.Request = hmacRequest;
            rs.Requestor = this;
            hmacRequest.Method = method;
            string contentType = getContentType(method);
            if (contentType.Length > 0)
            {
                rs.RequestBody = requestBody;
                hmacRequest.ContentType = contentType;
                hmacRequest.BeginGetRequestStream(new AsyncCallback(RequestStreamCallback), rs);
                
                //hmacRequest.ContentLength = bytes.Length;
            }

            DateTime currentDate = DateTime.Now;
            string httpDate = currentDate.ToUniversalTime().ToString("r");
            string hmacSignature = computeHmacSignature(apiKey, url, method, requestBody, httpDate);
            debugOut.WriteLine("HMAC Digest: " + hmacSignature);
            hmacRequest.Headers.Set(HttpRequestHeader.Authorization, "AuthHMAC " + apiUsername + ":" + hmacSignature);
            hmacRequest.Headers.Add("X-AuthHMAC-Request-Date", httpDate);

            foreach (string header in hmacRequest.Headers.Keys)
            {
                debugOut.WriteLine(header + ": " + hmacRequest.Headers[header]);
            }

            allDone.WaitOne();
        }

        private static void RequestStreamCallback(IAsyncResult ar)
        {
            RequestState rs = (RequestState)ar.AsyncState;
            HttpWebRequest hmacRequest = (HttpWebRequest)rs.Request;
            Stream requestStream = hmacRequest.EndGetRequestStream(ar);
            string requestBody = rs.RequestBody;
            byte[] bytes = Encoding.UTF8.GetBytes(requestBody);
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            hmacRequest.BeginGetResponse(new AsyncCallback(RespCallback), rs);
        }

        private static void RespCallback(IAsyncResult ar)
        {
            RequestState rs = (RequestState)ar.AsyncState;
            HttpWebRequest hmacRequest = (HttpWebRequest)rs.Request;
            try
            {
                HttpWebResponse hmacResponse = (HttpWebResponse)hmacRequest.EndGetResponse(ar);
                Stream ResponseStream = hmacResponse.GetResponseStream();
                rs.ResponseStream = ResponseStream;
                IAsyncResult iarRead = ResponseStream.BeginRead(rs.BufferRead, 0,
                    BUFFER_SIZE, new AsyncCallback(ReadCallback), rs);
            }
            catch (Exception ex)
            {
                rs.Requestor.debugOut.WriteErrorLine(ex.Message);
                rs.Requestor.reset();
            }
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            RequestState rs = (RequestState)ar.AsyncState;
            Stream responseStream = rs.ResponseStream;
            int read = responseStream.EndRead(ar);
            if (read > 0)
            {
                Char[] charBuffer = new Char[BUFFER_SIZE];
                int len = rs.StreamDecode.GetChars(rs.BufferRead, 0, read, charBuffer, 0);
                String str = new String(charBuffer, 0, len);
                rs.RequestData.Append(
                    Encoding.ASCII.GetString(rs.BufferRead, 0, read));
                IAsyncResult recursiveAsyncResult = responseStream.BeginRead(
                    rs.BufferRead, 0, BUFFER_SIZE,
                    new AsyncCallback(ReadCallback), rs);
            }
            else
            {
                if (rs.RequestData.Length > 0)
                {
                    string strContent;
                    strContent = rs.RequestData.ToString();
                }
                responseStream.Close();
                allDone.Set();
            }
            return;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            if (testButton.Text == "Test")
            {
                testButton.Text = "Cancel";
                workingBar.Visible = true;
                debugOut.OutputTextBox.ResetText();

                // grab the values from the text boxes
                string serviceUrl = serviceUrlTextBox.Text;
                string apiUsername = apiUsernameTextBox.Text;
                string apiKey = apiKeyTextBox.Text;
                string httpMethod = methodPicker.Text;
                string requestBody = requestBodyTextBox.Text;

                debugOut.WriteLine("Request");
                debugOut.WriteDivider();
                debugOut.WriteLine("Sending " + httpMethod + " request to " + serviceUrl);
                if (requestBody.Length > 0)
                {
                    debugOut.WriteLine("With request body:");
                    debugOut.WriteLine(requestBody);
                    debugOut.WriteLine();
                }
                debugOut.WriteLine("Authenticating with API Username \"" + apiUsername + "\" & Key \"" + apiKey + "\"");
                debugOut.WriteDivider();

                try
                {
                    doHmacRequest(httpMethod, serviceUrl, apiUsername, apiKey, requestBody);
                }
                catch (Exception ex)
                {
                    debugOut.WriteErrorLine(ex.Message);
                    reset();
                }
            }
            else if (testButton.Text == "Cancel")
            {
                reset();
            }
        }

        private void methodPicker_selectedIndexChanged(object sender, EventArgs e)
        {
            switch (methodPicker.Text)
            {
                case "POST":
                case "PUT":
                    requestBodyTextBox.ReadOnly = false;
                    break;
                default:
                    requestBodyTextBox.ReadOnly = true;
                    break;
            }
        }

        protected void reset()
        {
            allDone.Set();

            if (testButton.InvokeRequired)
            {
                testButton.BeginInvoke(new MethodInvoker(delegate() { testButton.Text = "Test"; }));
            }
            else
            {
                testButton.Text = "Test";
            }

            if (workingBar.InvokeRequired)
            {
                workingBar.BeginInvoke(new MethodInvoker(delegate() { workingBar.Visible = false; }));
            }
            else
            {
                workingBar.Visible = false;
            }
        }
    }

    public class DebugOutputter
    {
        RichTextBox outBox;
        string nl = Environment.NewLine;

        public DebugOutputter()
        {

        }

        public RichTextBox OutputTextBox
        {
            get
            {
                return outBox;
            }
            set
            {
                outBox = value;
                outBox.ResetText();
            }
        }

        public void WriteLine(String text)
        {
            WriteLine(text, Color.White);
        }

        public void WriteLine(String text, Color textColor)
        {
            if (outBox.InvokeRequired)
            {
                outBox.BeginInvoke(
                    new MethodInvoker(
                        delegate() { WriteLine(text, textColor); }
                    )
                );
            }
            else
            {
                outBox.SelectionColor = textColor;
                outBox.AppendText(text + nl);
            }
        }

        public void WriteErrorLine(String text)
        {
            WriteLine();
            WriteLine(text, Color.Red);
        }

        public void WriteSuccessLine(String text)
        {
            WriteLine();
            WriteLine(text, Color.Green);
        }

        public void WriteWarningLine(String text)
        {
            WriteLine();
            WriteLine(text, Color.Yellow);
        }

        public void WriteLine()
        {
            WriteLine("");
        }

        public void WriteDivider()
        {
            WriteLine("--------------------------------------------------------------------------------");
            WriteLine();
        }
    }

    public class RequestState
    {
        const int BufferSize = 1024;
        public StringBuilder RequestData;
        public byte[] BufferRead;
        public WebRequest Request;
        public Stream ResponseStream;
        public Decoder StreamDecode = Encoding.UTF8.GetDecoder();
        public dynamic Requestor;
        public String RequestBody;

        public RequestState()
        {
            BufferRead = new byte[BufferSize];
            RequestData = new StringBuilder(String.Empty);
            Request = null;
            ResponseStream = null;
            Requestor = null;
            RequestBody = null;
        }
    }
}
