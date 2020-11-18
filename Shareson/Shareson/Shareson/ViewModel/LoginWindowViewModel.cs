using Shareson.Model;
using Shareson.Repository;
using Shareson.Support;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class LoginWindowViewModel : Property_Changed
    {
        private LoginWindowModel model;
        private LoginWindowRepository repository;
        private Action closeLoginWindow;

        InfoLog log;
        LoginControlViewModel LoginControlViewModel;
        Task Task_UpdateServerStatus;

        #region Command
        public ICommand CallLogInControl
        {
            get
            {
                if(model._CallLogInControl == null)
                {
                    model._CallLogInControl = new RelayCommand(f => true, f =>
                    {
                        //AccountControlViewModel = new AccountControlViewModel(closeLoginWindow); <---- Reset textboxes Login/Password
                        LoginWindowContentControl = LoginControlViewModel;
                    });
                }
                return model._CallLogInControl;
            }
            set { }
        }
        public ICommand CallLimitedOptionsControl
        {
            get
            {
                if(model._CallLimitedOptionsControl == null)
                {
                    model._CallLimitedOptionsControl = new RelayCommand(f => true, f =>
                    {
                        LoginWindowContentControl = new OptionsControlViewModel();
                    });
                }
                return model._CallLimitedOptionsControl;
            }
            set { }
        }
        public ICommand CallContactControl
        {
            get
            {
                if(model._CallContactControl == null)
                {
                    model._CallContactControl = new RelayCommand(f => true, f =>
                    {
                        LoginWindowContentControl = new ContactControlViewModel();
                    });
                }
                return model._CallContactControl;
            }
            set { }
        }
        public ICommand CallCreateAccountControl
        {
            get
            {
                if(model._CallCreateAccountControl == null)
                {
                    model._CallCreateAccountControl = new RelayCommand(f => true, f =>
                    {
                        LoginWindowContentControl = new CreateAccountControlViewModel();
                    });
                }
                return model._CallCreateAccountControl;
            }
            set { }
        }
        #endregion

        #region Properties
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
        //public bool ServerStatus
        //{
        //    get
        //    {
        //        return model._ServerStatus;
        //    }
        //    set
        //    {
        //        model._ServerStatus = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        public bool IsCreateAccountAvailable
        {

            get
            {
                return model._IsCreateAccountAvailable;
            }
            set
            {
                model._IsCreateAccountAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public LoginWindowViewModel(Action closeLoginWindow)
        {
            this.closeLoginWindow = closeLoginWindow;
            Initialize();
        }
        
        private void Initialize()
        {
            ClientHelper.ContinueTestConnection = true;

            model = new LoginWindowModel();
            repository = new LoginWindowRepository();
            LoginControlViewModel = new LoginControlViewModel(closeLoginWindow);
            LoginWindowContentControl = LoginControlViewModel;
            InitializeTasks();
            Update();

            log = new InfoLog(Properties.Settings.Default.LogsFilePath);
        }

        private void InitializeTasks()
        {
            Task_UpdateServerStatus = new Task(() =>
            {
                try
                {
                    do
                    {
                        Data.ClientHelperModel.IsServerRun = ClientHelper.TestConnection().Result;
                        IsCreateAccountAvailable = Data.ClientHelperModel.IsServerRun;

                        if (Data.ClientHelperModel.IsServerRun == true)
                        {
                            if (LoginControlViewModel != null)
                            {
                                LoginControlViewModel.IsServerOn = true;
                                LoginControlViewModel.LogInEnable = true;
                                IsCreateAccountAvailable = true;
                            }
                        }
                        else
                        {
                            if (LoginControlViewModel != null)
                            {
                                LoginControlViewModel.IsServerOn = false;
                                LoginControlViewModel.LogInEnable = false;
                                IsCreateAccountAvailable = false;
                            }
                        }

                        Thread.Sleep(5000);
                    }
                    while (ClientHelper.ContinueTestConnection == true);
                }
                catch(Exception e)
                {
                    log.Add(e.ToString());
                }
                
            });
        }

        private void Update()
        {
            Task_UpdateServerStatus.Start();
        }
    }
}
