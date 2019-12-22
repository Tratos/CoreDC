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
using System.Text.RegularExpressions;
using System.Net;

namespace CoreDC.Implementation
{
    using Classes;
     
    internal class ProtocolHandler
    {
        readonly DcClient client;
        
        public ProtocolHandler(DcClient client)
        {
            this.client = client;
        }


        /// <summary>
        /// Handles protocol commands and trigger the proper events based
        /// on the type of command it recieves.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ProcessCommand(string message)
        {
            client.DoMessageReceived(message);

            string command;
            string parameter;

            try
            {
                // split message into command and parameters
                command = message.Substring(0, message.IndexOf(" "));
                parameter = message.Substring(message.IndexOf(" ") + 1);
            }
            catch
            {
                // Command has no parameters. ($GetPass for example)
                command = message;
                parameter = "";
            }
            
            if (message.StartsWith("<"))
            {
                try
                {
                    int divider = message.IndexOf(">");
                    string userName = message.Substring(1, divider - 1);
                    bool ownMessage = (userName == client.Username);

                    User user = new User();
                    user.Username = userName;
                    client.DoMainchatMessage(user, message.Substring(divider + 1).Trim(), ownMessage);
                }
                catch
                {
                    // Command was malformed (just "<" for example)
                    client.DoMainchatMessage(new User(), message, false);
                }
            }
            else
            {
                switch (command)
                {
                    case "$Search":
                        // Search sent from other clients, most common protocolmessage
                        DoSearch(parameter);
                        break;
                    case "$MyINFO":
                        // Client information string, sent for all users on login, and when new users joins
                        DoMyInfo(parameter);
                        break;
                    case "$Quit":
                        // Specified user has exited the hub
                        DoQuit(parameter);
                        break;
                    case "$SR":
                        // Searchresult sent back if passive
                        DoSearchResult(parameter);
                        break;
                    case "$ConnectToMe":
                        // Another client wants us to initiate client to client connection (active users)
                        DoConnectToMe(parameter);
                        break;
                    case "$RevConnectToMe":
                        // Another client wants us to ask them to initiate client to client connection (passive users)
                        break;
                    case "$To:":
                        // Private message
                        DoPrivateMessage(parameter);
                        break;
                    case "$OpForceMove":
                        // Someone wants to redirect us
                        DoForceMove(parameter);
                        break;
                    /*
                    We won't get this message afaik, it's only sent to the hub
                    case "$Kick":
                        // We've been kicked out
                        break; 
                    */
                    case "$Lock":
                        // First thing we get from the hub
                        DoLock(message, parameter);
                        break;
                    case "$HubName":
                        // Second thing we get from the hub, after Validatenick
                        DoHubName(parameter);
                        break;
                    case "$GetPass":
                        // A password is required, nick is probably registered
                        DoGetPass();
                        break;
                    case "$Hello":
                        // We are welcome into the hub, alternatively another user has joined
                        DoHello(parameter);
                        break;
                    case "$OpList":
                        // List of operator nicknames
                        DoOpList(parameter);
                        break;
                    case "$BadPass":
                        // Incorrect password, not sent by all hubs
                        DoBadPass();
                        break;
                    case "$HubIsFull":
                        // Sent if hub is full, usually after it's checked if we have account
                        break;
                    /*
                    // EXTENDEDPROTOCOL list of extra (non-original-nmdc) features supported by hub (& clients)
                    */
                    case "$Supports":
                        DoSupports(parameter);
                        break;
                    case "$UserCommand":
                        DoUserCommand(parameter);
                        break;
                    case "$HubINFO":
                        DoHubInfo(parameter);
                        break;
                    case "$HubTopic":
                        // Verlihub only?
                        DoHubTopic(parameter);
                        break;
                    default:       
                        // ?
                        break;
                }
            }   
        }


        /// <summary>
        /// Sent from hub when we open a socket to it, must be decoded (for most hubs) in order to gain access.
        /// </summary>
        /// <param name="message">The complete $Lock protocol message</param>
        /// <param name="parameter">Contains only the $Lock string</param>
        void DoLock(string message, string parameter)
        {

            string key = LockToKey(parameter);
            client.HubInfo.Lock = parameter;
            client.HubInfo.Key = key;
            client.HubInfo.HubSoft = HubDetection.IdentifyHub(message);

            string userName = client.Username;
            string supports = String.Format("$Supports UserCommand NoGetINFO NoHello UserIP2 TTHSearch BotINFO|$Key {0}|$ValidateNick {1}|", key, userName);
            client.SendRawMessage(supports);
        }


        /// <summary>
        /// Sent from the hub when we enter, and when hubname and/or topic changes
        /// </summary>
        /// <param name="parameter">Contains the hubname, which includes topic (if exists)</param>
        void DoHubName(string parameter)
        {
            int index = parameter.IndexOf(" - ");           
            if (index > -1)
            {
                client.HubInfo.Hubname = parameter.Substring(0, index);
                string topic = parameter.Substring(index + 3);
                client.HubInfo.Topic = topic;
                client.DoTopic(topic);                                                
            }
            else
            {
                client.HubInfo.Hubname = parameter;
                client.HubInfo.Topic = String.Empty;
            }
                                    
        
            if (!client.IsConnected)
            {
                string password = client.Password;
                if (!String.IsNullOrEmpty(password))
                {
                    client.SendRawMessage(String.Format("$MyPass {0}|", password));
                }
                
                string userName = client.Username;            
                string description = client.ClientInfo.Description;
                string email = client.ClientInfo.Email;
                string message = String.Format("$Version 1,009|$MyINFO $ALL {0} {1} <CoreDC>$ $BOT${2}$0$|", userName, description, email);
                client.SendRawMessage(message);            
            }
        }

        /// <summary>
        /// New topic was set in the hub
        /// </summary>
        /// <param name="topic">The new topic</param>
        void DoHubTopic(string topic)
        {
            client.DoTopic(topic);
        }


        /// <summary>
        /// Announces new users, when sent with our nickname it means we are welcome.
        /// Mostly obsolete, MyINFO is usually sent right away instead, when supporting NoHello.
        /// </summary>
        /// <param name="parameter">Contains username sent with the $Hello command</param>
        void DoHello(string parameter)  
        {
            if (client.Username == parameter && !client.IsConnected)
            {
                // when the client gets an hello it means we are welcome.
                // We only want to send it once, or we'll be nicklist spamming.
                client.IsConnected = true;
                client.DoConnected();
                client.SendRawMessage("$GetNickList|");
            }   
        }


        /// <summary>
        /// A password is required. Either our username is registered, or hub requires global password.
        /// </summary>
        void DoGetPass()
        {
            client.SendRawMessage(String.Format("$MyPass {0}|", client.ClientInfo.Password));
        }


        /// <summary>
        /// The password we sent was incorrect according to the hub. 
        /// </summary>
        void DoBadPass()
        {
            // TODO: Since not all hubs send BadPass, we might have to hook into the general disconnection
            // event and do some kind of guesswork as to why we were disconnected, i.e if it happens right
            // after we sent our password, well maybe that's the reason then. Not enough to rely on BadPass.
            client.DoPasswordRejected();
        }
               
        
        /// <summary>
        /// Hub supports Extended protocol features.
        /// </summary>
        /// <param name="parameter">Space delimited list of supported EP features.</param>
        void DoSupports(string parameter)
        {
            string[] supports = parameter.Split(" ".ToCharArray());
            List<string> supportlist = new List<string>(supports.Length);
            supportlist.AddRange(supports);

            client.HubInfo.HubSupports = supportlist;
        }


        /// <summary>
        /// We have been redirected from the hub.
        /// </summary>
        /// <param name="parameter">Redirection hub address</param>
        void DoForceMove(string parameter)
        {
            // TODO: Needs cleanup. Extend HubInfo to include IP and Port and use that?
            client.DoRedirected(parameter);
        }


        /// <summary>
        /// Recieved a UserCommand from the hub.
        /// </summary>
        /// <param name="parameter">UserCommand string</param>
        void DoUserCommand(string parameter)
        {
            // TODO: Need to make UserCommand a proper class and split this nicely to fit.
            // Preferably with Regexp :)
            UserCommand uc = new UserCommand();
            uc.UserCmd = parameter;

            client.DoUserCommand(uc);
        }


        /// <summary>
        /// Response to a sent $BotINFO request.
        /// </summary>
        /// <param name="parameter">String with various hubspecific data.</param>
        void DoHubInfo(string parameter)
        {
            // TODO: We should probably break this apart, not just store it in HubInfo.
            // TODO: Also, give Topic support.
            // Example: ..::HaR–RaCiNg-Huﬂ::..$hardracing.myip.hu:1209$[HUN] Hardcore & Streetracing hub.px.$500$0$0$0$PtokaX$
            client.HubInfo.Info = parameter;
        }


        /// <summary>
        /// A user has exited the hub.
        /// </summary>
        /// <param name="parameter">The username of the quitter</param>
        void DoQuit(string parameter)
        {
            User user = client.NickList[parameter];
            if (user != null)
            {
                client.NickList.Remove(user);
                client.DoParts(user.Clone());
            }
        }


        /// <summary>
        /// A list of users who are operators.
        /// </summary>
        /// <param name="parameter">The list of users</param>
        void DoOpList(string parameter)
        {
            // TODO: Need to split this list and put it into the NickList.OperatorList
        }


        /// <summary>
        /// A user wants us to initiate a client-to-client connection to them
        /// </summary>
        /// <param name="parameter">String with connection specific data</param>
        void DoConnectToMe(string parameter)
        {
            // $ConnectToMe <remoteNick> <senderIp>:<senderPort>
            User user = new User();

            // TODO: Split string properly and store in user object.
            client.DoClientConnectionRecieved(user);
        }
        

        /// <summary>
        /// Someone sent us a private message.
        /// </summary>
        /// <param name="parameter">String with PM specific data</param>
        void DoPrivateMessage(string parameter)
        {
            // $To: <nickname> From: <ownnickname> $<ownnickname> <message> <message> <message>

            // Unescaped pm regexp
            // "^(?<to>[^\s]+) From: (?<firstfrom>[^\s]+) \$<(?<secondfrom>[^\s]+)>(?<message>[\s\S]*)$"

            User user = new User();

            // Run regexp
            Regex regExSearch = new Regex("^(?<to>[^\\s]+) From: (?<firstfrom>[^\\s]+) \\$<(?<secondfrom>[^\\s]+)>(?<message>[\\s\\S]*)$");
            Match regExResults = regExSearch.Match(parameter);

            // Need to set a replyto, incase it's a chatroom or the like.
            user.ReplyTo = regExResults.Groups["firstfrom"].Value ?? String.Empty;
            user.Username = regExResults.Groups["secondfrom"].Value ?? String.Empty;

            // Get message
            string text = regExResults.Groups["message"].Value.Trim() ?? String.Empty;

            client.DoPrivateMessageRecieved(user, text);
        }
        

        /// <summary>
        /// Response to a $MyINFO datastring
        /// </summary>
        /// <param name="parameter">String with user specific data</param>
        void DoMyInfo(string parameter)
        {
            // Unescaped regexp
            // "^\$ALL (?<name>[^\x20]+) (?<desc>[^$]*?)\$ \$(?<speed>[^\x01-\x12]*?)[\x01-\x12]{0,1}\$(?<email>.*?)\$(?<share>\d+)\$$"

            User user = new User();

            // Run regexp
            Regex regExSearch = new Regex("^\\$ALL (?<name>[^\\x20]+) (?<desc>[^$]*?)\\$ \\$(?<speed>[^\\x01-\\x12]*?)[\\x01-\\x12]{0,1}\\$(?<email>.*?)\\$(?<share>\\d+)\\$$");
            Match regExResults = regExSearch.Match(parameter);

            // Set username
            user.Username = regExResults.Groups["name"].Value ?? String.Empty;

            // Set description - including tag
            user.Description = regExResults.Groups["desc"].Value ?? String.Empty;

            // Set speed
            user.Speed = regExResults.Groups["speed"].Value ?? String.Empty;

            // Set e-mail
            user.Email = regExResults.Groups["email"].Value ?? String.Empty;

            // Set sharesize
            Int64 shareSize;
            Int64.TryParse(regExResults.Groups["share"].Value, out shareSize);
            user.ShareSize = shareSize;

            // Send to event
            if (client.NickList.Set(user))
            {
                client.DoJoin(user.Clone());
            }
            else
            {
                client.DoUserChanged(user.Clone());
            }
        }

        
        /// <summary>
        /// Search sent from another user
        /// </summary>
        /// <param name="parameter">String with searchresult-specific data</param>
        void DoSearch(string parameter)
        {
            // Unescaped passive regexp
            // "^Hub:(?<name>.+) (?<sizerestr>[FT])\?(?<hassize>[FT])\?(?<size>\d+)\?(?<type>\d{1,2})\?(?<pattern>[0-9A-Za-z\$\:]+)$"
            //
            // Unescaped active regexp
            // "^(?<ip>[0-9\.]{7,15}):(?<port>[0-9]{1,5}) (?<sizerestr>[FT])\?(?<hassize>[FT])\?(?<size>\d+)\?(?<type>\d{1,2})\?(?<pattern>[0-9A-Za-z\$\:]+)$"

            SearchInfo search = new SearchInfo();

            Regex regExSearch;
            Match regExResults;

            // Passive, we get a nickname
            if (parameter.StartsWith("Hub:"))
            {
                regExSearch = new Regex("^Hub:(?<name>.+) (?<sizerestr>[FT])\\?(?<hassize>[FT])\\?(?<size>\\d+)\\?(?<type>\\d{1,2})\\?(?<pattern>.+)$");
                regExResults = regExSearch.Match(parameter);

                search.User.Username = regExResults.Groups["name"].Value ?? String.Empty;
            }
            // Active, we get an ip (and port)
            else
            {
                regExSearch = new Regex("^(?<ip>[0-9\\.]{7,15}):(?<port>[0-9]{1,5}) (?<sizerestr>[FT])\\?(?<hassize>[FT])\\?(?<size>\\d+)\\?(?<type>\\d{1,2})\\?(?<pattern>.+)$");
                regExResults = regExSearch.Match(parameter);

                search.IsActive = true;

                // Set Ip address
                IPAddress address = new IPAddress(0);
                IPAddress.TryParse(regExResults.Groups["ip"].Value, out address);
                search.User.IP = address;

                // Set port
                int port = 0;
                Int32.TryParse(regExResults.Groups["port"].Value, out port);
                search.User.Port = port;
            }

            // Set sizerestriction
            if (regExResults.Groups["sizerest"].Value == "T")
            {
                search.SizeRestriction = SearchInfo.SearchSizeRestriction.Maximum;
            }
            else
            {
                search.SizeRestriction = SearchInfo.SearchSizeRestriction.Minimum;
            }

            // Set minimum/maximum size of the search
            Int64 searchSize;
            Int64.TryParse(regExResults.Groups["size"].Value, out searchSize);
            search.SearchSize = searchSize;

            // Set datatype of the search
            if (!String.IsNullOrEmpty(regExResults.Groups["type"].Value))
            {
                search.DataType = (SearchInfo.SearchDataType)Enum.Parse(typeof(SearchInfo.SearchDataType), regExResults.Groups["type"].Value, true);
            }

            // Check if it's a TTH search
            if (Regex.IsMatch(regExResults.Groups["pattern"].Value, "TTH:\\w{39}"))
            {
                // Then save the TTH
                search.SearchPattern = regExResults.Groups["pattern"].Value.Replace("TTH:", "");
                search.IsTTH = true;
            }
            else
            {
                // Otherwise save the search as "word1 word2"
                search.SearchPattern = regExResults.Groups["pattern"].Value.Replace("$", " ") ?? String.Empty;
            }

            // Send to event
            client.DoSearch(search);
        }
        

        /// <summary>
        /// Response to a sent search
        /// </summary>
        /// <param name="parameter">String with searchresult-specific data</param>
        void DoSearchResult(string parameter)
        {
            // Unescaped regexp
            // "^(?<name>[^\s]+) (?<file>.*?)\x05{0,1}(?<size>\d*?) (?<slotsfree>\d*?)/(?<slotstotal>\d*?)\x05TTH:{0,1}(?<tth>\w{0,39}) \((?<ip>[0-9\.]{7,15}):(?<port>[0-9]{1,5})\)$"

            SearchReply sr = new SearchReply();

            // Run regexp
            Regex regExSearch = new Regex("^(?<name>[^\\s]+) (?<file>.*?)\\x05{0,1}(?<size>\\d*?) (?<slotsfree>\\d*?)/(?<slotstotal>\\d*?)\\x05TTH:{0,1}(?<tth>\\w{0,39}) \\((?<ip>[0-9\\.]{7,15}):(?<port>[0-9]{1,5})\\)$");
            Match regExResults = regExSearch.Match(parameter);

            // Set user name
            sr.User.Username = regExResults.Groups["name"].Value ?? String.Empty;

            // Set Ip address
            IPAddress address = new IPAddress(0);
            IPAddress.TryParse(regExResults.Groups["ip"].Value, out address);
            sr.User.IP = address;

            // Set port
            int port;
            Int32.TryParse(regExResults.Groups["port"].Value, out port);
            sr.User.Port = port;

            // Set free slots
            int slotsFree;
            Int32.TryParse(regExResults.Groups["slotsfree"].Value, out slotsFree);
            sr.SlotsFree = slotsFree;

            // Set total slots
            int slotsTotal;
            Int32.TryParse(regExResults.Groups["slotstotal"].Value, out slotsTotal);
            sr.SlotsTotal = slotsTotal;

            // Set filedata
            sr.FilePath = regExResults.Groups["file"].Value ?? String.Empty;
            sr.TTH = regExResults.Groups["tth"].Value ?? String.Empty;

            // Set filesize
            Int64 fileSize = 0;
            Int64.TryParse(regExResults.Groups["size"].Value, out fileSize);
            sr.FileSize = fileSize;

            // Send to event
            client.DoSearchReply(sr);
        }

        /// <summary>
        /// Create a matching Key for a recieved Lock
        /// </summary>
        /// <param name="lck">$Lock data recieved from the hub</param>
        /// <returns>$Key generated from $Lock</returns>
        static string LockToKey(string lck)
        {
            lck = lck.Replace("$Lock ", "");
            int iPos = lck.IndexOf(" Pk=", 1);
            if (iPos > 0) lck = lck.Substring(0, iPos);

            char[] arrChar = new char[lck.Length];
            int[] arrRet = new int[lck.Length];
            arrChar[0] = lck[0];
            for (int i = 1; i < lck.Length; i++)
            {
                //arrChar[i] = lck[i];
                byte[] test = Encoding.Default.GetBytes(new Char[] { lck[i] });
                arrChar[i] = (char)test[0];
                arrRet[i] = arrChar[i] ^ arrChar[i - 1];
            }
            arrRet[0] = arrChar[0] ^ arrChar[lck.Length - 1] ^ arrChar[lck.Length - 2] ^ 5;
            string sKey = "";
            for (int n = 0; n < lck.Length; n++)
            {
                arrRet[n] = ((arrRet[n] * 16 & 240)) | ((arrRet[n] / 16) & 15);
                int j = arrRet[n];
                switch (j)
                {
                    case 0:
                    case 5:
                    case 36:
                    case 96:
                    case 124:
                    case 126:
                        sKey += String.Format("/%DCN{0:000}%/", j);
                        break;
                    default:
                        sKey += Encoding.Default.GetChars(new byte[] { Convert.ToByte((char)j) })[0];
                        break;
                }
            }
            return sKey;
        }
    }
}
