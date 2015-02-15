using Crypton.AvChat.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Crypton.AvChat.Client.Net
{
    /// <summary>
    /// Encapsulates receiving data from the network stream
    /// </summary>
    class ReceiveDispatcher : IReceiveDispatcher
    {

        private Stopwatch timeSinceLastPacketTimer = new Stopwatch();

        private class StateObject
        {
            public const int BufferSize = 16 * 1024;
            public byte[] Buffer = new byte[BufferSize];
            public List<byte> ReceivedBytes = new List<byte>();
            public Socket Socket { get; private set; }
            public StateObject(Socket socket)
            {
                this.Socket = socket;
            }
        }

        private bool _disposed = false;

        /// <summary>
        /// Fires when a new packet has been received
        /// </summary>
        public event Action<NameValueCollection> PacketReceived;

        /// <summary>
        /// Begins listening for new packets on the network stream
        /// </summary>
        /// <param name="networkStream"></param>
        public void Start(TcpClient networkClient)
        {
            this.timeSinceLastPacketTimer.Reset();
            this.receiveFromSocketAsync(networkClient.Client);
        }

        public void Stop()
        {
            this.timeSinceLastPacketTimer.Stop();
        }

        public void Dispose()
        {
            this._disposed = true;
        }

        private void receiveFromSocketAsync(Socket socketClient)
        {
            if (this._disposed)
                return;
            try
            {
                StateObject state = new StateObject(socketClient);
                socketClient.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, receiveFromSocketCallback, state);
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
            catch (ObjectDisposedException socketDisposed)
            {
                //Trace.TraceError(socketDisposed.ToString());
                this._disposed = true;
            }
        }

        private void receiveFromSocketCallback(IAsyncResult result)
        {
            if (!this._disposed)
            {
                StateObject state = (StateObject)result.AsyncState;

                // read data
                int bytesRead = state.Socket.EndReceive(result);
                if (bytesRead > 0)
                {
                    // there could be more data, store current in buffer
                    for (int i = 0; i < bytesRead; i++)
                    {
                        byte piece = state.Buffer[i];
                        if (piece == '\0')
                        {
                            // consider the current array of bytes to be the final packet
                            if (state.ReceivedBytes.Count > 0)
                            {
                                processPacketScope(state.ReceivedBytes.ToArray());
                                state.ReceivedBytes.Clear();
                            }
                        }
                        else
                        {
                            state.ReceivedBytes.Add(piece);
                        }
                    }

                    // receive the rest
                    state.Socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, receiveFromSocketCallback, state);
                }
                else
                {
                    // all data we have so far
                    if (state.ReceivedBytes.Count > 0)
                    {
                        processPacketScope(state.ReceivedBytes.ToArray());
                        state.ReceivedBytes.Clear();
                    }
                }
            }
        }

        private void processPacketScope(byte[] packetBytes)
        {
            string packetString = Encoding.UTF8.GetString(packetBytes);
            NameValueCollection nvcPacket = XmlPacketBuilder.ReadPacket(packetString);
            if (nvcPacket != null)
            {
                this.timeSinceLastPacketTimer.Reset();
                this.timeSinceLastPacketTimer.Start();
                if (this.PacketReceived != null)
                {
                    foreach (Action<NameValueCollection> callback in this.PacketReceived.GetInvocationList())
                    {
                        callback.BeginInvoke(nvcPacket, null, null);
                    }
                }
            }
        }

        public event Action<SocketException> SocketException;


        public TimeSpan TimeSinceLastPacket
        {
            get { return this.timeSinceLastPacketTimer.Elapsed; }
        }
    }
}
