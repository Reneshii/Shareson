using Shareson.Interface;
using Shareson.Model;
using Shareson.Repository;
using Shareson.Support;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Shareson.ViewModel
{
    class MainWindowViewModel_IIS : Property_Changed
    {
        MainWindowModel model;
        ImainWindowRepository_IIS windowRepository;
        public ICommand Upload
        {
            get
            {
                if(model._Upload == null)
                {
                    model._Upload = new RelayCommand(p =>  true, async p=> await windowRepository.PutImageAsync());
                }
                return model._Upload;
            }
            set { }
        }
        public ICommand DisplaySingleRandom
        {
            get
            {
                if(model._DisplaySingleRandom == null)
                {
                    model._DisplaySingleRandom = new RelayCommand(p => true, async p => OCImagesSource = await windowRepository.GetImageAsync(windowRepository.ModelForSingleRandom));
                }
                return model._DisplaySingleRandom;
            }
            set { }
        }
        public ICommand DisplaySingle
        {
            get
            {
                if(model._DisplaySingle == null)
                {                  
                    model._DisplaySingle = new RelayCommand(p => CanDisplayImage, async p => OCImagesSource = await windowRepository.GetImageAsync(windowRepository.ModelForSingle));
                }
                return model._DisplaySingle;
            }
            set { }
        }
        public ICommand DisplayImages
        {
            get
            { 
                if(model._DisplayImages == null)
                {
                    model._DisplayImages = new RelayCommand(p=> CanDisplayImages, async p => OCImagesSource = await windowRepository.GetImageAsync(windowRepository.ModelForMulti));
                }
                return model._DisplayImages;
                
            }
            set { }
        }
        public ObservableCollection<StoredFiles> OCImagesSource
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
        public bool CanDisplayImages
        {
            get
            {
                return model._CanDisplayImages;
            }
            set
            {
                model._CanDisplayImages = value;
                NotifyPropertyChanged();
            }
        }
        public bool CanDisplayImage
        {
            get
            {
                return model._CanDisplayImage;
            }
            set
            {
                model._CanDisplayImage = value;
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
                windowRepository.ModelForSingle.FileName = model._FileName;

                if(!string.IsNullOrEmpty(model._FileName))
                {
                    CanDisplayImage = true;
                }
                else
                {
                    CanDisplayImage = false;
                }
                NotifyPropertyChanged();
            }
        }
        public string DirectoryPathMulti
        {
            get
            {
                return model._DirectoryPathMulti;
            }
            set
            {
                model._DirectoryPathMulti = value;
                windowRepository.ModelForMulti.DirectoryPath = model._DirectoryPathMulti;

                if (!string.IsNullOrEmpty(model._DirectoryPathMulti))
                {
                    CanDisplayImages = true;
                }
                else
                {
                    CanDisplayImages = false;
                }
                NotifyPropertyChanged();
            }
        }
        public string DirectoryPathSingle
        {
            get
            {
                return model._DirectoryPathSingle;
            }
            set
            {
                model._DirectoryPathSingle = value;
                windowRepository.ModelForSingle.DirectoryPath = model._DirectoryPathSingle;
                windowRepository.ModelForSingleRandom.DirectoryPath = model._DirectoryPathSingle;

                if (!string.IsNullOrEmpty(model._DirectoryPathSingle))
                {
                    CanDisplayImage = true;
                }
                else
                {
                    CanDisplayImage = false;
                }
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
                windowRepository.ModelForMulti.Limit = model._ImagesLimit;
                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel_IIS()
        {
            model = new MainWindowModel();
            windowRepository = new MainWindowRepository_IIS();
            Initialize();
        }

        private void Initialize()
        {
            Upload = new RelayCommand(p => true, async p =>await windowRepository.PutImageAsync());

            windowRepository.ModelForSingle = new MainWindowRepository_IIS.DownloadingInfo
            {
                Limit = 0,
                Random = false,
                RefreshList = false,
                ExcludedExtensions = new System.Collections.Generic.List<MainWindowRepository_IIS.DownloadingInfo.Extension>
                {
                    MainWindowRepository_IIS.DownloadingInfo.Extension.webm,
                    MainWindowRepository_IIS.DownloadingInfo.Extension.mp4,
                },
                Type = MainWindowRepository_IIS.DownloadingInfo.Mode.Single,
            };
            DisplaySingle = new RelayCommand(p => CanDisplayImage, async p => OCImagesSource = await windowRepository.GetImageAsync(windowRepository.ModelForSingle));

            windowRepository.ModelForSingleRandom = new MainWindowRepository_IIS.DownloadingInfo
            {
                Limit = 0,
                Random = true,
                RefreshList = false,
                ExcludedExtensions = new System.Collections.Generic.List<MainWindowRepository_IIS.DownloadingInfo.Extension>
                {
                    MainWindowRepository_IIS.DownloadingInfo.Extension.webm,
                    MainWindowRepository_IIS.DownloadingInfo.Extension.mp4,
                },
                Type = MainWindowRepository_IIS.DownloadingInfo.Mode.SingleRandom,
            };
            DisplaySingleRandom = new RelayCommand(p => true, async p => OCImagesSource = await windowRepository.GetImageAsync(windowRepository.ModelForSingleRandom));
            
            windowRepository.ModelForMulti = new MainWindowRepository_IIS.DownloadingInfo
            {
                Random = true,
                RefreshList = true,
                ExcludedExtensions = new System.Collections.Generic.List<MainWindowRepository_IIS.DownloadingInfo.Extension>
                {
                    MainWindowRepository_IIS.DownloadingInfo.Extension.webm,
                    MainWindowRepository_IIS.DownloadingInfo.Extension.mp4,
                },
                Type = MainWindowRepository_IIS.DownloadingInfo.Mode.Multi,
            };
            DisplayImages = new RelayCommand(p => CanDisplayImages, async p => OCImagesSource = await windowRepository.GetImageAsync(windowRepository.ModelForMulti));
        }
    }
}
