using System;
namespace RedStone
{
    public class MCProxyBase : ProxyBase
    {
        public void SendTo<T>(long playerID, T proto)
        {
        }

        public void SendTo(long sessionID, byte[] data)
        {
        }
    }
}
