using Newtonsoft.Json;
using Shareson.Model;
using Shareson.Repository.SupportMethods;
using Shareson.Support.ClientHelper;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shareson.Repository
{
    public class AccountControlRepository
    {
        private string Email;
        private string Password;
        private string jsonToSend;

        ClientHelper client;

        public AccountControlRepository()
        {
            client = new ClientHelper();
        }

        public async Task<bool> ConnectToAccount(string Email, string Password)
        {
            bool ConnectedToServer;
            this.Email = Email;
            this.Password = Password;

            var ConnectionTask = Task.Factory.StartNew(() => client.ConnectToServer());
            ConnectedToServer = await ConnectionTask;

            if (ConnectedToServer == true)
            {
                jsonToSend = Support.RequestConstructor.CreateConnectionToAccountRequestAsJson(Enum.AvailableMethodsOnServer.LoginToAccount, Email, Password);
                client.Send(ClientHelperModel.clientSocket, jsonToSend);
                client.Receive(ClientHelperModel.clientSocket);

                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
