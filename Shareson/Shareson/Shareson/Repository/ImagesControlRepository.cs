using Shareson.Enum;
using Shareson.Interface;
using Shareson.Model;
using Shareson.Repository.SupportMethods;
using Shareson.Support;
using Shareson.Support.ClientHelper;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shareson.Repository
{
    public class ImagesControlRepository : ISocketOnly
    {
        ClientHelper clientHelper = new ClientHelper();

        InfoLog errorLog = new InfoLog(Properties.Settings.Default.LogsFilePath);

        public bool IsMethodInProgress = false;

        async Task<bool> ISocketOnly.StartConnect()
        {
            return clientHelper.ConnectToServer();
        }

        async Task<ObservableCollection<ScrollViewerItemsModel>> ISocketOnly.GetSingleImage(string PathToDirectory, string FileName, string[] ExcludedExtensions = null)
        {
            //ObservableCollection<ScrollViewerItemsModel> Result = new ObservableCollection<ScrollViewerItemsModel>();
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetImage, PathToDirectory, FileName, ExcludedExtensions);


            Task<ObservableCollection<ScrollViewerItemsModel>> GetSingleImage_Task =
                new Task<ObservableCollection<ScrollViewerItemsModel>>(() => GetFile(searchingFile));
            GetSingleImage_Task.Start();
            
            return await GetSingleImage_Task;
        }

        async Task<ObservableCollection<ScrollViewerItemsModel>> ISocketOnly.GetRandomImage(string PathToDirectory, string[] ExcludedExtensions = null)
        {
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetRandomImage, PathToDirectory, null, ExcludedExtensions);

            Task<ObservableCollection<ScrollViewerItemsModel>> GetRandomImage_Task = 
                new Task<ObservableCollection<ScrollViewerItemsModel>>(() => GetFile(searchingFile));
            GetRandomImage_Task.Start();

            return await GetRandomImage_Task;
        }

        async Task<ObservableCollection<ScrollViewerItemsModel>> ISocketOnly.GetMultiImage(string PathToDirectory, int Amount = 5, string[] ExcludedExtensions = null)
        {
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetRandomImage, PathToDirectory, null, ExcludedExtensions);

            Task<ObservableCollection<ScrollViewerItemsModel>> GetMultiImage_Task =
                    new Task<ObservableCollection<ScrollViewerItemsModel>>(() => GetFile(searchingFile,Amount));
            GetMultiImage_Task.Start();

            return await GetMultiImage_Task;
        }

        private void PutFile(string name)
        {

        }
        private ObservableCollection<ScrollViewerItemsModel> GetFile(string searchingFile, int repeat = 1)
        {
            ObservableCollection<ScrollViewerItemsModel> resultToReturn = new ObservableCollection<ScrollViewerItemsModel>();
            for (int i = 0; i < repeat; i++)
            {
                clientHelper.Send(ClientHelperModel.clientSocket, searchingFile);
                clientHelper.Receive(ClientHelperModel.clientSocket);
                resultToReturn.Add(FileAsElementOfScrollViewer());


                clientHelper.ResetAllManualResetEvent();
            }
            return resultToReturn;
        }
        private ScrollViewerItemsModel FileAsElementOfScrollViewer()
        {
            ConvertImage convertImage = new ConvertImage();
            ScrollViewerItemsModel resultToReturn = new ScrollViewerItemsModel()
            {
                BitmapImages = convertImage.CreateImageFromByteArray(clientHelper.model.receivedBytes),
                SizeInBytes = convertImage.Length,
                SizeInMegabytes = convertImage.Length / 1000000,
            };
            return resultToReturn;
        }
    }
}
