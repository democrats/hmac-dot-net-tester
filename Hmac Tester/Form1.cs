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

namespace HmacTester
{
    public partial class HmacTester : Form
    {
        public HmacTester()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            string serviceUrl = serviceUrlTextBox.Text;
            string apiKey = apiKeyTextBox.Text;
            string nl = Environment.NewLine;

            debugOutput.ResetText();
            debugOutput.AppendText("Sending GET request to " + serviceUrl + nl);
            debugOutput.AppendText("Authenticating with API Key " + apiKey + nl);
            debugOutput.AppendText(nl + nl);

            WebClient serviceRequest = new WebClient();
            serviceRequest.DownloadStringCompleted += (downloadSender, args) =>
            {
                if (args.Cancelled)
                {
                    debugOutput.AppendText("Request cancelled. This shouldn't happen?" + nl);
                }
                else if (args.Error != null)
                {
                    debugOutput.AppendText(args.Error.Message + nl);
                }
                else
                {
                    debugOutput.AppendText(nl + nl + "Response" + nl);
                    debugOutput.AppendText("----------------------------------" + nl);

                    foreach (string header in serviceRequest.ResponseHeaders.Keys)
                    {
                        debugOutput.AppendText(header + ": " + serviceRequest.ResponseHeaders[header] + nl);
                    }
                    debugOutput.AppendText(nl + nl);
                    debugOutput.AppendText(args.Result);
                }
            };

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(apiKey);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyBytes);
            DateTime currentDate = DateTime.Now;
            string httpDate = currentDate.ToUniversalTime().ToString("r");
            Uri serviceUri = new Uri(serviceUrl);
            string requestPath = serviceUri.AbsolutePath;
            string canonicalString = "GET\n\n\n" + httpDate + "\n" + requestPath;
            string debugCanonicalString = canonicalString.Replace("\n", "\\n");
            string hmacString = Convert.ToBase64String(hmacsha1.ComputeHash(encoding.GetBytes(canonicalString)));

            debugOutput.AppendText("Request" + nl);
            debugOutput.AppendText("----------------------------------" + nl);
            debugOutput.AppendText("HMAC Canonical String: " + debugCanonicalString + nl);
            debugOutput.AppendText("HMAC Digest: " + hmacString + nl);

            serviceRequest.Headers.Set(HttpRequestHeader.Authorization, "AuthHMAC " + apiUsernameTextBox.Text + ":" + hmacString);
            serviceRequest.Headers.Add("X-AuthHMAC-Request-Date", httpDate);
            foreach (string header in serviceRequest.Headers.Keys)
            {
                debugOutput.AppendText(header + ": " + serviceRequest.Headers[header] + nl);
            }

            // Useful for testing date-related header issues where it's a race condition
            // System.Threading.Thread.Sleep(2000);
            serviceRequest.DownloadStringAsync(serviceUri);
        }
    }
}
