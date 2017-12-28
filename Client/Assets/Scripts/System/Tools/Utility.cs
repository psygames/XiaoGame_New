using System;
using UnityEngine;
using Message;

namespace RedStone
{
    public class Utility
    {
		public static T ToEnum<T> (string name, T defaultValue, bool ignoreCase = false)
		{
			T t = defaultValue;
			try{
				t = (T)Enum.Parse(typeof(T), name, ignoreCase);
			}
			catch(ArgumentException e)
			{
				return defaultValue;
			}
			return t;
		}
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}
		static void WriteField(object obj, Type type, string field, object value)
		{
			var info = type.GetProperty(field);
			if (info != null) {
				info.SetValue (obj, value, null);
			}
		}
		public static void WriteDeviceInfo(object obj)
		{
			var type = obj.GetType ();
			WriteField (obj, type, "category", Application.platform.ToString());
			WriteField (obj, type, "device_unique_identifier", SystemInfo.deviceUniqueIdentifier);
			WriteField (obj, type, "system_time_zone", TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours);
			WriteField (obj, type, "system_language", Application.systemLanguage.ToString());
			WriteField (obj, type, "device_name", SystemInfo.deviceName);
			WriteField (obj, type, "device_model", SystemInfo.deviceModel);
			WriteField (obj, type, "operating_system", SystemInfo.operatingSystem);
			WriteField (obj, type, "processor_type", SystemInfo.processorType);
			WriteField (obj, type, "processor_frequency", SystemInfo.processorFrequency);
			WriteField (obj, type, "processor_count", SystemInfo.processorCount);
			WriteField (obj, type, "system_memory_size", SystemInfo.systemMemorySize);
			WriteField (obj, type, "graphics_device_name", SystemInfo.graphicsDeviceName);
			WriteField (obj, type, "graphics_memory_size", SystemInfo.graphicsMemorySize);
			WriteField (obj, type, "graphics_device_vendor", SystemInfo.graphicsDeviceVendor);
			WriteField (obj, type, "graphics_device_type", SystemInfo.graphicsDeviceType.ToString());
			WriteField (obj, type, "graphics_device_version", SystemInfo.graphicsDeviceVersion);
			WriteField (obj, type, "graphics_shader_level", SystemInfo.graphicsShaderLevel);
			WriteField (obj, type, "screen_resolution", Screen.currentResolution.ToString());
			WriteField (obj, type, "screen_dpi", Screen.dpi);
		}
        /*
        public static void CopyNetVector3(ref Vector3 vec, Vector3Msg msg)
        {
			if (msg != null) {
				vec.x = msg.x;
				vec.y = msg.y;
				vec.z = msg.z;
			}
        }
		public static Vector3 ToVec3(Vector3Msg msg)
		{
			var vec = new Vector3 ();
			CopyNetVector3 (ref vec, msg);
			return vec;
		}
		public static Vector2 ToVec2(Vector2Msg msg)
		{
			var vec2 = new Vector2 ();
			CopyNetVector2 (ref vec2, msg);
			return vec2;
		}
        public static void CopyNetVector2(ref Vector2 vec, Vector2Msg msg)
		{
			if (msg != null) 
			{
				vec.x = msg.x;
				vec.y = msg.y;
			}
        }

        public static  void CopyNetRot(ref Quaternion rot, QuaternionMsg msg)
        {
            rot.x = msg.x;
            rot.y = msg.y;
            rot.z = msg.z;
            rot.w = msg.w;
        }

        public static Vector3Msg ToMessageVector3(Vector3 vec)
        {
			Vector3Msg msgVec = Coolfish.System.RecycleObjectPool<Vector3Msg>.Get();
            msgVec.x = vec.x;
            msgVec.y = vec.y;
            msgVec.z = vec.z;
            return msgVec;
        }

        public static Vector2Msg ToMessageVector2(Vector2 vec)
        {
			Vector2Msg msgVec = Coolfish.System.RecycleObjectPool<Vector2Msg>.Get();
            msgVec.x = vec.x;
            msgVec.y = vec.y;
            return msgVec;
        }

        public static QuaternionMsg ToMessageRotation(Quaternion rot)
        {
			QuaternionMsg msgRot = Coolfish.System.RecycleObjectPool<QuaternionMsg>.Get();
            msgRot.x = rot.x;
            msgRot.y = rot.y;
            msgRot.z = rot.z;
            msgRot.w = rot.w;
            return msgRot;
        }

        public static Quaternion ToRotation(QuaternionMsg rot)
        {
			Quaternion rotation = new Quaternion ();
			if (rot != null)
			{
				rotation.x = rot.x;
				rotation.y = rot.y;
				rotation.z = rot.z;
				rotation.w = rot.w;
			}
            return rotation;
        }
        */
    }
}
