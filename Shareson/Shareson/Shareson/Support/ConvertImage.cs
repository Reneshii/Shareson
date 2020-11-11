using System.IO;
using System.Windows.Media.Imaging;

namespace Shareson.Repository.SupportMethods
{
    public class ConvertImage
    {
        public int Length;

        public BitmapImage CreateImageFromByteArray(byte[] data, bool miniature = false)
        {
            MemoryStream stream = new MemoryStream();
            BitmapImage bitMapImage = new BitmapImage();


            Length = data.Length;
            stream.Close();
            stream.Dispose();
            stream = new MemoryStream(data, 0, data.Length);
            stream.Position = 0;

            bitMapImage.BeginInit();
            bitMapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitMapImage.StreamSource = stream;
            bitMapImage.EndInit();


            stream.Dispose();
            stream.Close();
            bitMapImage.Freeze();

            return bitMapImage;
        }
    }
}
