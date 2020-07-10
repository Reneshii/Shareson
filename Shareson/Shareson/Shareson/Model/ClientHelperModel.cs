using System.Net;
using System.Net.Sockets;

namespace Shareson.Model
{
    public class ClientHelperModel
    {
        public static Socket clientSocket { get; set; }

        public bool Connected = false;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;
        public IPEndPoint remoteEP;
        
        public int PORT = Properties.Settings.Default.Port /*11000*/;
        public string DNSorIP = Properties.Settings.Default.DNSorIP;

        public const int MaxBufferSize = 5000000;
        public byte[] buffer = new byte[MaxBufferSize];
        public byte[] receivedBytes;
    }
}
