using Shareson.Model;
using Shareson.Model.ForViews;
using Shareson.Repository;
using Shareson.Support;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Shareson.ViewModel
{
    public class ObservableCollectionControlViewModel : Property_Changed
    {
        private ObservableCollectionControlModel model;
        private ObservableCollectionControlRepository repository;

        #region Properties
        public delegate void ImageInfoHandler();
        public event ImageInfoHandler GetImageInfo_Event;

        public ICommand ClickImage
        {
            get
            {
                if (model._ClickImage == null)
                {
                    model._ClickImage = new RelayCommand(f => true, (x) =>
                    {
                        InfoModel = repository.GetImageInfo(x);
                        GetImageInfo_Event?.Invoke();
                    });
                }
                return model._ClickImage;
            }
            set
            {

                NotifyPropertyChanged();
            }
        }

        public FileInfoModel InfoModel
        {
            get
            {
                return model._InfoModel;
            }
            set
            {
                model._InfoModel = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<FileInfoModel> OCImagesSource
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
        #endregion

        public ObservableCollectionControlViewModel()
        {
            model = new ObservableCollectionControlModel();
            repository = new ObservableCollectionControlRepository();
            OCImagesSource = new ObservableCollection<FileInfoModel>();
        }
    }
}
