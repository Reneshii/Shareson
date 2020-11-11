using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Shareson.Model.ForViews
{
    public class ObservableCollectionControlModel
    {
        public ICommand _ClickImage { get; set; }
        public ObservableCollection<Data.FileInfoModel> _OCImagesSource { get; set; }
        public Data.FileInfoModel _InfoModelFor_ImagesControlViewModel { get; set; }
    }
}
