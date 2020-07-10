using Shareson.Interface;
using Shareson.Support;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using Shareson.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Shareson.Repository
{
    public class MainWindowRepository_IIS : ImainWindowRepository_IIS
    {
        public class DownloadingInfo
        {
            public string DirectoryPath { get; set; }
            public bool RefreshList { get; set; }
            public int Limit { get; set; }
            public string FileName { get; set; }
            public bool Random { get; set; }
            public Mode Type { get; set; }
            public List<Extension> ExcludedExtensions = new List<Extension>();
            public bool UseFileSource { get; set; }
            public string FileSroucePath { get; set; }

            public enum Mode
            {
                Single,
                SingleRandom,
                Multi,
            }
            public enum Extension
            {
                webm,
                jpeg,
                jpg,
                png,
                mp4,
            }
        }

        public DownloadingInfo ModelForSingle { get; set; }
        public DownloadingInfo ModelForSingleRandom { get; set; }
        public DownloadingInfo ModelForMulti { get; set; }
        List<StoredFiles> FilesReadyToDownloadInfo;
        ErrorLog Error;
        string URI;
        static HttpClient client;

        private void Initialize()
        {
            URI = "http://192.168.0.11:80/";
            FilesReadyToDownloadInfo = new List<StoredFiles>();
            client = new HttpClient();
            client.BaseAddress = new Uri(URI);
            Error = new ErrorLog(AppDomain.CurrentDomain.BaseDirectory, "ErrorLog.txt");
        }

        public MainWindowRepository_IIS()
        {
            Initialize();
        }

        public async Task PutImageAsync()
        {
            HttpContent content;
            Microsoft.Win32.FileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.ShowDialog();
            var fileName = fileDialog.FileName;

            var imageBase64String = GetBase64StringFromImage(fileName);

            content = new StringContent(imageBase64String, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($@"api/Files?file",content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("File uploaded");
            }
            else
            {
                MessageBox.Show(response.RequestMessage.ToString());
                MessageBox.Show("File not uploaded");
            }
        }

        public async Task<ObservableCollection<StoredFiles>>GetImageAsync(DownloadingInfo info)
        {
            var ObsCollection = new ObservableCollection<StoredFiles>();
            try
            {
                var result = await GetFiles(info);

                foreach (var item in result)
                {
                    ObsCollection.Add(item);
                }
                return ObsCollection;
            }
            catch (Exception e)
            {
                Error.Add(e.ToString());
                return null;
            }
        }

        private async Task<List<StoredFiles>> GetFiles(DownloadingInfo info)
        {
            var Collection = new List<StoredFiles>();
            try
            {
                if (FilesReadyToDownloadInfo.Count == 0 || info.RefreshList)
                {
                    FilesReadyToDownloadInfo = await GetFillesInfoFromServer(info, ExcludeExtension(info.ExcludedExtensions));
                }

                // If single or random single download only 1 from list of max available items
                if((info.Type == DownloadingInfo.Mode.Single && FilesReadyToDownloadInfo.Count > 1)||
                    (info.Type == DownloadingInfo.Mode.SingleRandom && FilesReadyToDownloadInfo.Count > 1))
                {
                    FilesReadyToDownloadInfo = await GetFillesInfoFromServer(info, ExcludeExtension(info.ExcludedExtensions));
                }
                else if(info.Type == DownloadingInfo.Mode.Multi && FilesReadyToDownloadInfo.Count == 1)
                {
                    FilesReadyToDownloadInfo = await GetFillesInfoFromServer(info, ExcludeExtension(info.ExcludedExtensions));
                }


                if(info.Type == DownloadingInfo.Mode.Single)
                {
                    List<StoredFiles> SingleStordFiles = new List<StoredFiles>();
                    SingleStordFiles = FilesReadyToDownloadInfo.Where(x => x.Name.Equals(info.FileName)).ToList();
                    Collection = await GetImageFromBase64String(SingleStordFiles);
                }
                else if(info.Type == DownloadingInfo.Mode.SingleRandom)
                {
                    Random randomNumber = new Random();
                    List<StoredFiles> SingleStordFiles = new List<StoredFiles>();
                    SingleStordFiles.Add(FilesReadyToDownloadInfo[randomNumber.Next(0,FilesReadyToDownloadInfo.Count)]);
                    Collection = await GetImageFromBase64String(SingleStordFiles);
                }
                else if(info.Type == DownloadingInfo.Mode.Multi)
                {
                    Collection = await GetImageFromBase64String(FilesReadyToDownloadInfo);
                }
                else
                {
                    Collection = await GetImageFromBase64String(FilesReadyToDownloadInfo);
                }


                return Collection;
            }
            catch(Exception e)
            {
                Error.Add(e.ToString());
                return null;
            }
        }

        private List<string> ExcludeExtension(List<DownloadingInfo.Extension> extensions)
        {
            List<string> result = new List<string>();
            foreach (var item in extensions)
            {
                switch (item)
                {
                    case DownloadingInfo.Extension.webm:
                        {
                            result.Add(".webm");
                            break;
                        }
                    case DownloadingInfo.Extension.jpeg:
                        {
                            result.Add(".jpeg");
                            break;
                        }
                    case DownloadingInfo.Extension.jpg:
                        {
                            result.Add(".jpg");
                            break;
                        }
                    case DownloadingInfo.Extension.png:
                        {
                            result.Add(".png");
                            break;
                        }
                    case DownloadingInfo.Extension.mp4:
                        {
                            result.Add(".mp4");
                            break;
                        }
                    default:
                        return null;
                }
            }
            return result;
        }
        private async Task<List<StoredFiles>> GetFillesInfoFromServer(DownloadingInfo info, List<string> excludedExstensions)
        {
            string convertedPath;
            List<StoredFiles> StoredFilesInstance = new List<StoredFiles>();

            if (excludedExstensions != null)
            {
                convertedPath = $@"api/Files?directoryNameToSearch=" + info.DirectoryPath +
                       "&limit=" + info.Limit +
                       "&random=" + info.Random;
                for (int i = 0; i < excludedExstensions.Count; i++)
                {
                    convertedPath += "&Excluded=" + excludedExstensions[i];
                }
            }
            else
            {
                convertedPath = $@"api/Files?directoryNameToSearch=" + info.DirectoryPath + "&limit=" + info.Limit +
                    "&random=" + info.Random + "&Excluded=null";
            }

            var FileListRespone = await client.GetAsync(convertedPath);
            if (FileListRespone.IsSuccessStatusCode)
            {
                var raw = FileListRespone.Content.ReadAsStringAsync().Result.Replace(@"\", string.Empty);
                var final = raw.Trim().Substring(1, (raw.Length) - 2);
                var deserializedJson = JsonConvert.DeserializeObject<List<StoredFiles>>(final);

                foreach (var jsonItem in deserializedJson)
                {
                    StoredFilesInstance.Add(new StoredFiles
                    {
                        Name = jsonItem.Name,
                        SizeInBytes = jsonItem.SizeInBytes,
                        Path = $@"api/Files?path=" + info.DirectoryPath + "&file=" + jsonItem.Name,
                    });
                }
                //if(info.UseFileSource)
                //{
                //    if(File.Exists(info.FileSroucePath))
                //    {
                //        using (FileStream file = new FileStream(info.FileSroucePath, FileMode.Open))
                //        {
                //            if(info.RefreshList)
                //            {
                                
                //            }
                //        }
                //    }
                //}
                return StoredFilesInstance;
            }
            else
            {
                return null;
            }
        }
        private string GetBase64StringFromImage(string pathToFile)
        {
            string Base64StringImage;
            FileInfo fileInfo = new FileInfo(pathToFile);
            byte[] data = new byte[fileInfo.Length];

            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }
            Base64StringImage = Convert.ToBase64String(data);
            return Base64StringImage;
        }
        private async Task<List<StoredFiles>> GetImageFromBase64String(List<StoredFiles> PathToFiles)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            List<StoredFiles> FilesToReturn = new List<StoredFiles>();
            
            BitmapImage bitmap = new BitmapImage();

            foreach (var item in PathToFiles)
            {
                response = await client.GetAsync(item.Path);

                if (response.IsSuccessStatusCode)
                {
                    bitmap = new BitmapImage();
                    var byteResponse = Convert.FromBase64String(response.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty));

                    using (MemoryStream ms = new MemoryStream(byteResponse, 0, byteResponse.Length))
                    {
                        ms.Position = 0;
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                    }

                    FilesToReturn.Add(new StoredFiles()
                    {
                        Name = item.Name,
                        BitmapImages = bitmap,
                        SizeInBytes = item.SizeInBytes,
                        SizeInMegabytes = item.SizeInBytes / 1000000
                    });
                }
                else
                {
                    return null;
                }
            }
            return FilesToReturn;
        }
    }
}
