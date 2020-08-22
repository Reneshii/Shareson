using Shareson.Support;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Shareson.Model
{
    public class ScrollViewerItemsModel : Property_Changed
    {
        public string Name { get; set; }
        public double SizeInBytes { get; set; }
        public double SizeInMegabytes { get; set; }
        public BitmapImage BitmapImages { get; set; }
        public string Path { get; set; }
        public ICommand DetailsBtn { get; set; }
    }
}
