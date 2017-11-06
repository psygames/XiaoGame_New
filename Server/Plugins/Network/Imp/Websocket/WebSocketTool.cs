using System;
namespace Plugins.Network
{
    public static class WebSocketTool
    {
        public static string GetAddress(string ip, int port)
        {
            return $"ws://{ip}:{port}";
        }
    }
}
