using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkLib
{
    public interface ISerializer
    {
        void Init();
        object Deserialize(byte[] data);
        byte[] Serialize(object proto);
    }
}
