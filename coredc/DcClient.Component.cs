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
using System.Security.Permissions;
using System.Text;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]
[assembly: CLSCompliant(true)]
namespace CoreDC
{
    using CoreDC.Implementation;
    using CoreDC.Classes;

    public partial class DcClient : Component, IDisposable
    {

        /// <summary>
        /// Client information. Must be set before a connection can be established.
        /// </summary>
        [Bindable(true)]
        [Category("General")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Client information. Must be set before a connection can be established.")]
        public ClientInfo ClientInfo { get { return clientInfo; } }
        private ClientInfo clientInfo = new ClientInfo();
    
        /// <summary>
        /// Contains information about current connected hub.  
        /// If not connected, all properties will be empty or default. 
        /// </summary>
        [Browsable(false)]
        public HubInfo HubInfo { get { return hubInfo; } }
        private HubInfo hubInfo = new HubInfo();

        /// <summary>
        /// Contains the users in a hub. Entry point for retrieving User instances
        /// If not connected, Nicklist will contain zero users. 
        /// </summary>
        [Browsable(false)]
        public NickList NickList { get { return nickList; } }
        private NickList nickList = new NickList();

        public DcClient()
        {
            InitializeComponent();
            InitializeInstance();
        }

        public DcClient(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            InitializeInstance();
        }
    
    }
}
