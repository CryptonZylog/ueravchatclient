using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Net.Sockets;
using System.Text;

namespace Crypton.AvChat.Client.Net
{
    class SendDispatcher : ISendDispatcher
    {

        private Socket networkSocket = null;
        private bool _dispoed = false;

        public void SendPacket(System.Collections.Specialized.NameValueCollection packet)
        {
            if (this._dispoed)
                return;
            string packetData = XmlPacketBuilder.CreatePacket(packet);
            byte[] packetBytes = Encoding.UTF8.GetBytes(packetData + '\0');

            try
            {
                this.networkSocket.BeginSend(packetBytes, 0, packetBytes.Length, SocketFlags.None, sendCallback, networkSocket);
            }
            catch (SocketException socketException)
            {
                //Trace.TraceError(socketException.ToString());
                if (this.SocketException != null)
                {
                    foreach (Action<SocketException> callback in this.SocketException.GetInvocationList())
                    {
                        callback.BeginInvoke(socketException, null, null);
                    }
                }
            }
            catch (ObjectDisposedException objectDisposed)
            {
                //Trace.TraceError(objectDisposed.ToString());
                this._dispoed = true;
            }
        }

        private void sendCallback(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            int bytesSent = socket.EndSend(result);
        }

        public void Start(System.Net.Sockets.TcpClient networkClient)
        {
            this.networkSocket = networkClient.Client;
        }

        public void Stop()
        {

        }

        public void Dispose()
        {
            this._dispoed = true;
        }
        
        public event Action<SocketException> SocketException;
    }
}
