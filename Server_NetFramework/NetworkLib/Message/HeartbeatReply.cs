using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLib
{
    public class HeartbeatReply
    {
        public const ushort PROTOCOL_NUM = ushort.MaxValue - 99;
        public static HeartbeatReply Parse(byte[] data)
        {
            var num = BitConverter.ToInt32(data, 0);
            HeartbeatReply msg = new HeartbeatReply();
            msg.number = num;
            return msg;
        }
        public byte[] Serialize()
        {
            byte[] data = new byte[6];
            Array.Copy(BitConverter.GetBytes(PROTOCOL_NUM), data, 2);
            Array.Copy(BitConverter.GetBytes(number), 0, data, 2, 4);
            return data;
        }

        public int number { get; set; }
    }
}
