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

namespace CoreDC.Classes
{
    [Serializable]
    [TypeConverter(typeof(ClientInfoConverter))]
    public class ClientInfo
    {
        string hostname = "localhost";
        public string Hostname
        {
            get { return hostname; }
            set { hostname = value; }
        }

        int port = 411;
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        string username = "-[CoreDc]-";
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        string password = String.Empty;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        string description = String.Empty;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        string email = String.Empty;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        
    }

    class ClientInfoConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(ClientInfo));
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            ClientInfo ci = (ClientInfo)value;
            if (String.IsNullOrEmpty(ci.Hostname))
            {
                return "[N/A]";
            }
            return ci.Hostname + ":" + ci.Port.ToString();
        }
    }
}