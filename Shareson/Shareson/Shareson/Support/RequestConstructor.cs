using Shareson.Enum;
using Shareson.Model;

namespace Shareson.Support
{
    public class RequestConstructor
    {
        private class JsonModel
        {
            //public string Method;
            public string PathToDirectory;
            public string FileName;
            public string[] ExcludedExtensions;
        }
        public static string CreateConnectionToAccountRequestAsJson(AvailableMethodsOnServer Method, string Email, string Password)
        {
            string ServerMethod = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method);

            AccountModel model = new AccountModel()
            {
                Email = Email,
                Password = Password
            };

            string jsonRequest = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method) + "<Meth>" + Newtonsoft.Json.JsonConvert.SerializeObject(model);
            jsonRequest += "<EOS>";
            return jsonRequest;
        }
        public static string CreateImageRequestAsJson(AvailableMethodsOnServer Method, string PathToDirectory, string FileName, string[] ExcludedExtensions = null)
        {
            JsonModel model = new JsonModel()
            {
                PathToDirectory = PathToDirectory,
                FileName = FileName,
                ExcludedExtensions = ExcludedExtensions
            };

            string jsonRequest = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method) + "<Meth>" + Newtonsoft.Json.JsonConvert.SerializeObject(model);
            jsonRequest += "<EOS>";
            return jsonRequest;
        }
    }
}
