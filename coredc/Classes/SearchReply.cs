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
    public class SearchReply
    {
        User user = new User();
        /// <summary>
        /// The user the searchreply originates from
        /// </summary>
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        string filePath;
        /// <summary>
        /// The path and filename of the file in the users share
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        Int64 fileSize;
        /// <summary>
        /// The size of the file
        /// </summary>
        public Int64 FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        int slotsFree;
        /// <summary>
        /// Number of free slots of the user
        /// </summary>
        public int SlotsFree
        {
            get { return slotsFree; }
            set { slotsFree = value; }
        }

        int slotsTotal;
        /// <summary>
        /// Total number of slots of the user
        /// </summary>
        public int SlotsTotal
        {
            get { return slotsTotal; }
            set { slotsTotal = value; }
        }

        string TTHash;
        /// <summary>
        /// The Tiger Tree Hash of the file
        /// </summary>
        public string TTH
        {
            get { return TTHash; }
            set { TTHash = value; }
        }

        IPAddress hubIP = new IPAddress(0);
        /// <summary>
        /// The IP-address of the hub the user is connected to
        /// </summary>
        public IPAddress HubIP
        {
            get { return hubIP; }
            set { hubIP = value; }
        }

        int hubPort = 0;
        /// <summary>
        /// The port of the hub the user is connected to
        /// </summary>
        public int HubPort
        {
            get { return hubPort; }
            set { hubPort = value; }
        }

        /// <summary>
        /// Formats the variables into a proper DC-protocol searchreply
        /// </summary>
        public override string ToString()
        {
            //Format: $SR <ownname> <filenamewithpath)<filesizeinbytes>
            // <current openslots>/<allopenslots><hubname> (<hubip>:<hubport>)<targetuser> 

            StringBuilder sr = new StringBuilder();

            sr.Append(filePath);
            sr.Append(Convert.ToChar(5));
            sr.Append(fileSize.ToString());
            sr.Append(" " + slotsFree.ToString());
            sr.Append("/" + slotsTotal.ToString());
            sr.Append(Convert.ToChar(5));
            sr.Append("TTH:" + TTHash);
            sr.Append(" (" + hubIP.ToString());
            sr.Append(":" + hubPort.ToString() + ")");
            sr.Append(Convert.ToChar(5));
         
            return sr.ToString();
        }
    }
}
