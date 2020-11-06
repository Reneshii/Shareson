using Shareson.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shareson.Interface
{
    public interface ISocketOnly
    {
        Task<bool> StartConnect();
        Task<ObservableCollection<FileInfoModel>> GetRandomImage(string PathToDirectory, string[] ExcludedExtensions = null);
        Task<ObservableCollection<FileInfoModel>> GetSingleImage(string PathToDirectory, string FileName, string[] ExcludedExtensions = null);
        Task<ObservableCollection<FileInfoModel>> GetMultiImage(string PathToDirectory, int Amount = 1, string[] ExcludedExtensions = null);
    }
}
