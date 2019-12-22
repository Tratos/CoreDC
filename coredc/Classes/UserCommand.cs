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
    public class UserCommand
    {
        // TODO: Split apart in the various sections, for easy GUI implementation

        string userCmd = String.Empty;
        public string UserCmd
        {
            get { return userCmd; }
            set { userCmd = value; }
        }
    }
}
