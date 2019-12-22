#region License

/* Copyright 2007 © Johan Pettersson, Fredrik Johansson, Michael Starberg, Christian Epstein.
 
This file is part of CoreDC.

CoreDC is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 2 of the License, or
(at your option) any later version.

CoreDC is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with CoreDC.  If not, see <http://www.gnu.org/licenses/>. */

#endregion
using System;
using CoreDC.Classes;
using CoreDC.Implementation;

namespace CoreDC
{
    /// <summary>
    /// The DcClient component manages a connection to a hub.
    /// DcClient is fully WinForm and CLS Compliant.
    /// Use this component to:
    /// - Connect and disconnect from a hub
    /// - Read messages via subscribing to events
    /// - Send mainchat and private messages
    /// - Search
    /// </summary>
    public partial class DcClient
    {
        DcConnection connection;
        ProtocolHandler protocolHandler;
        
        internal bool IsConnected;
        

        string username;
        internal string Username
        {
            get { return username; }
        }

        string password;
        internal string Password
        {
            get { return password; }
            set { password = value; }
        }

        private void InitializeInstance()
        {
            protocolHandler = new ProtocolHandler(this);
        }
        

        /// <summary>
        /// Connect to a hub using values from the ClientInfo properties.
        /// </summary>
        public void Connect()
        {
            try
            {
                // Try connect to the hub, set up a listener for incomming messages
                // and finally set the username and password that will be used during this connection.
                IsConnected = false;
                DoConnecting();
                connection = new DcConnection(this);
                connection.OnMessage += connection_OnMessage;
                connection.Open(clientInfo.Hostname, clientInfo.Port);
                username = clientInfo.Username;
                password = clientInfo.Password;
            }
            catch (Exception ex)
            {
                Exception res = TryReport(this, ex);
                if (res != null) throw; 
            }
        }

        void connection_OnMessage(string message)
        {
            // Recieves messages from a connection and forwards them to the protocolmanager
            // The protocol managager will later do a call back and trigger the proper event.
            protocolHandler.ProcessCommand(message);            
        }
        
        /// <summary>
        /// Disconnect from a hub.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                connection.Dispose();
                IsConnected = false;
                DoDisconnected();
            }
            catch (Exception ex)
            {
                Exception res = TryReport(this, ex);
                if (res != null) throw;
            }
        }

        /// <summary>
        /// Sends a raw message to the hub. 
        /// If the message is missing a trailing pipe, it will be added to the rawMessage.
        /// </summary>
        /// <param name="message">The raw message to send.</param>
        public void SendRawMessage(string message)
        {
            try
            {
                if (!message.EndsWith("|"))
                {
                    message += "|";
                }

                DoMessageSent(message);
                connection.Write(message);
            }
            catch (Exception ex)
            {
                Exception res = TryReport(this, ex);
                if (res != null) throw;
            }
        }
        
        /// <summary>
        /// Send a message to mainchat.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public void SendMainchatMessage(string message)
        {
            message = DcEncode(String.Format("<{0}> {1}", Username, message));
            SendRawMessage(message);
        }
        
        /// <summary>
        /// Send a private message to a user.
        /// </summary>
        /// <param name="recipient">The username of the recipient of the private message.</param>
        /// <param name="message">The message to send.</param>
        public void SendPrivateMessage(string recipient, string message)
        {
            User user = new User();
            user.Username = recipient;
            SendPrivateMessage(user, message);

        }
        /// <summary>
        /// Send a private message to a user.
        /// </summary>
        /// <param name="recipient">The username of the recipient of the private message.</param>
        /// <param name="message">The message to send.</param>
        public void SendPrivateMessage(User recipient, string message)
        {
            string sendMessage = String.Format("$To: {0} From: {1} $<{1}> {2}|", recipient.Username, Username, DcEncode(message));
            SendRawMessage(sendMessage);
            DoPrivateMessageSent(recipient, message);
        }
        
        /// <summary>
        ///  Send a private message to a user via mainchat. The hub must support the $MCTo protocol (only later YnHub versions).
        /// </summary>
        /// <param name="recipient">The username for the repicient of the private mainchat message.</param>
        /// <param name="message">The message to send.</param>
        public void SendPrivateMainchatMessage(string recipient, string message)
        {
            message = String.Format("$MCTo: {0} $<{1}> {2}|", recipient, Username, DcEncode(message));
            SendRawMessage(message);
        }
        /// <summary>
        ///  Send a private message to a user via mainchat. The hub must support the $MCTo protocol (only later YnHub versions).
        /// </summary>
        /// <param name="recipient">The user object for the recipient of the private mainchat message.</param>
        /// <param name="message">The message to send.</param>
        public void SendPrivateMainchatMessage(User recipient, string message)
        {
            SendPrivateMessage(recipient.Username, message);
        }
        
        /// <summary>
        /// As DcClient only supports passive mode connections this method performs 
        /// a passive search. Search replies are returned via the OnSearchReply event.
        /// </summary>
        /// <param name="searchInfo">The search to send to the hub.</param>
        public void SendSearch(SearchInfo searchInfo)
        {
            string message = String.Format("$Search Hub:{0} {1}", Username, DcEncode(searchInfo.ToString()));
            SendRawMessage(message);
        }

        /// <summary>
        /// Send a passive search reply to a specific recipient.
        /// </summary>
        /// <param name="recipient">The username for the recipient of the searchreply.</param>
        /// <param name="searchReply">The searchreply to send.</param>
        public void SendSearchReply(string recipient, SearchReply searchReply)
        {
            string message = String.Format("$SR {0} {1}{2}", Username, DcEncode(searchReply.ToString()), recipient);
            SendRawMessage(message);
        }
        /// <summary>
        /// Send a passive search reply to a specific recipient.
        /// </summary>
        /// <param name="recipient">The user object for the recipient of the searchreply.</param>
        /// <param name="searchReply">The searchreply to send.</param>
        public void SendSearchReply(User recipient, SearchReply searchReply)
        {
            SendSearchReply(recipient.Username, searchReply);
        }

        /// <summary>
        /// Send some basic BotInfo information about ourselves, hub should send some HubInfo back.
        /// </summary>
        public void SendBotInfo()
        {
            string description = clientInfo.Description;

            string message = String.Format("$BotINFO {0} {1}|", Username, description);
            SendRawMessage(message);
        }


        internal Exception TryReport(DcClient client, Exception ex)
        {
            if (client.DoError(ex))
            {
                return null;
            }

            return ex;
        }
        
        internal void Trace(string message)
        {
            
        }

        static string DcEncode(string text)
        {
            text = text.Replace("|", "%#124;");
            text = text.Replace("$", "%#36;");

            return text;
        }

        static string DcDecode(string text)
        {
            text = text.Replace("%#124;", "|");
            text = text.Replace("%#36;", "$");

            return text;
        }
            
    }
}
