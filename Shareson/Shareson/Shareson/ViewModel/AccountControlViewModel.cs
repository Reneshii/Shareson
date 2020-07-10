using Shareson.Model;
using Shareson.Repository;
using Shareson.Support;
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

        public ICommand LogInBtn
        {
            get
            {
                if(model._LogInBtn == null)
                {
                    model._LogInBtn = new RelayCommand(f => true, async f =>
                    {
                        //await repository.ConnectToAccount(Email, Password);
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
        private bool Login_Password
        {
            get
            {
                if(!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set { }
        }

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
