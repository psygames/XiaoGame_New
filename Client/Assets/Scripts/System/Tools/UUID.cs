using System;
using UnityEngine;
namespace RedStone
{
	public static class UUID
	{
		private static string m_device = null;
		public static string DEVICE
		{
			get
			{
				if (m_device == null)
					m_device = GetDevice();
				return m_device;
			}
		}

		private static string GetDevice()
		{
			if (USE_RANDOM_DEVICE)
				return "RANDOM-" + UnityEngine.Random.Range(0, int.MaxValue);
			else
				return SystemInfo.deviceUniqueIdentifier;
		}

		public static bool USE_RANDOM_DEVICE = false;
	}
}
