using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Crypton.AvChat.Win
{
    class ConsoleWindowTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            ChatDispatcher.Singleton.LogToConsole(message);
        }

        public override void WriteLine(string message)
        {
            ChatDispatcher.Singleton.LogToConsole(message);
        }
    }
}
