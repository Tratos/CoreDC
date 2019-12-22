using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace CoreDC.Demo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            myDcClient.ClientInfo.Description = txtDescription.Text;
            myDcClient.ClientInfo.Email = txtEmail.Text;
            myDcClient.ClientInfo.Username = txtUsername.Text;
            myDcClient.ClientInfo.Password = txtPassword.Text;
            if (!String.IsNullOrEmpty(txtDescription.Text))
            {
                myDcClient.ClientInfo.Hostname = txtHubAddress.Text;
            }
            if (!String.IsNullOrEmpty(txtPort.Text))
            {
                myDcClient.ClientInfo.Port = Convert.ToInt32(txtPort.Text);
            }

            myDcClient.Connect();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            myDcClient.Disconnect();
        }

        private void cmbEventChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtxtDisplay1.Visible = false;
            rtxtDisplay2.Visible = false;
            rtxtDisplay3.Visible = false;
            rtxtDisplay4.Visible = false;
            rtxtDisplay5.Visible = false;
            rtxtDisplay6.Visible = false;
            lstDisplay1.Visible = false;

            switch ((string)cmbEventChooser.SelectedItem)
            {
                case "OnMainchatMessage":
                    rtxtDisplay1.Visible = true;
                    break;
                case "OnPrivateMessageRecieved":
                    rtxtDisplay2.Visible = true;
                    break;
                case "OnPrivateMessageSent":
                    rtxtDisplay3.Visible = true;
                    break;
                case "OnMessageReceived":
                    rtxtDisplay4.Visible = true;
                    break;
                case "OnMessageSent":
                    rtxtDisplay5.Visible = true;
                    break;
                case "OnSearch":
                    lstDisplay1.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void myDcClient_OnMainchatMessage(CoreDC.Classes.User user, string text, bool ownMessage)
        {
            rtxtDisplay1.AppendText("<" + user.Username + "> " + text);
            rtxtDisplay1.AppendText(Environment.NewLine);
        }

        private void myDcClient_OnPrivateMessageRecieved(CoreDC.Classes.User user, string text)
        {
            rtxtDisplay2.AppendText("<" + user.Username + "> " + text);
            rtxtDisplay2.AppendText(Environment.NewLine);
            myDcClient.SendPrivateMessage(user, "I am a CoreDC Demobot!!1 - Visit http://coredc.googlecode.com for more information.");
        }

        private void myDcClient_OnPrivateMessageSent(CoreDC.Classes.User user, string text)
        {
            rtxtDisplay3.AppendText("Sent to " + user.Username + ":"+ Environment.NewLine);
            rtxtDisplay3.AppendText("<" + myDcClient.ClientInfo.Username + "> " + text);
            rtxtDisplay3.AppendText(Environment.NewLine);
        }

        private void myDcClient_OnMessageReceived(string text)
        {
            rtxtDisplay4.AppendText("[" + DateTime.Now.ToLongTimeString() + "] " + text);
            rtxtDisplay4.AppendText(Environment.NewLine);
        }

        private void myDcClient_OnMessageSent(string text)
        {
            string[] messages = text.Split("|".ToCharArray());
            foreach (string message in messages)
            {
                if (message.Length > 1)
                {
                    rtxtDisplay5.AppendText("[" + DateTime.Now.ToLongTimeString() + "] " + message);
                    rtxtDisplay5.AppendText(Environment.NewLine);
                }
            }
        }

        private void myDcClient_OnSearch(CoreDC.Classes.SearchInfo searchInfo)
        {
            ListViewItem it = new ListViewItem();

            it.Text = searchInfo.User.Username;
            if (searchInfo.User.IP != null && searchInfo.User.IP.ToString() != "0.0.0.0")
            {
                it.SubItems.Add(searchInfo.User.IP.ToString());
            }
            else
            {
                it.SubItems.Add("");
            }
            it.SubItems.Add(searchInfo.DataType.ToString());
            it.SubItems.Add(searchInfo.SearchPattern);

            lstDisplay1.Items.Add(it);
        }

        private void btnSendMainchatMessage_Click(object sender, EventArgs e)
        {
            myDcClient.SendMainchatMessage(txtSendMainchatMessage.Text);
        }
    }
}