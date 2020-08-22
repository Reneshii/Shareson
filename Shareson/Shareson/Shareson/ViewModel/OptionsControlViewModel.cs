using Shareson.Model;
using Shareson.Repository;
using Shareson.Support;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class OptionsControlViewModel : Property_Changed
    {
        private OptionsRepository repository;
        private OptionsModel model;

        #region Command
        public ICommand SaveSettings
        {
            get
            {
                if (model._SaveSettings == null)
                {
                    model._SaveSettings = new RelayCommand(f => true, f =>
                    {
                        repository.SaveSettings(model);
                    });
                }
                return model._SaveSettings;
            }
            set { }
        }
        #endregion

        #region Properties
        public string PathToServerFolder
        {
            get
            {
                return model._PathToServerFolder;
            }
            set
            {
                if (value.Contains("/"))
                {
                    value = value.Replace("/", @"\");
                }

                var lastChar = value.LastIndexOf(@"\");
                if (lastChar > 0 && lastChar < value.Length && !value.EndsWith(@"\"))
                {
                    value += @"\";
                }

                model._PathToServerFolder = value;
                NotifyPropertyChanged();
            }
        }
        public int Port
        {
            get
            {
                return model._Port;
            }
            set
            {
                model._Port = value;
                NotifyPropertyChanged();
            }
        }
        public string DNSorIP
        {
            get
            {
                return model._DNSorIP;
            }
            set
            {
                model._DNSorIP = value;
                NotifyPropertyChanged();
            }
        }
        public string LogsFilePath
        {
            get
            {
                return model._LogsFilePath;
            }
            set
            {
                model._LogsFilePath = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Excluded> ExcludedExtensionsList
        {
            get
            {
                model._ExcludedExtensionsList = repository.RefreshList(model._ExcludedExtensionsList);
                return model._ExcludedExtensionsList;
            }
            set
            {
                model._ExcludedExtensionsList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public OptionsControlViewModel()
        {
            Initialize();
            InitialExecute();
        }
        public OptionsModel ReloadSettings()
        {
            return repository.LoadSettings();
        }
        private void Initialize()
        {
            repository = new OptionsRepository();
            model = new OptionsModel();
        }
        private void InitialExecute()
        {
            model = repository.LoadSettings();
        }
    }
}
