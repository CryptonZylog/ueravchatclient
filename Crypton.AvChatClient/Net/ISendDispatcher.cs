using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Text;

namespace Crypton.AvChat.Client.Net
{
    interface ISendDispatcher : INetDispatcher
    {

        void SendPacket(NameValueCollection packet);

    }
}
