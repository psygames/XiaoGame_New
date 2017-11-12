using System;
namespace RedStone
{
	public class BitConvert
	{
		public static byte[] ToBytes(ushort num)
		{
			byte[] bytes = new byte[2];
			for (int i = 0; i < 2; i++)
			{
				bytes[i] = (byte)(num >> i * 8 & 0xff);
			}
			return bytes;
		}

		public static ushort ToUshort(byte[] bytes)
		{
			ushort num = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				num += (ushort)(bytes[i] << i * 8);
			}
			return num;
		}
	}
}
