using Shareson.Model;
using Shareson.Repository;
using Shareson.Support;
using Shareson.Support.ClientHelper;
using Shareson.View;
using System;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class AccountControlViewModel : Property_Changed
    {
        AccountControlRepository repository;
        AccountControlModel model;
        Action closeLoginWindow;

        #region Command
        public ICommand LogInBtn
        {
            get
            {
                if(model._LogInBtn == null)
                {
                    model._LogInBtn = new RelayCommand(f => true, async f =>
                    {
                        ClientHelper.ContinueTestConnection = false;
                        await repository.ConnectToAccount(Email, Password);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        closeLoginWindow.Invoke();
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
                return model._LogInEnable;
            }
            set
            {
                model._LogInEnable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public AccountControlViewModel(Action closeLoginWindow)
        {
            Initialize();
            this.closeLoginWindow = closeLoginWindow;
        }

        private void Initialize()
        {
            repository = new AccountControlRepository();
            model = new AccountControlModel();
        }
    }
}
