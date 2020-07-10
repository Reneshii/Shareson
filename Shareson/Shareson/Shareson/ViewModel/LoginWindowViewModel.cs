using Shareson.Model;
using Shareson.Repository;
using Shareson.Support;
using System;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class LoginWindowViewModel : Property_Changed
    {
        private LoginWindowModel model;
        private LoginWindowRepository repository;
        private Action closeLoginWindow;

        public object LoginWindowContentControl
        {
            get
            {
                return model._LoginWindowContentControl;
            }
            set
            {
                model._LoginWindowContentControl = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand CallAccountPage
        {
            get
            {
                if(model._CallAccountPage == null)
                {
                    model._CallAccountPage = new RelayCommand(f => true, f =>
                    {
                        LoginWindowContentControl = new AccountControlViewModel(closeLoginWindow);
                    });
                }
                return model._CallAccountPage;
            }
            set
            {
                model._CallAccountPage = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand CallOptionsPage
        {
            get
            {
                if(model._CallOptionsPage == null)
                {
                    model._CallOptionsPage = new RelayCommand(f => true, f =>
                    {
                        LoginWindowContentControl = new OptionsControlViewModel();
                    });
                }
                return model._CallOptionsPage;
            }
            set
            {
                model._CallOptionsPage = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand CallContactPage
        {
            get
            {
                if(model._CallContactPage == null)
                {
                    model._CallContactPage = new RelayCommand(f => true, f =>
                    {
                        LoginWindowContentControl = new ContactControlViewModel();
                    });
                }
                return model._CallContactPage;
            }
            set
            {
                model._CallContactPage = value;
                NotifyPropertyChanged();
            }
        }

        public LoginWindowViewModel(Action closeLoginWindow)
        {
            this.closeLoginWindow = closeLoginWindow;
            Initialize();
        }
        
        private void Initialize()
        {
            model = new LoginWindowModel();
            repository = new LoginWindowRepository();
            LoginWindowContentControl = new AccountControlViewModel(closeLoginWindow);
        }
    }
}
