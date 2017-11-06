using System;
using System.Text;

namespace Coolfish.System
{
	public static class StringTools
	{

		private static ObjectPool<StringBuilder> s_builderPool = new ObjectPool<StringBuilder>(null, (s)=>s.Length = 0);
		public static string FormatStr(this string str, params object[] p)
		{
			if (str == null || p == null)
				return "";
			var stringBuilder = s_builderPool.Get ();
			stringBuilder.Length = 0;
			stringBuilder.AppendFormat (str, p);
			var ret = stringBuilder.ToString ();
			s_builderPool.Release (stringBuilder);
			return ret;
		}

		public static StringBuilder GetStringBuilderFromPool()
		{
			return s_builderPool.Get ();
		}
		public static void ReleaseStringBuilder(StringBuilder stringBuilder)
		{
			s_builderPool.Release (stringBuilder);
		}
	}
}

