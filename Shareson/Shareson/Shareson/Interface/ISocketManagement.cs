using System.Net.Sockets;

namespace Shareson.Interface
{
    interface ISocketManagement
    {
        bool ConnectToServer();
        void Send(Socket client, string data);
        void Receive(Socket client);
    }
}
