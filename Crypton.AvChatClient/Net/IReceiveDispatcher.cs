using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Text;

namespace Crypton.AvChat.Client.Net
{
    
    interface IReceiveDispatcher : INetDispatcher
    {

        event Action<NameValueCollection> PacketReceived;
        TimeSpan TimeSinceLastPacket { get; }

    }
}
