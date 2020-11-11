using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Shareson.Model
{
    public class ImagesControlModel
    {
        public Data.FileInfoModel _FileInfoModel { get; set; }
        public ObservableCollection<Data.FileInfoModel> _OCImagesSource { get; set; }
        public ICommand _DisplaySingle { get; set; }
        public ICommand _DisplayMulti { get; set; }
        public ICommand _DisplaySingleRandom { get; set; }
        public ICommand _Upload { get; set; }
        public Visibility _Loading { get; set; }
        public Visibility _ItemsControlVisability { get; set; }
        public string _FileName { get; set; }
        public string _DirectoryPath { get; set; }
        public string[] _DirectoriesPath { get; set; }
        public bool _ConnectedToServer { get; set; }
        public int _ImagesLimit { get; set; }
        public object _ItemsControlViewModelContentControl { get; set; }
    }
}
