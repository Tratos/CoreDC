namespace CoreDC.Demo
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.grpHub = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblHubAddress = new System.Windows.Forms.Label();
            this.txtHubAddress = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.cmbEventChooser = new System.Windows.Forms.ComboBox();
            this.rtxtDisplay1 = new System.Windows.Forms.RichTextBox();
            this.rtxtDisplay2 = new System.Windows.Forms.RichTextBox();
            this.rtxtDisplay3 = new System.Windows.Forms.RichTextBox();
            this.rtxtDisplay4 = new System.Windows.Forms.RichTextBox();
            this.rtxtDisplay5 = new System.Windows.Forms.RichTextBox();
            this.rtxtDisplay6 = new System.Windows.Forms.RichTextBox();
            this.grpSendMainchatMessage = new System.Windows.Forms.GroupBox();
            this.btnSendMainchatMessage = new System.Windows.Forms.Button();
            this.txtSendMainchatMessage = new System.Windows.Forms.TextBox();
            this.lstDisplay1 = new System.Windows.Forms.ListView();
            this.Username = new System.Windows.Forms.ColumnHeader();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.Type = new System.Windows.Forms.ColumnHeader();
            this.Search = new System.Windows.Forms.ColumnHeader();
            this.myDcClient = new CoreDC.DcClient(this.components);
            this.grpClient.SuspendLayout();
            this.grpHub.SuspendLayout();
            this.grpSendMainchatMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 191);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(9, 33);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 17);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(112, 59);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(115, 75);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 59);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(9, 75);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // grpClient
            // 
            this.grpClient.Controls.Add(this.lblPassword);
            this.grpClient.Controls.Add(this.txtPassword);
            this.grpClient.Controls.Add(this.lblUsername);
            this.grpClient.Controls.Add(this.lblDescription);
            this.grpClient.Controls.Add(this.txtUsername);
            this.grpClient.Controls.Add(this.txtDescription);
            this.grpClient.Controls.Add(this.txtEmail);
            this.grpClient.Controls.Add(this.lblEmail);
            this.grpClient.Location = new System.Drawing.Point(12, 12);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(224, 104);
            this.grpClient.TabIndex = 10;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Client settings";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(112, 17);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 11;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(115, 33);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // grpHub
            // 
            this.grpHub.Controls.Add(this.txtPort);
            this.grpHub.Controls.Add(this.lblHubAddress);
            this.grpHub.Controls.Add(this.txtHubAddress);
            this.grpHub.Controls.Add(this.lblPort);
            this.grpHub.Location = new System.Drawing.Point(12, 122);
            this.grpHub.Name = "grpHub";
            this.grpHub.Size = new System.Drawing.Size(224, 63);
            this.grpHub.TabIndex = 11;
            this.grpHub.TabStop = false;
            this.grpHub.Text = "Hub settings";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(162, 33);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(56, 20);
            this.txtPort.TabIndex = 8;
            this.txtPort.Text = "411";
            // 
            // lblHubAddress
            // 
            this.lblHubAddress.AutoSize = true;
            this.lblHubAddress.Location = new System.Drawing.Point(6, 17);
            this.lblHubAddress.Name = "lblHubAddress";
            this.lblHubAddress.Size = new System.Drawing.Size(64, 13);
            this.lblHubAddress.TabIndex = 3;
            this.lblHubAddress.Text = "Hubaddress";
            // 
            // txtHubAddress
            // 
            this.txtHubAddress.Location = new System.Drawing.Point(9, 33);
            this.txtHubAddress.Name = "txtHubAddress";
            this.txtHubAddress.Size = new System.Drawing.Size(147, 20);
            this.txtHubAddress.TabIndex = 5;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(159, 17);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 7;
            this.lblPort.Text = "Port";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(161, 191);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 8;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // cmbEventChooser
            // 
            this.cmbEventChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEventChooser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventChooser.Items.AddRange(new object[] {
            "OnMainchatMessage",
            "OnMessageReceived",
            "OnMessageSent",
            "OnSearch",
            "OnPrivateMessageRecieved",
            "OnPrivateMessageSent"});
            this.cmbEventChooser.Location = new System.Drawing.Point(242, 19);
            this.cmbEventChooser.Name = "cmbEventChooser";
            this.cmbEventChooser.Size = new System.Drawing.Size(586, 21);
            this.cmbEventChooser.TabIndex = 11;
            this.cmbEventChooser.SelectedIndexChanged += new System.EventHandler(this.cmbEventChooser_SelectedIndexChanged);
            // 
            // rtxtDisplay1
            // 
            this.rtxtDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDisplay1.Location = new System.Drawing.Point(242, 44);
            this.rtxtDisplay1.Name = "rtxtDisplay1";
            this.rtxtDisplay1.Size = new System.Drawing.Size(586, 239);
            this.rtxtDisplay1.TabIndex = 16;
            this.rtxtDisplay1.Text = "";
            // 
            // rtxtDisplay2
            // 
            this.rtxtDisplay2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDisplay2.Location = new System.Drawing.Point(242, 44);
            this.rtxtDisplay2.Name = "rtxtDisplay2";
            this.rtxtDisplay2.Size = new System.Drawing.Size(586, 239);
            this.rtxtDisplay2.TabIndex = 17;
            this.rtxtDisplay2.Text = "";
            this.rtxtDisplay2.Visible = false;
            // 
            // rtxtDisplay3
            // 
            this.rtxtDisplay3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDisplay3.Location = new System.Drawing.Point(242, 44);
            this.rtxtDisplay3.Name = "rtxtDisplay3";
            this.rtxtDisplay3.Size = new System.Drawing.Size(586, 239);
            this.rtxtDisplay3.TabIndex = 18;
            this.rtxtDisplay3.Text = "";
            this.rtxtDisplay3.Visible = false;
            // 
            // rtxtDisplay4
            // 
            this.rtxtDisplay4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDisplay4.Location = new System.Drawing.Point(242, 44);
            this.rtxtDisplay4.Name = "rtxtDisplay4";
            this.rtxtDisplay4.Size = new System.Drawing.Size(586, 239);
            this.rtxtDisplay4.TabIndex = 19;
            this.rtxtDisplay4.Text = "";
            this.rtxtDisplay4.Visible = false;
            // 
            // rtxtDisplay5
            // 
            this.rtxtDisplay5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDisplay5.Location = new System.Drawing.Point(242, 44);
            this.rtxtDisplay5.Name = "rtxtDisplay5";
            this.rtxtDisplay5.Size = new System.Drawing.Size(586, 239);
            this.rtxtDisplay5.TabIndex = 20;
            this.rtxtDisplay5.Text = "";
            this.rtxtDisplay5.Visible = false;
            // 
            // rtxtDisplay6
            // 
            this.rtxtDisplay6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDisplay6.Location = new System.Drawing.Point(242, 44);
            this.rtxtDisplay6.Name = "rtxtDisplay6";
            this.rtxtDisplay6.Size = new System.Drawing.Size(586, 239);
            this.rtxtDisplay6.TabIndex = 21;
            this.rtxtDisplay6.Text = "";
            this.rtxtDisplay6.Visible = false;
            // 
            // grpSendMainchatMessage
            // 
            this.grpSendMainchatMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSendMainchatMessage.Controls.Add(this.btnSendMainchatMessage);
            this.grpSendMainchatMessage.Controls.Add(this.txtSendMainchatMessage);
            this.grpSendMainchatMessage.Location = new System.Drawing.Point(12, 237);
            this.grpSendMainchatMessage.Name = "grpSendMainchatMessage";
            this.grpSendMainchatMessage.Size = new System.Drawing.Size(224, 46);
            this.grpSendMainchatMessage.TabIndex = 12;
            this.grpSendMainchatMessage.TabStop = false;
            this.grpSendMainchatMessage.Text = "Send mainchat message";
            // 
            // btnSendMainchatMessage
            // 
            this.btnSendMainchatMessage.Location = new System.Drawing.Point(178, 19);
            this.btnSendMainchatMessage.Name = "btnSendMainchatMessage";
            this.btnSendMainchatMessage.Size = new System.Drawing.Size(40, 23);
            this.btnSendMainchatMessage.TabIndex = 10;
            this.btnSendMainchatMessage.Text = "Send";
            this.btnSendMainchatMessage.UseVisualStyleBackColor = true;
            this.btnSendMainchatMessage.Click += new System.EventHandler(this.btnSendMainchatMessage_Click);
            // 
            // txtSendMainchatMessage
            // 
            this.txtSendMainchatMessage.Location = new System.Drawing.Point(6, 20);
            this.txtSendMainchatMessage.Name = "txtSendMainchatMessage";
            this.txtSendMainchatMessage.Size = new System.Drawing.Size(166, 20);
            this.txtSendMainchatMessage.TabIndex = 9;
            // 
            // lstDisplay1
            // 
            this.lstDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDisplay1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Username,
            this.IP,
            this.Type,
            this.Search});
            this.lstDisplay1.FullRowSelect = true;
            this.lstDisplay1.GridLines = true;
            this.lstDisplay1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstDisplay1.Location = new System.Drawing.Point(242, 44);
            this.lstDisplay1.Name = "lstDisplay1";
            this.lstDisplay1.Size = new System.Drawing.Size(586, 239);
            this.lstDisplay1.TabIndex = 12;
            this.lstDisplay1.UseCompatibleStateImageBehavior = false;
            this.lstDisplay1.View = System.Windows.Forms.View.Details;
            this.lstDisplay1.Visible = false;
            // 
            // Username
            // 
            this.Username.Text = "Username";
            this.Username.Width = 120;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 90;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 70;
            // 
            // Search
            // 
            this.Search.Text = "Search";
            this.Search.Width = 284;
            // 
            // myDcClient
            // 
            this.myDcClient.ClientInfo.Description = "";
            this.myDcClient.ClientInfo.Email = "";
            this.myDcClient.ClientInfo.Hostname = "localhost";
            this.myDcClient.ClientInfo.Password = "";
            this.myDcClient.ClientInfo.Port = 411;
            this.myDcClient.ClientInfo.Username = "-[CoreDc]-";
            this.myDcClient.OnPrivateMessageRecieved += new CoreDC.DcPrivateMessageHandler(this.myDcClient_OnPrivateMessageRecieved);
            this.myDcClient.OnMessageSent += new CoreDC.DcMessageHandler(this.myDcClient_OnMessageSent);
            this.myDcClient.OnMessageReceived += new CoreDC.DcMessageHandler(this.myDcClient_OnMessageReceived);
            this.myDcClient.OnPrivateMessageSent += new CoreDC.DcPrivateMessageHandler(this.myDcClient_OnPrivateMessageSent);
            this.myDcClient.OnMainchatMessage += new CoreDC.DcUserMessageHandler(this.myDcClient_OnMainchatMessage);
            this.myDcClient.OnSearch += new CoreDC.DcSearchHandler(this.myDcClient_OnSearch);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 292);
            this.Controls.Add(this.lstDisplay1);
            this.Controls.Add(this.grpSendMainchatMessage);
            this.Controls.Add(this.rtxtDisplay6);
            this.Controls.Add(this.rtxtDisplay5);
            this.Controls.Add(this.rtxtDisplay4);
            this.Controls.Add(this.rtxtDisplay3);
            this.Controls.Add(this.rtxtDisplay2);
            this.Controls.Add(this.rtxtDisplay1);
            this.Controls.Add(this.cmbEventChooser);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.grpHub);
            this.Controls.Add(this.grpClient);
            this.Controls.Add(this.btnConnect);
            this.Name = "Main";
            this.Text = "CoreDC Demo";
            this.grpClient.ResumeLayout(false);
            this.grpClient.PerformLayout();
            this.grpHub.ResumeLayout(false);
            this.grpHub.PerformLayout();
            this.grpSendMainchatMessage.ResumeLayout(false);
            this.grpSendMainchatMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DcClient myDcClient;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.GroupBox grpHub;
        private System.Windows.Forms.Label lblHubAddress;
        private System.Windows.Forms.TextBox txtHubAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ComboBox cmbEventChooser;
        private System.Windows.Forms.RichTextBox rtxtDisplay1;
        private System.Windows.Forms.RichTextBox rtxtDisplay2;
        private System.Windows.Forms.RichTextBox rtxtDisplay3;
        private System.Windows.Forms.RichTextBox rtxtDisplay4;
        private System.Windows.Forms.RichTextBox rtxtDisplay5;
        private System.Windows.Forms.RichTextBox rtxtDisplay6;
        private System.Windows.Forms.GroupBox grpSendMainchatMessage;
        private System.Windows.Forms.Button btnSendMainchatMessage;
        private System.Windows.Forms.TextBox txtSendMainchatMessage;
        private System.Windows.Forms.ListView lstDisplay1;
        private System.Windows.Forms.ColumnHeader Username;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Search;
        private System.Windows.Forms.TextBox txtPort;
    }
}

