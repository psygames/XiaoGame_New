using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Linq;
using System.Collections;

namespace NetworkLib
{
    public class NetTool
    {
        //TODO: MAC 平台下，获取占用端口不全。
        public static int GetAvailablePort(int startPort, int checkLength = 100)
        {
            // 获取本地计算机的网络连接和通信统计数据的信息
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            //返回本地计算机上的所有Tcp监听程序
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
            //返回本地计算机上的所有UDP监听程序
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            List<int> allPorts = new List<int>();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            for (int i = 0; i < checkLength; i++)
            {
                int curPort = startPort + i;
                if (allPorts.All(a => a != curPort))
                {
                    return curPort;
                }
            }
            throw new Exception("没用可用的端口From {0} To {1} ".FormatStr(startPort, startPort + 100));
        }

        public static string GetIP(string address)
        {
            int begin = address.IndexOf("://") + 3;
            if (begin < 3) begin = 0;
            int end = address.IndexOf(":", begin);
            if (end <= begin)
                return null;
            return address.Substring(begin, end - begin);
        }

        public static int GetPort(string address)
        {
            int begin = address.LastIndexOf(":") + 1;
            if (begin <= 0)
                return -1;
            string portStr = address.Substring(begin);
            int port = -1;
            if (int.TryParse(portStr, out port))
                return port;
            return -1;
        }

        public static string GetAddress(string ip, int port)
        {
            return "ws://{0}:{1}".FormatStr(ip, port);
        }

        public static string GetLocalIPV4()
        {
            string hostname = Dns.GetHostName();//得到本机名      
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            IPAddress localaddr = localhost.AddressList[1];
            return localaddr.ToString();
        }
    }
}
