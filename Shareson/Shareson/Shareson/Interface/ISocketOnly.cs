using Shareson.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shareson.Interface
{
    public interface ISocketOnly
    {
        Task<bool> StartConnect();
        Task<ObservableCollection<ScrollViewerItemsModel>> GetRandomImage(string PathToDirectory, string[] ExcludedExtensions = null);
        Task<ObservableCollection<ScrollViewerItemsModel>> GetSingleImage(string PathToDirectory, string FileName, string[] ExcludedExtensions = null);
        Task<ObservableCollection<ScrollViewerItemsModel>> GetMultiImage(string PathToDirectory, int Amount = 1, string[] ExcludedExtensions = null);
    }
}
