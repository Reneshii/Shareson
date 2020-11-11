using Shareson.Model;
using Shareson.Model.ForViews;
using Shareson.Support;

namespace Shareson.ViewModel
{
    public class ImageInfoControlViewModel : Property_Changed
    {
        ImageInfoControlModel model;

        #region Properites
        public Data.FileInfoModel FileInfoModel
        {
            get
            {
                return model.FileInfoModel;
            }
            set
            {
                model.FileInfoModel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        void InitializeProperties()
        {
            model = new ImageInfoControlModel();
            FileInfoModel = new Data.FileInfoModel();
        }

        public ImageInfoControlViewModel(Data.FileInfoModel model)
        {
            InitializeProperties();

            this.model.FileInfoModel = model;
        }
    }
}
