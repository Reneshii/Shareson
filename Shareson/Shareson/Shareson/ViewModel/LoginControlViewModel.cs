using Shareson.Model;
using Shareson.Repository;
using Shareson.Support;
using Shareson.View;
using System;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class LoginControlViewModel : Property_Changed
    {
        LoginControlRepository repository;
        LoginControlModel model;
        Action closeLoginWindow;

        #region Command
        public ICommand LogInBtn
        {
            get
            {
                if(model._LogInBtn == null)
                {
                    model._LogInBtn = new RelayCommand(f => LogInEnable, async f =>
                    {
                        IsServerOn = Data.ClientHelperModel.IsServerRun;
                        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && IsServerOn == true)
                        {
                            var accModel = await repository.requests.ConnectToAccount(Email, Password);
                            ClientHelper.ContinueTestConnection = false;
                            MainWindow mainWindow = new MainWindow(accModel);
                            mainWindow.Show();
                            closeLoginWindow.Invoke();
                        }
                        else
                        {

                        }
                    });
                }
                return model._LogInBtn;
            }
            set
            {
                model._LogInBtn = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Properties
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
        public bool LogInEnable
        {
            get
            {
                return model._LogInEnable = true;
            }
            set
            {
                model._LogInEnable = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsServerOn
        {
            get
            {
                return model._IsServerOn;
            }
            set
            {
                model._IsServerOn = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public LoginControlViewModel(Action closeLoginWindow)
        {
            Initialize();
            this.closeLoginWindow = closeLoginWindow;
        }

        private void Initialize()
        {
            repository = new LoginControlRepository();
            model = new LoginControlModel();
        }
    }
}
