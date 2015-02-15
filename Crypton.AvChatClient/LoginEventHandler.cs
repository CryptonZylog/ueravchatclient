using System;
using System.Collections.Generic;

using System.Text;

namespace Crypton.AvChat.Client
{
    public delegate void LoginEventHandler(ChatClient chatClient, bool loginResult, string errorMessage);
}
