using System;
using System.Collections.Generic;

using System.Net.Sockets;
using System.Text;

namespace Crypton.AvChat.Client.Net
{
    interface INetDispatcher : IDisposable
    {

        void Start(TcpClient networkClient);
        void Stop();

        event Action<SocketException> SocketException;

    }
}
