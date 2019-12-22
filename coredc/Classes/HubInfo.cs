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

namespace CoreDC.Classes
{
    public class HubInfo
    {
        string lck = String.Empty;
        /// <summary>
        /// The $Lock recieved from the hub in the initial handshake
        /// </summary>
        public string Lock
        {
            get { return lck; }
            set { lck = value; }
        }

        string key = String.Empty;
        /// <summary>
        /// The $Key sent to the hub in response to the $Lock
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        List<string> hubSupports = new List<string>();
        /// <summary>
        /// The extended protocol functions (outside the original dc protocol) the hub supports
        /// </summary>
        public List<string> HubSupports
        {
            get { return hubSupports; }
            set { hubSupports = value; }
        }

        string hubname = String.Empty;
        /// <summary>
        /// The hubs name
        /// </summary>
        public string Hubname
        {
            get { return hubname; }
            set { hubname = value; }
        }

        string topic = String.Empty;
        /// <summary>
        /// The current topic set in the hub if supported
        /// </summary>
        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }        

        HubDetection.HubType hubSoft;
        /// <summary>
        /// The type of hubsoftware the hub is running on
        /// </summary>
        public HubDetection.HubType HubSoft
        {
            get { return hubSoft; }
            set { hubSoft = value; }
        }

        string hubVersion;
        /// <summary>
        /// The version of the hubsoftware the hub is running on
        /// </summary>
        public string HubVersion
        {
            get { return hubVersion; }
            set { hubVersion = value; }
        }

        string hubInfo;
        /// <summary>
        /// The version of the hubsoftware the hub is running on
        /// </summary>
        public string Info
        {
            get { return hubInfo; }
            set { hubInfo = value; }
        }
    }
    
}
