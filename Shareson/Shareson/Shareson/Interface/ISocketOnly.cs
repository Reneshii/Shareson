using Shareson.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shareson.Interface
{
    public interface ISocketOnly
    {
        Task<bool> StartConnect();
        Task<ObservableCollection<Data.FileInfoModel>> GetRandomImage(string PathToDirectory, int Amount = 1, Data.AccountModel accountModel = null, string[] ExcludedExtensions = null);
        Task<ObservableCollection<Data.FileInfoModel>> GetSingleImage(string PathToDirectory, string FileName, Data.AccountModel accountModel = null, string[] ExcludedExtensions = null);
    }
}
