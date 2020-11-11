using Newtonsoft.Json;
using Shareson.Enum;
using Shareson.Interface;
using Shareson.Model;
using Shareson.Repository.SupportMethods;
using Shareson.Support;
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

        async Task<ObservableCollection<Data.FileInfoModel>> ISocketOnly.GetSingleImage(string PathToDirectory, string FileName, Data.AccountModel accountModel = null, string[] ExcludedExtensions = null)
        {
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetImage, PathToDirectory, FileName, accountModel, ExcludedExtensions);

            Task<ObservableCollection<Data.FileInfoModel>> GetSingleImage_Task =
                new Task<ObservableCollection<Data.FileInfoModel>>(() => GetFile(searchingFile));
            GetSingleImage_Task.Start();
            
            return await GetSingleImage_Task;
        }

        async Task<ObservableCollection<Data.FileInfoModel>> ISocketOnly.GetRandomImage(string PathToDirectory, int Amount = 1, Data.AccountModel accountModel = null, string[] ExcludedExtensions = null)
        {
            string searchingFile = RequestConstructor.CreateImageRequestAsJson(AvailableMethodsOnServer.GetRandomImage, PathToDirectory,null ,accountModel, ExcludedExtensions);

            Task <ObservableCollection<Data.FileInfoModel>> GetRandomImage_Task = 
                new Task<ObservableCollection<Data.FileInfoModel>>(() => GetFile(searchingFile, Amount));
            GetRandomImage_Task.Start();

            return await GetRandomImage_Task;
        }

        public void PutFile(string name)
        {

        }
        private ObservableCollection<Data.FileInfoModel> GetFile(string searchingFile, int repeat = 1)
        {
            ObservableCollection<Data.FileInfoModel> resultToReturn = new ObservableCollection<Data.FileInfoModel>();
            for (int i = 0; i < repeat; i++)
            {
                clientHelper.Send(Data.ClientHelperModel.clientSocket, searchingFile);
                clientHelper.Receive(Data.ClientHelperModel.clientSocket);
                resultToReturn.Add(PrepareModelForObservableCollection());


                clientHelper.ResetAllManualResetEvent();
            }
            return resultToReturn;
        }
        private Data.FileInfoModel PrepareModelForObservableCollection()
        {
            ConvertImage convertImage = new ConvertImage();
            byte[] raw;
            string received;
            Data.FileInfoModel imageInfo;

            raw = clientHelper.model.receivedBytes;
            received = Encoding.ASCII.GetString(raw);
            imageInfo = JsonConvert.DeserializeObject<Data.FileInfoModel>(received);

            float KB = imageInfo.Size / 1024;
            float MB = KB / 1024;

            Data.FileInfoModel resultToReturn = new Data.FileInfoModel()
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
