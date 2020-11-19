using Shareson.Model;
using Shareson.Support;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class MainWindowViewModel : Property_Changed
    {
        MainWindowModel model;
        OptionsModel optionsModel;
        Data.AccountModel AccountModel;

        OptionsControlViewModel OptionsControlViewModel;
        ImagesControlViewModel imagesViewModel;
        AccountOptionsControlViewModel accountOptionsViewModel;
        
        public ICommand CallImagesPage
        {
            get
            {
                if(model._CallImagesPage == null)
                {
                    model._CallImagesPage = new RelayCommand(f => true, f => 
                    {
                        optionsModel = OptionsControlViewModel.ReloadSettings();
                        imagesViewModel.ReloadSettings(optionsModel);
                        MainWindowContentControl = imagesViewModel;
                    });
                }
                return model._CallImagesPage;
            }
            set{ }
        }
        public ICommand CallOptionsPage
        {
            get
            {
                if(model._CallOptionsPage == null)
                {
                    model._CallOptionsPage = new RelayCommand(f => true, f =>
                    {
                        OptionsControlViewModel = new OptionsControlViewModel();
                        OptionsControlViewModel.ReloadSettings();
                        MainWindowContentControl = OptionsControlViewModel;
                    });
                }
                return model._CallOptionsPage;
            }
            set { }
        }
        public ICommand CallContactPage
        {
            get
            {
                if(model._CallContactPage == null)
                {
                    model._CallContactPage = new RelayCommand(f => true, f =>
                    {
                        MainWindowContentControl = new ContactControlViewModel();
                    });
                }
                return model._CallContactPage;
            }
            set { }
        }
        public ICommand CallAccountOptionsPage
        {
            get
            {
                if(model._CallAccountOptionsPage == null)
                {
                    model._CallAccountOptionsPage = new RelayCommand(f => true, f =>
                    {
                        MainWindowContentControl = new AccountOptionsControlViewModel();
                    });
                }
                return model._CallAccountOptionsPage;
            }
            set { }
        }
        public object MainWindowContentControl
        {
            get
            {
                return model._MainWindowContentControl;
            }
            set
            {
                model._MainWindowContentControl = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel(Data.AccountModel accountModel = null)
        {
            AccountModel = accountModel;

            Initialize();
            
        }
        public void Initialize()
        {
            model = new MainWindowModel();
            optionsModel = new OptionsModel();
            OptionsControlViewModel = new OptionsControlViewModel();
            imagesViewModel = new ImagesControlViewModel(AccountModel);
            accountOptionsViewModel = new AccountOptionsControlViewModel();
        }
    }
}