using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace L.Util
{
    class PortSelector
    {
        public static List<int> GetUsedPorts()
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            var allPorts = new List<int>();
            allPorts.AddRange(ipGlobalProperties.GetActiveTcpListeners().Select(t => t.Port));
            allPorts.AddRange(ipGlobalProperties.GetActiveUdpListeners().Select(t => t.Port));
            allPorts.AddRange(ipGlobalProperties.GetActiveTcpConnections().Select(t => t.LocalEndPoint.Port));

            return allPorts.Distinct().ToList();
        }

        public static int GetDefaultPort(int startPortValue)
        {
            var usedPorts = GetUsedPorts();
            while (usedPorts.Any(t => t == startPortValue)) startPortValue++;

            return startPortValue;
        }
    }
}
