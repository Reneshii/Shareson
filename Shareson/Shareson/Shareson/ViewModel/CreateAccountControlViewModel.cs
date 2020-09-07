using Shareson.Model;
using Shareson.Model.ForViews;
using Shareson.Repository;
using Shareson.Support;
using Shareson.Support.ClientHelper;
using System.Text;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class CreateAccountControlViewModel : Property_Changed
    {
        AccountModel accModel;
        CreateAccountModel model;
        ClientHelper clientHelper;
        CreateAccountControlRepository repository;

        #region ICommand
        public ICommand CreateAccount
        {
            get
            {
                if(model._CreateAccount == null)
                {
                    model._CreateAccount = new RelayCommand(f => true, f =>
                    {
                        clientHelper = new ClientHelper();
                        clientHelper.ConnectToServer();
                        var request = RequestConstructor.CreateAccountRequestAsJson(Enum.AvailableMethodsOnServer.CreateAccount, Email, Login, Password);
                        clientHelper.Send(ClientHelperModel.clientSocket, request);
                        clientHelper.Receive(ClientHelperModel.clientSocket);
                        string received = Encoding.ASCII.GetString(clientHelper.model.receivedBytes);
                    });
                }
                return model._CreateAccount;
            }
            set { }
        }
        #endregion

        #region Properties
        public string ErrorLogin
        {
            get
            {
                return model._ErrorLogin;
            }
            set
            {
                model._ErrorLogin = value;
                NotifyPropertyChanged();
            }
        }
        public string ErrorPassword
        {
            get
            {
                return model._ErrorPassword;
            }
            set
            {
                model._ErrorPassword = value;
                NotifyPropertyChanged();
            }
        }
        public string ErrorEmail
        {
            get
            {
                return model._ErrorEmail;
            }
            set
            {
                model._ErrorEmail = value;
                NotifyPropertyChanged();
            }
        }
        public string Login
        {
            get
            {
                return model._Login;
            }
            set
            {
                model._Login = value;
                NotifyPropertyChanged();
            }
        }
        public string Email
        {
            get
            {
                return model._Email;
            }
            set
            {
                model._Email = value;
                NotifyPropertyChanged();
            }
        }
        public string Password
        {
            get
            {
                return model._Password;
            }
            set
            {
                model._Password = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public CreateAccountControlViewModel()
        {
            accModel = new AccountModel();
            model = new CreateAccountModel();
            repository = new CreateAccountControlRepository();
        }
    }
}
