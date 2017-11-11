using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    public interface IProtoSerializer
    {
        Google.Protobuf.IMessage Deserialize(byte[] data);
        byte[] Serialize(Google.Protobuf.IMessage proto);
    }
}
