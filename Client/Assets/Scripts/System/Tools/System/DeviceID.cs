using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
 
using System.Net.Sockets;
using System;
 namespace RedStone
{
    public class DeviceID
    {
        private string _uuid = "";
        private string _macAddress = "";

        private static DeviceID s_instance;

        public static DeviceID Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new DeviceID();
                return s_instance;
            }
        }

        private DeviceID()
        {

        }

        //"192.168.1.1&&ipv4"
        public static string GetIPv6(string mHost, string mPort)
        {
#if UNITY_IPHONE && !UNITY_EDITOR
		string mIPv6 = getIPv6(mHost, mPort);
		return mIPv6;
#else
            return mHost + "&&ipv4";
#endif
        }

        public void getIPType(string serverIp, string serverPorts, out string newServerIp, out AddressFamily mIPType)
        {
            mIPType = AddressFamily.InterNetwork;
            newServerIp = serverIp;
            try
            {
                string mIPv6 = GetIPv6(serverIp, serverPorts);
                if (!string.IsNullOrEmpty(mIPv6))
                {
                    string[] m_StrTemp = System.Text.RegularExpressions.Regex.Split(mIPv6, "&&");
                    if (m_StrTemp != null && m_StrTemp.Length >= 2)
                    {
                        string IPType = m_StrTemp[1];
                        if (IPType == "ipv6")
                        {
                            newServerIp = m_StrTemp[0];
                            mIPType = AddressFamily.InterNetworkV6;
                        }
                    }
                }
            }
            catch (Exception e)
            {
				Logger.LogError(this, "GetIPv6 error:{0}", e);
            }

        }

        public void Init()
        {
            LoadUUID();
            LoadMacAddress();
        }

        public void LoadUUID()
        {
            getUUID();
        }

        public void LoadMacAddress()
        {
            getMacAddress();
        }
#if UNITY_EDITOR
        private void getUUID()
        {
			_uuid = "19AAB430-9CB8-4325-ACC5-D7D386B68960" + GetMacAddress ();

		}

        private void getMacAddress()
        {
            _macAddress = GetMacAddress();
        }

        public string GetMacAddress()
        {
            string physicalAddress = "";

            NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adaper in nice)
            {
                if (adaper.Description == "en0")
                {
                    physicalAddress = adaper.GetPhysicalAddress().ToString();
                    break;
                }
                else
                {
                    physicalAddress = adaper.GetPhysicalAddress().ToString();

                    if (physicalAddress != "")
                    {
                        break;
                    };
                }
            }

            return physicalAddress;
        }
#elif UNITY_IOS
    [DllImport("__Internal")]
    public static extern void getUUID ();

    [DllImport("__Internal")]
    public static extern void getMacAddress ();

    [DllImport("__Internal")]
    private static extern string getIPv6(string mHost, string mPort);
#elif UNITY_ANDROID
        public void getUUID()
    {
        SendMessage("getUUID");
    }

    public void getMacAddress()
    {
        SendMessage("getMacAddress");
    }

    private void SendMessage(string methodname, params object[] args)
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.CallStatic(methodname, args);
            }
        }
    }
#else
        private void getUUID()
    {
        _uuid = "19AAB430-9CB8-4325-ACC5-D7D386B68960" + GetMacAddress();//编辑器没有UUID。用一个网上的UUID + 本机mac地址代替。
    }

    private void getMacAddress()
    {
        _macAddress = GetMacAddress();
    }

    public string GetMacAddress()
    {
        string physicalAddress = "";

        NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface adaper in nice)
        {
            if (adaper.Description == "en0")
            {
                physicalAddress = adaper.GetPhysicalAddress().ToString();
                break;
            }
            else
            {
                physicalAddress = adaper.GetPhysicalAddress().ToString();

                if (physicalAddress != "")
                {
                    break;
                };
            }
        }

        return physicalAddress;
    }
#endif
        public void ReciveUUID(string uuid)
        {
            Debug.Log("uuid=" + uuid);
            _uuid = uuid;
        }

        public void ReciveMacAddress(string macaddress)
        {
            Debug.Log("macaddress=" + macaddress);
            _macAddress = macaddress;
        }

        public static string UUID
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new DeviceID();
                }
                return s_instance._uuid;
            }
        }
    }
}