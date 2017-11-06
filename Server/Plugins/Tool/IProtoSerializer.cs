using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    public interface IProtoSerializer
    {
        object DeserializeObj(byte[] data);
        byte[] Serialize<T>(T proto);
    }
}
