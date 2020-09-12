using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Shareson.Model
{
    public class ImagesControlModel
    {
        public ObservableCollection<ScrollViewerItemsModel> _OCImagesSource { get; set; }
        public ICommand _DisplaySingle { get; set; }
        public ICommand _DisplayMulti { get; set; }
        public ICommand _DisplaySingleRandom { get; set; }
        public ICommand _DetailsBtn { get; set; }
        public ICommand _Upload { get; set; }
        //public ICommand _ConnectToServer { get; set; }
        public Visibility _Loading { get; set; }
        public Visibility _ItemsControlVisability { get; set; }
        public string _FileName { get; set; }
        public string _DirectoryPath { get; set; }
        public bool _ConnectedToServer { get; set; }
        public int _ImagesLimit { get; set; }
    }
}
