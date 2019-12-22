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
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CoreDC
{
    using CoreDC.Classes;

    #region Delegates

    public delegate void DcMessageHandler(string text);
    public delegate void DcEventHandler();
    public delegate void DcUserMessageHandler(User user, string text, bool ownMessage);
    public delegate void DcPrivateMessageHandler(User user, string text);
    public delegate void DcUserHandler(User user);
    public delegate void DcRedirectHandler(string hubAddress);
    public delegate void DcSearchHandler(SearchInfo searchInfo);
    public delegate void DcSearchReplyHandler(SearchReply searchReply);
    public delegate void DcClientInfoHandler(ClientInfo redirectInfo);
    public delegate void DcUserCommandHandler(UserCommand userCommand);
    public delegate void DcErrorHandler(Exception ex);

    #endregion

    public partial class DcClient
    {
        #region Connection

        /// <summary>
        /// Occurs immediatly after calling Connect().
        /// </summary>
        [Description("Occurs immediatly after calling Connect().")]
        [Category("Connection")]
        public event DcEventHandler OnConnecting;
        internal void DoConnecting()
        {
            if (OnConnecting != null)
            {
                OnConnecting();
            }
        }

        /// <summary>
        /// Occurs when accepted into a hub.
        /// </summary>
        [Description("Occurs when accepted into a hub.")]
        [Category("Connection")]
        public event DcEventHandler OnConnected;
        internal void DoConnected()
        {
            if (OnConnected != null)
            {
                OnConnected();
            }
        }

        /// <summary>
        /// Occurs when disconnected from a hub.
        /// </summary>
        [Description("Occurs when disconnected from a hub.")]
        [Category("Connection")]
        public event DcEventHandler OnDisconnected;
        internal void DoDisconnected()
        {
            if (OnDisconnected != null)
            {
                OnDisconnected();
            }
        }

        //[Description("")]
        //[Category("Connection")]
        //public event DcEventHandler OnConnectionRejected;

        /// <summary>
        /// Occurs when current username is registered and password is incorrect.
        /// </summary>
        [Description("Occurs when current username is registered and password is incorrect.")]
        [Category("Connection")]
        public event DcEventHandler OnPasswordRejected;
        internal void DoPasswordRejected()
        {
            if (OnPasswordRejected != null)
            {
                OnPasswordRejected();
            }
        }

        /// <summary>
        /// Occurs when a UserCommand is recieved from the hub.
        /// </summary>
        [Description("Occurs when a UserCommand is recieved from the hub.")]
        [Category("Connection")]
        public event DcUserCommandHandler OnUserCommand;
        internal void DoUserCommand(UserCommand userCommand)
        {
            if (OnUserCommand != null)
            {
                OnUserCommand(userCommand);
            }
        }

        #endregion

        #region Raw
        
        /// <summary>
        /// Occurs for every raw message received from a hub.
        /// </summary>
        [Description("Occurs for every raw message received from a hub.")]
        [Category("Raw")]
        public event DcMessageHandler OnMessageReceived;
        internal void DoMessageReceived(string message)
        {
            if (OnMessageReceived != null)
            {
                OnMessageReceived(message + "|");
            }
        }

        /// <summary>
        /// Occurs for every raw message sent to a hub.
        /// </summary>
        [Description("Occurs for every raw message sent to a hub.")]
        [Category("Raw")]
        public event DcMessageHandler OnMessageSent;
        internal bool DoMessageSent(string message)
        {
            if (OnMessageSent != null)
            {
                OnMessageSent(message);
                return true;
            }
            return false;
        }

        // TODO: Tracing what? Write description and actual functionality.
        [Description("")]
        [Category("Raw")]
        public event DcMessageHandler OnTrace;
        internal bool DoTrace(string text)
        {
            if (OnTrace != null)
            {
                OnTrace(text);
                return true;
            }
            return false;
        }

        #endregion

        #region Hub

        /// <summary>
        /// Occurs when a new user enters the hub.
        /// </summary>
        [Description("Occurs when a new user enters the hub.")]
        [Category("User")]
        public event DcUserHandler OnJoin;
        internal void DoJoin(User user)
        {
            if (OnJoin != null)
            {
                OnJoin(user);
            }
        }

        /// <summary>
        /// Occurs when an existing user updates his client data.
        /// </summary>
        [Description("Occurs when an existing user updates his client data.")]
        [Category("User")]
        public event DcUserHandler OnUserChanged;
        internal void DoUserChanged(User user)
        {
            if (OnUserChanged != null)
            {
                OnUserChanged(user);
            }
        }

        /// <summary>
        /// Occurs when a user exits the hub.
        /// </summary>
        [Description("Occurs when a user exits the hub.")]
        [Category("User")]
        public event DcUserHandler OnParts;
        internal void DoParts(User user)
        {
            if (OnParts != null)
            {
                OnParts(user);
            }
        }

        /// <summary>
        /// Occurs for mainchat messages recieved from the hub.
        /// </summary>
        [Description("Occurs for mainchat messages recieved from the hub.")]
        [Category("Hub")]
        public event DcUserMessageHandler OnMainchatMessage;
        internal bool DoMainchatMessage(User user, string text, bool ownMessage)
        {
            if (OnMainchatMessage != null)
            {
                OnMainchatMessage(user, text, ownMessage);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs for private messages recieved from other users.
        /// </summary>
        [Description("Occurs for private messages recieved from other users.")]
        [Category("Hub")]
        public event DcPrivateMessageHandler OnPrivateMessageRecieved;
        internal bool DoPrivateMessageRecieved(User user, string text)
        {
            if (OnPrivateMessageRecieved != null)
            {
                OnPrivateMessageRecieved(user, text);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs for private messages sent to other users.
        /// </summary>
        [Description("Occurs for private messages sent to other users.")]
        [Category("Hub")]
        public event DcPrivateMessageHandler OnPrivateMessageSent;
        internal bool DoPrivateMessageSent(User user, string text)
        {
            if (OnPrivateMessageSent != null)
            {
                OnPrivateMessageSent(user, text);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when the topic in the hub changes.
        /// </summary>
        [Description("Occurs when the topic in the hub changes.")]
        [Category("Hub")]
        public event DcMessageHandler OnTopic;
        internal void DoTopic(string text)
        {
            if (OnTopic != null)
            {
                OnTopic(text);
            }
        }

        /// <summary>
        /// Occurs when a client to client connection request is recieved.
        /// </summary>
        [Description("Occurs when a client to client connection request is recieved.")]
        [Category("Hub")]
        public event DcUserHandler OnClientConnectionRecieved;
        internal void DoClientConnectionRecieved(User user)
        {
            if (OnClientConnectionRecieved != null)
            {
                OnClientConnectionRecieved(user);
            }
        }

        #endregion

        #region Search

        /// <summary>
        /// Occurs when a search is recieved.
        /// </summary>
        [Description("Occurs when a search is recieved.")]
        [Category("Search")]
        public event DcSearchHandler OnSearch;
        internal bool DoSearch(SearchInfo search)
        {
            if (OnSearch != null)
            {
                OnSearch(search);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a searchreply is recieved.
        /// </summary>
        [Description("Occurs when a searchreply is recieved.")]
        [Category("Search")]
        public event DcSearchReplyHandler OnSearchReply;
        internal bool DoSearchReply(SearchReply sr)
        {
            if (OnSearchReply != null)
            {
                OnSearchReply(sr);
                return true;
            }
            return false;
        }

        #endregion

        #region Trauma

        /// <summary>
        /// Occurs when a redirect request is recieved.
        /// </summary>
        [Description("Occurs when a redirect request is recieved.")]
        [Category("Trauma")]
        public event DcRedirectHandler OnRedirected;
        internal void DoRedirected(string hubAddress)
        {
            if (OnRedirected != null)
            {
                OnRedirected(hubAddress);
            }
        }

        //[Description("")]
        //[Category("Trauma")]
        //public event DcEventHandler OnKicked;
        
        //[Description("")]
        //[Category("Trauma")]
        //public event DcEventHandler OnDropped;

        #endregion

        #region Error

        /// <summary>
        /// Occurs when an exception is thrown.
        /// </summary>
        [Description("Occurs when an exception is thrown.")]
        [Category("Error")]
        public event DcErrorHandler OnError;
        internal bool DoError(Exception ex)
        {
            if (OnError != null)
            {
                OnError(ex);
                return true;
            }
            return false;
        }

        // TODO: If we know it's a bug, shouldn't we fix it instead of sending it to an event?
        [Description("")]
        [Category("Error")]
        public event DcErrorHandler OnBug;
        internal bool DoBug(Exception ex)
        {
            if (OnBug != null)
            {
                OnBug(ex);
                return true;
            }
            return false;
        }

        #endregion

    }
}
