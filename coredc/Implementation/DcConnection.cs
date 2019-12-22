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
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CoreDC.Implementation
{

    internal class DcConnection : IDisposable
    {
        static readonly int bufferSize = 1024;
        static readonly int pulseInterval = 194000;
        static readonly int readInterval = 200;
        readonly byte[] readBuffer = new byte[bufferSize];

        TcpClient client;
        NetworkStream stream;

        System.Windows.Forms.Timer readTimer;
        System.Windows.Forms.Timer pulseTimer;

        DateTime lastSent;

        StringBuilder readQueue;

        readonly DcClient dcClient;
        
        public DcConnection(DcClient client)
        {
            this.dcClient = client;
        }

        public void Open(string server, int port)
        {
            client = new TcpClient(server, port);
            stream = client.GetStream();

            pulseTimer = new System.Windows.Forms.Timer();
            pulseTimer.Interval = pulseInterval;
            pulseTimer.Tick += new EventHandler(pulseTimer_Tick);
            pulseTimer.Start();

            readTimer = new System.Windows.Forms.Timer();
            readTimer.Interval = readInterval;
            readTimer.Tick += readTimer_Tick;
            readTimer.Start();

            lastSent = DateTime.Now;
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            readTimer.Dispose();
            pulseTimer.Dispose();
            stream.Close();
            client.Close();
        }

        /// <summary>
        /// Triggered every 3 minutes or so to check that the connection is still alive
        /// </summary>
        void pulseTimer_Tick(object sender, EventArgs e)
        {
            // Only send a pulse if nothing has been sent/recieved for over 1 minute
            if (lastSent.AddMinutes(1) <= DateTime.Now)
            {
                Write("|");
            }
        }

        public void Write(string message)
        {
            // TODO: Need to handle when the stream is closed (?)
            // "Unable to write data to the transport connection: An existing connection was forcibly closed by the remote host."
            try
            {
                Byte[] data = Encoding.Default.GetBytes(message);

                stream.Write(data, 0, data.Length);
                stream.Flush();

                lastSent = DateTime.Now;
            }
            catch (System.IO.IOException)
            {
                // If we can't write to the stream, we've been disconnected
                Dispose();
                dcClient.DoDisconnected();
            }
            catch (Exception ex)
            {
                // Something else might be wrong perhaps?
                Exception res = dcClient.TryReport(dcClient, ex);
                if (res != null) throw;
            }
        }

        void readTimer_Tick(object sender, EventArgs e)
        {
            if (stream.DataAvailable)
            {
                try
                {
                    int bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);

                    if (bytesRead > 0)
                    {
                        Read(Encoding.Default.GetString(readBuffer, 0, bytesRead));
                    }
                }
                catch (System.IO.IOException)
                {
                    // If we can't read from the stream, we've been disconnected
                    Dispose();
                    dcClient.DoDisconnected();
                }
                catch (Exception ex)
                {
                    // Something else might be wrong perhaps?
                    Exception res = dcClient.TryReport(dcClient, ex);
                    if (res != null) throw;
                }
            }
        }

        void Read(string frame)
        {
            string messageBundle = null;
            if (frame.EndsWith("|"))
            {
                //message bundle is intact
                messageBundle = GetReadQueue() + frame;
            }
            else
            {
                int index = frame.LastIndexOf('|');
                if (index == -1)
                {
                    //whole frame is part of message bundle
                    AddToReadQueue(frame);
                }
                else
                {
                    //part of frame is a message bundle
                    messageBundle = GetReadQueue() + frame.Substring(0, index + 1);
                    //while the rest must be queued
                    AddToReadQueue(frame.Substring(index + 1));
                }
            }

            if (messageBundle != null)
            {
                // split the bundle into seperate messages..
                string[] messages = messageBundle.Split("|".ToCharArray());
                foreach (string message in messages)
                {
                    // but only handle messages that actually carries any information 
                    if (message.Length > 1)
                    {
                        // and trigger event for each message without the trailing pipe
                        OnMessage(message.Substring(0, message.Length));
                    }
                }
            }
        }

        string GetReadQueue()
        {
            if (readQueue != null)
            {
                string s = readQueue.ToString();
                readQueue = null;
                return s;
            }
            return String.Empty;
        }

        void AddToReadQueue(string overflow)
        {
            if (readQueue == null)
            {
                readQueue = new StringBuilder();
            }
            readQueue.Append(overflow);
        }
        
        public event DcMessageHandler OnMessage;

    }
}