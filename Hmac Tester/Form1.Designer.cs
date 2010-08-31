namespace HmacTester
{
    partial class HmacTester
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HmacTester));
            this.serviceUrlLabel = new System.Windows.Forms.Label();
            this.apiKeyLabel = new System.Windows.Forms.Label();
            this.testButton = new System.Windows.Forms.Button();
            this.debugGroupBox = new System.Windows.Forms.GroupBox();
            this.debugOutput = new System.Windows.Forms.RichTextBox();
            this.serviceUrlTextBox = new System.Windows.Forms.TextBox();
            this.apiKeyTextBox = new System.Windows.Forms.TextBox();
            this.apiUsernameTextBox = new System.Windows.Forms.TextBox();
            this.apiUsernameLabel = new System.Windows.Forms.Label();
            this.methodPicker = new System.Windows.Forms.ComboBox();
            this.methodPickerLabel = new System.Windows.Forms.Label();
            this.requestBodyLabel = new System.Windows.Forms.Label();
            this.requestBodyTextBox = new System.Windows.Forms.TextBox();
            this.workingBar = new System.Windows.Forms.ProgressBar();
            this.debugGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // serviceUrlLabel
            // 
            this.serviceUrlLabel.AutoSize = true;
            this.serviceUrlLabel.Location = new System.Drawing.Point(13, 13);
            this.serviceUrlLabel.Name = "serviceUrlLabel";
            this.serviceUrlLabel.Size = new System.Drawing.Size(68, 13);
            this.serviceUrlLabel.TabIndex = 1;
            this.serviceUrlLabel.Text = "Service URL";
            // 
            // apiKeyLabel
            // 
            this.apiKeyLabel.AutoSize = true;
            this.apiKeyLabel.Location = new System.Drawing.Point(13, 65);
            this.apiKeyLabel.Name = "apiKeyLabel";
            this.apiKeyLabel.Size = new System.Drawing.Size(45, 13);
            this.apiKeyLabel.TabIndex = 3;
            this.apiKeyLabel.Text = "API Key";
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(16, 249);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 5;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.debugGroupBox.Controls.Add(this.debugOutput);
            this.debugGroupBox.Location = new System.Drawing.Point(13, 278);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(679, 453);
            this.debugGroupBox.TabIndex = 5;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug";
            // 
            // debugOutput
            // 
            this.debugOutput.BackColor = System.Drawing.Color.Black;
            this.debugOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugOutput.ForeColor = System.Drawing.Color.White;
            this.debugOutput.Location = new System.Drawing.Point(3, 16);
            this.debugOutput.Name = "debugOutput";
            this.debugOutput.Size = new System.Drawing.Size(673, 434);
            this.debugOutput.TabIndex = 6;
            this.debugOutput.Text = "";
            // 
            // serviceUrlTextBox
            // 
            this.serviceUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serviceUrlTextBox.Location = new System.Drawing.Point(94, 10);
            this.serviceUrlTextBox.Name = "serviceUrlTextBox";
            this.serviceUrlTextBox.Size = new System.Drawing.Size(601, 20);
            this.serviceUrlTextBox.TabIndex = 0;
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.apiKeyTextBox.Location = new System.Drawing.Point(94, 62);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            this.apiKeyTextBox.Size = new System.Drawing.Size(601, 20);
            this.apiKeyTextBox.TabIndex = 2;
            // 
            // apiUsernameTextBox
            // 
            this.apiUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.apiUsernameTextBox.Location = new System.Drawing.Point(94, 36);
            this.apiUsernameTextBox.Name = "apiUsernameTextBox";
            this.apiUsernameTextBox.Size = new System.Drawing.Size(601, 20);
            this.apiUsernameTextBox.TabIndex = 1;
            // 
            // apiUsernameLabel
            // 
            this.apiUsernameLabel.AutoSize = true;
            this.apiUsernameLabel.Location = new System.Drawing.Point(13, 39);
            this.apiUsernameLabel.Name = "apiUsernameLabel";
            this.apiUsernameLabel.Size = new System.Drawing.Size(75, 13);
            this.apiUsernameLabel.TabIndex = 8;
            this.apiUsernameLabel.Text = "API Username";
            // 
            // methodPicker
            // 
            this.methodPicker.FormattingEnabled = true;
            this.methodPicker.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD"});
            this.methodPicker.Location = new System.Drawing.Point(94, 89);
            this.methodPicker.Name = "methodPicker";
            this.methodPicker.Size = new System.Drawing.Size(121, 21);
            this.methodPicker.TabIndex = 3;
            this.methodPicker.Text = "GET";
            this.methodPicker.SelectedIndexChanged += new System.EventHandler(this.methodPicker_selectedIndexChanged);
            // 
            // methodPickerLabel
            // 
            this.methodPickerLabel.AutoSize = true;
            this.methodPickerLabel.Location = new System.Drawing.Point(13, 92);
            this.methodPickerLabel.Name = "methodPickerLabel";
            this.methodPickerLabel.Size = new System.Drawing.Size(43, 13);
            this.methodPickerLabel.TabIndex = 10;
            this.methodPickerLabel.Text = "Method";
            // 
            // requestBodyLabel
            // 
            this.requestBodyLabel.AutoSize = true;
            this.requestBodyLabel.Location = new System.Drawing.Point(13, 120);
            this.requestBodyLabel.Name = "requestBodyLabel";
            this.requestBodyLabel.Size = new System.Drawing.Size(74, 13);
            this.requestBodyLabel.TabIndex = 12;
            this.requestBodyLabel.Text = "Request Body";
            // 
            // requestBodyTextBox
            // 
            this.requestBodyTextBox.Location = new System.Drawing.Point(94, 117);
            this.requestBodyTextBox.Multiline = true;
            this.requestBodyTextBox.Name = "requestBodyTextBox";
            this.requestBodyTextBox.ReadOnly = true;
            this.requestBodyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.requestBodyTextBox.Size = new System.Drawing.Size(598, 110);
            this.requestBodyTextBox.TabIndex = 4;
            // 
            // workingBar
            // 
            this.workingBar.Location = new System.Drawing.Point(98, 248);
            this.workingBar.Name = "workingBar";
            this.workingBar.Size = new System.Drawing.Size(591, 23);
            this.workingBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.workingBar.TabIndex = 13;
            this.workingBar.Visible = false;
            // 
            // HmacTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 743);
            this.Controls.Add(this.workingBar);
            this.Controls.Add(this.requestBodyTextBox);
            this.Controls.Add(this.requestBodyLabel);
            this.Controls.Add(this.methodPickerLabel);
            this.Controls.Add(this.methodPicker);
            this.Controls.Add(this.apiUsernameTextBox);
            this.Controls.Add(this.apiUsernameLabel);
            this.Controls.Add(this.apiKeyTextBox);
            this.Controls.Add(this.serviceUrlTextBox);
            this.Controls.Add(this.debugGroupBox);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.apiKeyLabel);
            this.Controls.Add(this.serviceUrlLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HmacTester";
            this.Text = "Hmac Tester";
            this.debugGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label serviceUrlLabel;
        private System.Windows.Forms.Label apiKeyLabel;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.GroupBox debugGroupBox;
        private System.Windows.Forms.TextBox serviceUrlTextBox;
        private System.Windows.Forms.TextBox apiKeyTextBox;
        private System.Windows.Forms.TextBox apiUsernameTextBox;
        private System.Windows.Forms.Label apiUsernameLabel;
        private System.Windows.Forms.ComboBox methodPicker;
        private System.Windows.Forms.Label methodPickerLabel;
        private System.Windows.Forms.Label requestBodyLabel;
        private System.Windows.Forms.TextBox requestBodyTextBox;
        private System.Windows.Forms.RichTextBox debugOutput;
        private System.Windows.Forms.ProgressBar workingBar;
    }
}

