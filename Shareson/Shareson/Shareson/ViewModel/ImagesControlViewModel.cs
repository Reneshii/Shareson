using Shareson.Support;
using Shareson.Model;
using System.Windows.Input;
using Shareson.Repository;
using Shareson.Interface;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace Shareson.ViewModel
{
    public class ImagesControlViewModel : Property_Changed
    {
        ImagesControlModel model;
        Data.AccountModel AccountModel;
        OptionsModel optionsModel;
        ObservableCollectionControlViewModel observableCollectionViewModel;
        ImageInfoControlViewModel ImageInfoViewModel;

        ISocketOnly repositoryOnlySocket;

        #region Fields
        public ICommand DisplaySingleRandom
        {
            get
            {
                if (model._DisplaySingleRandom == null)
                {
                    model._DisplaySingleRandom = new RelayCommand(f => ConnectedToServer, async f =>
                    {
                        List<string> excluded = new List<string>();
                        IEnumerable<Excluded> checkedBox = optionsModel._ExcludedExtensionsList.Where(w => w.CheckedBox == true);
                        foreach (var item in checkedBox)
                        {
                            excluded.Add(item.ExcludedExtension);
                        }
                        observableCollectionViewModel.OCImagesSource = await repositoryOnlySocket.GetRandomImage(DirectoryPath, 1, AccountModel, excluded.ToArray());
                        ItemsControlViewModelContentControl = observableCollectionViewModel;
                    });
                }
                return model._DisplaySingleRandom;
            }
            set { }
        }
        public ICommand DisplayMulti
        {
            get
            {
                if (model._DisplayMulti == null)
                {
                    model._DisplayMulti = new RelayCommand(f => ConnectedToServer && ImagesLimit > 0, async f =>
                    {
                        List<string> excluded = new List<string>();
                        IEnumerable<Excluded> checkedBox = optionsModel._ExcludedExtensionsList.Where(w => w.CheckedBox == true);
                        foreach (var item in checkedBox)
                        {
                            excluded.Add(item.ExcludedExtension);
                        }
                        observableCollectionViewModel.OCImagesSource = await repositoryOnlySocket.GetRandomImage(DirectoryPath, ImagesLimit, AccountModel, excluded.ToArray());
                        ItemsControlViewModelContentControl = observableCollectionViewModel;
                    });
                }
                return model._DisplayMulti;
            }
            set { }
        }
        public ICommand DisplaySingle
        {
            get
            {
                if (model._DisplaySingle == null)
                {
                    model._DisplaySingle = new RelayCommand(f => ConnectedToServer, async f =>
                    {
                        List<string> excluded = new List<string>();
                        IEnumerable<Excluded> checkedBox = optionsModel._ExcludedExtensionsList.Where(w => w.CheckedBox == true);
                        foreach (var item in checkedBox)
                        {
                            excluded.Add(item.ExcludedExtension);
                        }
                        observableCollectionViewModel.OCImagesSource = await repositoryOnlySocket.GetSingleImage(DirectoryPath, FileName, AccountModel, excluded.ToArray());
                        ItemsControlViewModelContentControl = observableCollectionViewModel;
                    });
                }
                return model._DisplaySingle;
            }
            set { }
        }
        public Visibility Loading
        {
            get
            {
                return model._Loading;
            }
            set
            {
                model._Loading = value;
                NotifyPropertyChanged();
            }
        }
        public Visibility ItemsControlVisability
        {
            get
            {
                return model._Loading;
            }
            set
            {
                model._Loading = value;
                NotifyPropertyChanged();
            }
        }
        public bool ConnectedToServer
        {
            get
            {
                return model._ConnectedToServer;
            }
            set
            {
                model._ConnectedToServer = value;
                NotifyPropertyChanged();
            }
        }
        public string[] DirectoriesPath
        {
            get
            {
                return model._DirectoriesPath;
            }
            set
            {
                model._DirectoriesPath = value;
                NotifyPropertyChanged();
            }
        }
        public string DirectoryPath
        {
            get
            {
                return model._DirectoryPath;
            }
            set
            {
                model._DirectoryPath = value;
                NotifyPropertyChanged();
            }
        }
        public string FileName
        {
            get
            {
                return model._FileName;
            }
            set
            {
                model._FileName = value;
                NotifyPropertyChanged();
            }

        }
        public int ImagesLimit
        {
            get
            {
                return model._ImagesLimit;
            }
            set
            {
                model._ImagesLimit = value;
                NotifyPropertyChanged();
            }
        }
        public object ItemsControlViewModelContentControl
        {
            get
            {
                return model._ItemsControlViewModelContentControl;
            }
            set
            {
                model._ItemsControlViewModelContentControl = value;
                NotifyPropertyChanged();
            }
        }
        Data.FileInfoModel InfoModel
        {
            get
            {
                return model._FileInfoModel;
            }
            set
            {
                model._FileInfoModel = value;
                NotifyPropertyChanged();

                ImageInfoViewModel = new ImageInfoControlViewModel(model._FileInfoModel);
                ItemsControlViewModelContentControl = ImageInfoViewModel;
            }
        }
        #endregion

        #region MainMethods
        public ImagesControlViewModel(Data.AccountModel accountModel)
        {
            Initialize();
            AccountModel = accountModel;
            DirectoriesPath = AccountModel.AccessedDirectory;
        }
        private void Initialize()
        {
            model = new ImagesControlModel();
            AccountModel = new Data.AccountModel();
            repositoryOnlySocket = new ImagesControlRepository();
            observableCollectionViewModel = new ObservableCollectionControlViewModel();
            ConnectedToServer = true;

            observableCollectionViewModel.GetImageInfoWhileClick_Event += () =>
            {
                InfoModel = observableCollectionViewModel.InfoModelFor_ImagesControlViewModel;
            };
        }
        #endregion

        #region Methods
        public void ReloadSettings(OptionsModel model)
        {
            //DirectoryPath = model._PathToServerFolder;
            this.optionsModel = model;
        }

        #endregion
    }
}
