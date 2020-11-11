using System.Threading.Tasks;
using Shareson.Model;

namespace Shareson.Support
{
    public class Requests
    {
        private string Email;
        private string Password;
        private string jsonToSend;
        ClientHelper client;

        public Requests(string Email = null, string Password = null, string jsonToSend = null)
        {
            client = new ClientHelper();
            this.Email = Email != null? Email : "";
            this.Password = Password != null? Password : "";
            this.jsonToSend = jsonToSend != null? jsonToSend : "";
        }
        public async Task<Data.AccountModel> ConnectToAccount(string Email, string Password)
        {
            bool ConnectedToServer;
            this.Email = Email;
            this.Password = Password;

            var ConnectionTask = Task.Factory.StartNew(() => client.ConnectToServer());
            ConnectedToServer = await ConnectionTask;

            if (ConnectedToServer == true)
            {
                jsonToSend = Support.RequestConstructor.LoginToAccountRequestAsJson(Enum.AvailableMethodsOnServer.LoginToAccount, Email, "", Password);
                client.Send(Data.ClientHelperModel.clientSocket, jsonToSend);
                client.Receive(Data.ClientHelperModel.clientSocket);

                var received = System.Text.Encoding.ASCII.GetString(client.model.receivedBytes);
                var accModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Data.AccountModel>(received);

                return accModel;
            }
            else
            {
                return new Data.AccountModel();
            }
        }
    }
}
