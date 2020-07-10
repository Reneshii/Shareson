using Shareson.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static Shareson.Repository.MainWindowRepository_IIS;

namespace Shareson.Interface
{
    public interface ImainWindowRepository_IIS
    {
        //Task<BitmapImage> GetImageAsync(string path, string file);
        DownloadingInfo ModelForSingle { get; set; }
        DownloadingInfo ModelForSingleRandom { get; set; }
        DownloadingInfo ModelForMulti { get; set; }
        Task<ObservableCollection<StoredFiles>> GetImageAsync(DownloadingInfo info);
        Task PutImageAsync();
    }
}
