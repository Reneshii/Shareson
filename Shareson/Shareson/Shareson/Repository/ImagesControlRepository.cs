using Newtonsoft.Json;
using Shareson.Enum;
using Shareson.Interface;
using Shareson.Model;
using Shareson.Model.ForViews;
using Shareson.Repository.SupportMethods;
using Shareson.Support;
using Shareson.Support.ClientHelper;
using System.Collections.ObjectModel;
using System.Text;
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

        async Task<ObservableCollection<FileInfoModel>> ISocketOnly.GetSingleImage(string PathToDirectory, string FileName, string[] ExcludedExtensions = null)
        {
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetImage, PathToDirectory, FileName, ExcludedExtensions);


            Task<ObservableCollection<FileInfoModel>> GetSingleImage_Task =
                new Task<ObservableCollection<FileInfoModel>>(() => GetFile(searchingFile));
            GetSingleImage_Task.Start();
            
            return await GetSingleImage_Task;
        }

        async Task<ObservableCollection<FileInfoModel>> ISocketOnly.GetRandomImage(string PathToDirectory, string[] ExcludedExtensions = null)
        {
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetRandomImage, PathToDirectory, null, ExcludedExtensions);

            Task<ObservableCollection<FileInfoModel>> GetRandomImage_Task = 
                new Task<ObservableCollection<FileInfoModel>>(() => GetFile(searchingFile));
            GetRandomImage_Task.Start();

            return await GetRandomImage_Task;
        }

        async Task<ObservableCollection<FileInfoModel>> ISocketOnly.GetMultiImage(string PathToDirectory, int Amount = 5, string[] ExcludedExtensions = null)
        {
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetRandomImage, PathToDirectory, null, ExcludedExtensions);

            Task<ObservableCollection<FileInfoModel>> GetMultiImage_Task =
                    new Task<ObservableCollection<FileInfoModel>>(() => GetFile(searchingFile,Amount));
            GetMultiImage_Task.Start();

            return await GetMultiImage_Task;
        }

        public void PutFile(string name)
        {

        }
        private ObservableCollection<FileInfoModel> GetFile(string searchingFile, int repeat = 1)
        {
            ObservableCollection<FileInfoModel> resultToReturn = new ObservableCollection<FileInfoModel>();
            for (int i = 0; i < repeat; i++)
            {
                clientHelper.Send(ClientHelperModel.clientSocket, searchingFile);
                clientHelper.Receive(ClientHelperModel.clientSocket);
                resultToReturn.Add(PrepareModelForObservableCollection());


                clientHelper.ResetAllManualResetEvent();
            }
            return resultToReturn;
        }
        private FileInfoModel PrepareModelForObservableCollection()
        {
            ConvertImage convertImage = new ConvertImage();
            byte[] raw;
            string received;
            ReceivedFileInfoModel imageInfo;

            raw = clientHelper.model.receivedBytes;
            received = Encoding.ASCII.GetString(raw);
            imageInfo = JsonConvert.DeserializeObject<ReceivedFileInfoModel>(received);

            float KB = imageInfo.Size / 1024;
            float MB = KB / 1024;

            FileInfoModel resultToReturn = new FileInfoModel()
            {
                BMapImage = convertImage.CreateImageFromByteArray(imageInfo.Image, true),
                SizeInBytes = imageInfo.Size,
                SizeInMegabytes = MB,
                Name = imageInfo.Name,
            };
            return resultToReturn;
        }
    }
}
