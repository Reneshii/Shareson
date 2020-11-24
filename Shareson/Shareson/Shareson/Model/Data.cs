using Shareson.Support;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Media.Imaging;

namespace Shareson.Model
{
    public class Data
    {
        public class AccountModel
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string ID { get; set; }
            public string Message { get; set; }
            public string[] AccessedDirectory { get; set; }
        }

        public class ClientHelperModel
        {
            public static Socket clientSocket { get; set; }
            public static bool IsServerRun { get; set; }
            public static bool ConnectionMode { get; set; }

            public IPHostEntry ipHostInfo;
            public IPAddress ipAddress;
            public IPEndPoint remoteEP;

            public int PORT = Properties.Settings.Default.Port;
            public string DNSorIP = Properties.Settings.Default.DNSorIP;

            public const int MaxBufferSize = 10000000;
            public byte[] buffer = new byte[MaxBufferSize];
            public byte[] receivedBytes;
        }

        public class FileInfoModel : Property_Changed
        {
            public string Name { get; set; }
            public long Size { get; set; }
            public float SizeInBytes { get; set; }
            public float SizeInMegabytes { get; set; }
            public string Creator { get; set; }
            public string CreationTime { get; set; }
            public BitmapImage BMapImage { get; set; }
            public byte[] Image { get; set; }
            public string Path { get; set; }
        }

        public class ResourcesFromServer
        {
            public List<FoldersAndFiles> FoldersAndFiles = new List<FoldersAndFiles>();
            public List<FoldersAndFiles> UsedFiles = new List<FoldersAndFiles>();
        }

        public class FoldersAndFiles
        {
            public string DirectoryPath;
            public string[] Files;
        }

        //public class ReceivedFileInfoModel
        //{
        //    public string Name { get; set; }
        //    public long Size { get; set; }
        //    public string Creator { get; set; }
        //    public string CreationTime { get; set; }
        //    public string Folder { get; set; }
        //    public byte[] Image { get; set; }
        //}
    }
}
