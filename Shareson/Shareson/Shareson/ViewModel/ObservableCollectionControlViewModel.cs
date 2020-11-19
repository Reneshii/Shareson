using Shareson.Model;
using Shareson.Model.ForViews;
using Shareson.Repository;
using Shareson.Support;
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
        public event ImageInfoHandler GetImageInfoWhileClick_Event;

        public ICommand ClickImage
        {
            get
            {
                if (model._ClickImage == null)
                {
                    model._ClickImage = new RelayCommand(f => true, (x) =>
                    {
                        InfoModelFor_ImagesControlViewModel = repository.GetImageInfo(x);
                        GetImageInfoWhileClick_Event?.Invoke();
                    });
                }
                return model._ClickImage;
            }
            set
            {

                NotifyPropertyChanged();
            }
        }

        public Data.FileInfoModel InfoModelFor_ImagesControlViewModel
        {
            get
            {
                return model._InfoModelFor_ImagesControlViewModel;
            }
            set
            {
                model._InfoModelFor_ImagesControlViewModel = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Data.FileInfoModel> OCImagesSource
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
            OCImagesSource = new ObservableCollection<Data.FileInfoModel>();
        }
    }
}
