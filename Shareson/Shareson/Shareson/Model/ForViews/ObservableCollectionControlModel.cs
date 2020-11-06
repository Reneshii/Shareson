using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Shareson.Model.ForViews
{
    public class ObservableCollectionControlModel
    {
        public ICommand _ClickImage { get; set; }
        public ObservableCollection<FileInfoModel> _OCImagesSource { get; set; }
        public FileInfoModel _InfoModel{ get; set; }
    }
}
