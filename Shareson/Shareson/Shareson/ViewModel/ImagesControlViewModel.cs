using Shareson.Support;
using Shareson.Model;
using System.Windows.Input;
using Shareson.Repository;
using System.Collections.ObjectModel;
using Shareson.Interface;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace Shareson.ViewModel
{
    public class ImagesControlViewModel : Property_Changed
    {
        ImagesControlModel model;
        OptionsControlModel optionsModel;

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
                        OCImagesSource = await repositoryOnlySocket.GetRandomImage(DirectoryPath, excluded.ToArray());
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
                        OCImagesSource = await repositoryOnlySocket.GetMultiImage(DirectoryPath, ImagesLimit, excluded.ToArray());
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
                        OCImagesSource = await repositoryOnlySocket.GetSingleImage(DirectoryPath, FileName, excluded.ToArray());
                    });
                }
                return model._DisplaySingle;
            }
            set { }
        }
        public ICommand ConnectToServer
        {
            get
            {
                if (model._ConnectToServer == null)
                {
                    model._ConnectToServer = new RelayCommand(f => !ConnectedToServer, async f => ConnectedToServer = await repositoryOnlySocket.StartConnect());
                }
                return model._ConnectToServer;
            }
            set { }
        }
        //public ICommand DetailsBtn
        //{
        //    get
        //    {
        //        return model._DetailsBtn;
        //    }
        //    set
        //    {
        //        model._DetailsBtn = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public ObservableCollection<ScrollViewerItemsModel> ImagesItems
        //{
        //    get
        //    {
        //        return model._ImagesItems;
        //    }
        //    set
        //    {
        //        model._ImagesItems = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        public ObservableCollection<ScrollViewerItemsModel> OCImagesSource
        {
            get
            {
                return model._OCImagesSource;
            }
            set
            {
                model._OCImagesSource = value;
                NotifyPropertyChanged();
            }
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
        public string DirectoryPath
        {
            get
            {
                if (string.IsNullOrEmpty(model._DirectoryPath))
                {
                    model._DirectoryPath = this.optionsModel._PathToServerFolder;
                }
                return model._DirectoryPath;
            }
            set
            {
                if(value.Contains("/"))
                {
                    value = value.Replace("/", @"\");
                }

                var lastChar = value.LastIndexOf(@"\");
                if(lastChar > 0 && lastChar < value.Length && !value.EndsWith(@"\"))
                {
                    value += @"\";
                }

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
        #endregion

        #region MainMethods
        public ImagesControlViewModel()
        {
            Initialize();
        }
        private void Initialize()
        {
            model = new ImagesControlModel();
            repositoryOnlySocket = new ImagesControlRepository();
            OCImagesSource = new ObservableCollection<ScrollViewerItemsModel>();
        }
        #endregion

        #region Methods
        public void ReloadSettings(OptionsControlModel model)
        {
            this.optionsModel = model;
        }

        #endregion
    }
}
