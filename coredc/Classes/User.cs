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
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace CoreDC.Classes
{
    public class User
    {
        // Note: When adding fields to the User class, don't forget to update the Clone method.    

        public User()
        {
        }
        public User(string username)
        {
            this.username = username;
        }
        
        string username = String.Empty;
        /// <summary>
        /// The users username
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        string replyTo = String.Empty;
        /// <summary>
        /// The user to send replies to when recieving a private message. Can be different from Username
        /// </summary>
        public string ReplyTo
        {
            get { return replyTo; }
            set { replyTo = value; }
        }

        IPAddress ip = new IPAddress(0);
        /// <summary>
        /// The users IP-address, if known
        /// </summary>
        public IPAddress IP
        {
            get { return ip; }
            set { ip = value; }
        }

        int port = 0;
        /// <summary>
        /// The port the user has open for filetransfers
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        string description = String.Empty;
        /// <summary>
        /// The users description, current also contains the client-tag
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //string tag = String.Empty;
        /// <summary>
        /// The users client-tag. Currently always empty, parse tag from Description instead
        /// </summary>
        //public string Tag
        //{
        //    get { return tag; }
        //    set { tag = value; }
        //}

        string speed = String.Empty;
        /// <summary>
        /// The users transfer (usually upload) speed
        /// </summary>
        public string Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        string email = String.Empty;
        /// <summary>
        /// The users e-mail address
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        Int64 shareSize;
        /// <summary>
        /// The size of the users share
        /// </summary>
        public Int64 ShareSize
        {
            get { return shareSize; }
            set { shareSize = value; }
        }

        bool isOperator;
        /// <summary>
        /// Signals whether the user is an operator or not in the currently connected hub
        /// </summary>
        public bool IsOperator
        {
            get { return isOperator; }
            set { isOperator = value; }
        }

        /// <summary>
        /// Deep copies the User instance.
        /// </summary>
        public User Clone()
        {
            User u = (User)MemberwiseClone();

            u.ip = new IPAddress(ip.GetAddressBytes());
            return u;
        }
    }
}
