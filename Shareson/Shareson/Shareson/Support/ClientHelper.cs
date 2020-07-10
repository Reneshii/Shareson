using Shareson.Model;
using Shareson.Repository.SupportMethods;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace Shareson.Support.ClientHelper
{
    public class ClientHelper 
    {
        public ClientHelperModel model;
        Log errorLog = new Log(Properties.Settings.Default.LogsFilePath);

        ManualResetEvent connectDone = new ManualResetEvent(false);
        ManualResetEvent sendDone = new ManualResetEvent(false);
        ManualResetEvent receiveDone = new ManualResetEvent(false);

        public ClientHelper()
        {
            model = new ClientHelperModel();
        }

        public bool ConnectToServer()
        {
            model.ipHostInfo = Dns.GetHostEntry(model.DNSorIP); // Add textBox to add DNS;
            model.ipAddress = model.ipHostInfo.AddressList[0];
            model.remoteEP = new IPEndPoint(model.ipAddress, model.PORT); // Add textBox to add PORT;

            ClientHelperModel.clientSocket = new Socket(model.ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            ClientHelperModel.clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            ClientHelperModel.clientSocket.BeginConnect(model.remoteEP, new AsyncCallback(ConnectCallBack), ClientHelperModel.clientSocket);

            connectDone.WaitOne();
            if(ClientHelperModel.clientSocket.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ConnectCallBack(IAsyncResult AR)
        {
            try
            {
                Socket client = (Socket)AR.AsyncState;
                client.EndConnect(AR);
#if DEBUG
                MessageBox.Show("Client connected to : " + client.RemoteEndPoint.ToString());
#endif
#if !DEBUG
                MessageBox.Show("Connected to server");
#endif
                model.Connected = true;
                connectDone.Set();
            }
            catch(Exception e)
            {
                
                connectDone.Set();
                connectDone.Reset();
                errorLog.Add(e.ToString());
                MessageBox.Show("Server refuse connection. See logs file for more information.");
                return;
            }
        }

        public void Send(Socket client , string data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                client.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallBack), client);
                sendDone.WaitOne();
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
                socket.BeginReceive(model.buffer, 0, ClientHelperModel.MaxBufferSize, 0, new AsyncCallback(ReceiveCallBack), socket);
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
                client.BeginReceive(model.receivedBytes, 0, ClientHelperModel.MaxBufferSize, 0, new AsyncCallback(ReceiveCallBack), client);
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
