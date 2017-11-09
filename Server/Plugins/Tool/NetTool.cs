using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Linq;

namespace Plugins
{
    public class NetTool
    {
        public static int GetAvailablePort(int startPort, int checkLength = 100)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var ipEndPoints = ipProperties.GetActiveTcpListeners().ToList();
            for (int i = 0; i < checkLength; i++)
            {
                int curPort = startPort + i;
                if (ipEndPoints.All(a => a.Port != curPort))
                {
                    return curPort;
                }
            }
            throw new Exception("没用可用的端口From {0} To {1} ".FormatStr(startPort, startPort + 100));
        }
    }
}
