
namespace Shareson.Model.ForViews
{
    public class ReceivedFileInfoModel
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Creator { get; set; }
        public string CreationTime { get; set; }
        public string Folder { get; set; }
        public byte[] Image { get; set; }
    }
}
