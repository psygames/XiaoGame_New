using System;
namespace Plugins.Network
{
    public static class WebSocketTool
    {
        public static string GetAddress(string ip, int port)
        {
            return "ws://{0}:{1}".FormatStr(ip, port);
        }
    }
}
