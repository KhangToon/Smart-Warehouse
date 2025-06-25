using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;

namespace Smart_Warehouse.Services
{
    public static class PortFinder
    {
        // Find an available port
        public static int FindAvailablePort_notcheckUsed(int startingPort = 5000)
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var usedPorts = properties.GetActiveTcpListeners().Select(l => l.Port).ToList();

            for (int port = startingPort; port < 65535; port++)
            {
                if (!usedPorts.Contains(port))
                {
                    return port;
                }
            }

            throw new Exception("No available ports found.");
        }

        public static int FindAvailablePort_CheckUsed(IPAddress? targetIP = null, int startingPort = 5000)
        {
            if (startingPort < 1 || startingPort > 65535)
                throw new ArgumentOutOfRangeException(nameof(startingPort), "Port must be between 1 and 65535");

            for (int port = startingPort; port <= 65535; port++)
            {
                if (IsPortAvailable(targetIP, port))
                    return port;
            }

            throw new InvalidOperationException("No available ports found in range.");
        }

        private static bool IsPortAvailable(IPAddress? targetIP, int port)
        {
            // If targetIP is null, use IPAddress.Any
            var ipAddress = targetIP ?? IPAddress.Any;

            var tcpListener = new TcpListener(ipAddress, port);

            try
            {
                tcpListener.Start();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                tcpListener.Stop();
            }
        }

        // enable khi deploy - disable khi use local 
        // Get local IP address
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
