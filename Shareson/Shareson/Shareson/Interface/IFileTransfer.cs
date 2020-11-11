using Shareson.Model;
using System.Collections.ObjectModel;
using System.Net.Sockets;

namespace Shareson.Interface
{
    interface IFileTransfer
    {
        ObservableCollection<Data.FileInfoModel> GetFile(Socket clientSocket, string searchingFile, int repeat = 1);
        void PutFile(string name);
    }
}
