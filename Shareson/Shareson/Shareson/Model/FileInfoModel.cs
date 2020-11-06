using Shareson.Support;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Shareson.Model
{
    public class FileInfoModel : Property_Changed
    {
        public string Name { get; set; }
        public float SizeInBytes { get; set; }
        public float SizeInMegabytes { get; set; }
        public BitmapImage BMapImage { get; set; }
        public string Path { get; set; }
    }
}
