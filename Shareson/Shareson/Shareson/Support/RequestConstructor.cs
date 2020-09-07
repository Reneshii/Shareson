using Newtonsoft.Json;
using Shareson.Enum;
using Shareson.Model;

namespace Shareson.Support
{
    public static class RequestConstructor
    {
        private class JsonModel
        {
            //public string Method;
            public string PathToDirectory;
            public string FileName;
            public string[] ExcludedExtensions;
        }
        public static string CreateAccountRequestAsJson(AvailableMethodsOnServer Method, string Email, string Login, string Password, string Name = null, string Surname = null)
        {
            string ServerMethod = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method);

            AccountModel model = new AccountModel()
            {
                Login = Login,
                Email = Email,
                Password = Password,
                Name = Name,
                Surname = Surname,
            };

            string jsonRequest = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method) + "<Meth>" + JsonConvert.SerializeObject(model);
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

            string jsonRequest = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method) + "<Meth>" + JsonConvert.SerializeObject(model);
            jsonRequest += "<EOS>";
            return jsonRequest;
        }

    }
}
