using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WebSocketTest
{
    public class SocketManager
    {
        public static Dictionary<string, WebSocket> WebSockets { get; set; } = new Dictionary<string, WebSocket>();

        public static void Add(string key, WebSocket socket)
        {
            WebSockets.Add(key, socket);
        }
        public static WebSocket Get(string key)
        {
            return WebSockets[key];
        }
    }
}
