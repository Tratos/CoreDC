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
    public class SearchInfo
    {
        public enum SearchDataType { Any = 1, Audio = 2, Compressed = 3, Documents = 4, Executables = 5, Pictures = 6, Video = 7, Folder = 8, TTH = 9 };
        public enum SearchSizeRestriction { Minimum = 0, Maximum = 1 };

        bool isActive;
        /// <summary>
        /// Signals whether this is an active search or passive
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        bool isTTH;
        /// <summary>
        /// Signals whether this is a Tiger Tree Hash search or regular word search
        /// </summary>
        public bool IsTTH
        {
            get { return isTTH; }
            set { isTTH = value; }
        }

        User user = new User();
        /// <summary>
        /// The user the search originates from
        /// </summary>
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        string searchPattern;
        /// <summary>
        /// The words or TTH the user is searching for
        /// </summary>
        public string SearchPattern
        {
            get { return searchPattern; }
            set { searchPattern = value; }
        }           

        SearchDataType dataType = SearchDataType.Any;
        /// <summary>
        /// The datatype of the search (what extensions the datatypes map to is client dependent)
        /// </summary>
        public SearchDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        SearchSizeRestriction sizeRestriction = SearchSizeRestriction.Minimum;
        /// <summary>
        /// Dependent on SearchSize, signals if it should be a maximum or minimum size
        /// </summary>
        public SearchSizeRestriction SizeRestriction
        {
            get { return sizeRestriction; }
            set { sizeRestriction = value; }
        }

        Int64 searchSize = 0;
        /// <summary>
        /// The maximum or minimum (dependent on what SizeRestriction is set to) size of the file(s) to find
        /// </summary>
        public Int64 SearchSize
        {
            get { return searchSize; }
            set { searchSize = value; }
        }

        /// <summary>
        /// Formats the variables into a proper DC-protocol search
        /// </summary>
        public override string ToString()
        {
            string[] sp = searchPattern.Split(" ".ToCharArray());
            StringBuilder searchInfo = new StringBuilder();

            if (searchSize > 0)
            {
                // Yes there is a sizelimit
                searchInfo.Append("T?");
            }
            else
            {
                // No there is not a searchlimit
                searchInfo.Append("F?");
            }

            if (sizeRestriction == SearchSizeRestriction.Minimum)
            {
                // At least (default)
                searchInfo.Append("F?");
            }
            else
            {
                // At most
                searchInfo.Append("T?");
            }

            // Add the size
            searchInfo.Append(searchSize.ToString() + "?");

            // Add the type of file we want
            searchInfo.Append(((int)dataType).ToString() + "?");

            // Finally add the searchpattern
            if (IsTTH)
            {
                searchInfo.Append("TTH:" + searchPattern);
            }
            else
            {
                foreach (string s in sp)
                {
                    searchInfo.Append(s + "$");
                }
            }
            searchInfo.Append("$");

            return searchInfo.ToString();
        }
    }
    
}
