using Shareson.Model;
using Shareson.Model.ForViews;
using Shareson.Support;
using Shareson.Support.ClientHelper;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class CreateAccountControl : Property_Changed
    {
        AccountModel accModel;
        CreateAccountModel model;
        ClientHelper clientHelper;
        

        #region ICommand
        public ICommand CreateAccount
        {
            get
            {
                if(model._CreateAccount == null)
                {
                    model._CreateAccount = new RelayCommand(f => true, f =>
                    {
                        var request = RequestConstructor.CreateConnectionToAccountRequestAsJson(Enum.AvailableMethodsOnServer.CreateAccount, Email, Login, Password);
                        clientHelper.Send(ClientHelperModel.clientSocket,request);
                    });
                }
                return model._CreateAccount;
            }
            set { }
        }
        #endregion

        #region Properties
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

        public CreateAccountControl()
        {
            accModel = new AccountModel();
            model = new CreateAccountModel();
            clientHelper = new ClientHelper();
            clientHelper.ConnectToServer();
        }
    }
}
