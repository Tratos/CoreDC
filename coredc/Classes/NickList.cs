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
    using Classes;

    public class NickList
    {
        Dictionary<string, User> nickList = new Dictionary<string, User>();
        OperatorList operators = new OperatorList();
        
        public OperatorList Operators
        {
            get { return operators; }
        }

        internal User Get(string username)
        {
            return nickList[username];
        }        
        
        /// <summary>
        /// Adds or updates a user. Returns true if new user
        /// </summary>
        internal bool Set(User user)
        {
            bool exists = nickList.ContainsKey(user.Username);
            nickList[user.Username] = user;
            
            return (!exists);
        }
        
        internal void Remove(User user)
        {
            if (user != null) 
            {
                
                string username = user.Username;
                if (nickList.ContainsKey(username))
                {
                    nickList.Remove(username);
                }
                if (operators.operatorList.ContainsKey(username))
                {
                    operators.operatorList.Remove(username);
                }
            }            
        }
        
        
        internal void Clear()
        {
            nickList = new Dictionary<string,User>();
            operators = new OperatorList();
        }
        
        internal void SetOperators(ICollection<string> newOperators)
        {
            foreach(User op in operators.operatorList.Values)
            {
                op.IsOperator = false;
            }
            operators.operatorList.Clear();

            foreach (string opname in newOperators)
            {
                User u;
                if (nickList.TryGetValue(opname, out u))
                {
                    u.IsOperator = true;
                    operators.operatorList[u.Username] = u;
                }
            }            
        }
        
        public int Count
        {
            get
            {
                return nickList.Count;
            }
        }
        
        public User this[string userName]
        {
            get
            {
                User u;
                if (nickList.TryGetValue(userName, out u))
                {
                    return u.Clone();
                } 
                
                return null;
            }
        }

        public IEnumerator<User> GetEnumerator()
        {
            foreach (User u in nickList.Values)
            {
                yield return u.Clone();
            }
        }
    }
    
    public class OperatorList
    {
        internal Dictionary<string, User> operatorList = new Dictionary<string, User>();


        public int Count
        {
            get
            {
                return operatorList.Count;
            }
        }

        public User this[string userName]
        {
            get
            {
                User u = operatorList[userName];
                if (u != null)
                {
                    return u.Clone();
                }

                return null;
            }
        }
        
        public IEnumerator<User> GetEnumerator()
        {
            foreach (User u in operatorList.Values)
            {
                yield return u.Clone();
            }
        }
    }
    
    
}
