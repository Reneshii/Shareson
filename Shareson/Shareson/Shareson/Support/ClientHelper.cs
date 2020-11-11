using Shareson.Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shareson.Support
{
    public class ClientHelper 
    {
        InfoLog errorLog = new InfoLog(Properties.Settings.Default.LogsFilePath);

        public Data.ClientHelperModel model;
        
        ManualResetEvent connectDone = new ManualResetEvent(false);
        ManualResetEvent sendDone = new ManualResetEvent(false);
        ManualResetEvent receiveDone = new ManualResetEvent(false);

        public ClientHelper()
        {
            model = new Data.ClientHelperModel();
        }

        public bool ConnectToServer()
        {
            model.ipHostInfo = Dns.GetHostEntry(model.DNSorIP); 
            model.ipAddress = model.ipHostInfo.AddressList[0];
            model.remoteEP = new IPEndPoint(model.ipAddress, model.PORT);

            Data.ClientHelperModel.clientSocket = new Socket(model.ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //ClientHelperModel.clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            Data.ClientHelperModel.clientSocket.BeginConnect(model.remoteEP, new AsyncCallback(ConnectCallBack), Data.ClientHelperModel.clientSocket);

            connectDone.WaitOne();
            if(Data.ClientHelperModel.clientSocket.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ContinueTestConnection;
        private static Data.ClientHelperModel testModel = new Data.ClientHelperModel();
        public static async Task<bool> TestConnection()
        {
            bool test;
            Socket socket;

            try
            {
                if(ContinueTestConnection == true)
                {
                    testModel.ipHostInfo = Dns.GetHostEntry(testModel.DNSorIP); 
                    testModel.ipAddress = testModel.ipHostInfo.AddressList[0];
                    testModel.remoteEP = new IPEndPoint(testModel.ipAddress, testModel.PORT); 

                    socket = new Socket(testModel.ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    socket.Connect(testModel.remoteEP);

                    if (socket.Connected)
                    {
                        socket.Blocking = false;
                        socket.Disconnect(false);
                        test = true;
                    }
                    else
                    {
                        test = false;
                    }
                    return test;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public void DisconnectFromServer(Socket socket)
        {
            if(socket.Connected)
            {
                socket.Disconnect(false);
            }
            socket.Dispose();
        }

        private void ConnectCallBack(IAsyncResult AR)
        {
            try
            {
                Data.ClientHelperModel.clientSocket = (Socket)AR.AsyncState;
                Data.ClientHelperModel.clientSocket.EndConnect(AR);
                //Socket client = (Socket)AR.AsyncState;
                //client.EndConnect(AR);
#if DEBUG
                //MessageBox.Show("Client connected to : " + client.RemoteEndPoint.ToString());
#endif
#if !DEBUG
                MessageBox.Show("Connected to server");
#endif
                connectDone.Set();
            }
            catch(Exception e)
            {
                connectDone.Set();
            }
        }

        public void Send(Socket client , string data, bool closeSocket = false)
        {
            try
            {
                if(closeSocket == false)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(data);
                    client.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallBack), client);
                    sendDone.WaitOne();
                }
                else
                {
                    client.Disconnect(true);
                    client.Close();
                }

            }
            catch (Exception e)
            {
                ResetAllManualResetEvent();
                errorLog.Add(e.ToString());
            }
        }

        private void SendCallBack(IAsyncResult AR)
        {
            Socket client = (Socket)AR.AsyncState;
            int bytesSent = client.EndSend(AR);
            sendDone.Set();
        }

        public void Receive(Socket client)
        {
            try
            {
                Socket socket = client;
                socket.BeginReceive(model.buffer, 0, Data.ClientHelperModel.MaxBufferSize, 0, new AsyncCallback(ReceiveCallBack), socket);
                receiveDone.WaitOne();
            }
            catch(Exception e)
            {
                ResetAllManualResetEvent();
                errorLog.Add(e.ToString());
            }
        }

        private void ReceiveCallBack(IAsyncResult AR)
        {
            Socket client = (Socket)AR.AsyncState;

            int bytesRead = client.EndReceive(AR);

            if (bytesRead > 0)
            {
                model.receivedBytes = new byte[bytesRead];
                Array.Copy(model.buffer, model.receivedBytes, bytesRead);
            }
            else
            {
                client.BeginReceive(model.receivedBytes, 0, Data.ClientHelperModel.MaxBufferSize, 0, new AsyncCallback(ReceiveCallBack), client);
            }
            receiveDone.Set();
        }

        public void ResetAllManualResetEvent()
        {
            receiveDone.Reset();
            sendDone.Reset();
        }
    }
}
