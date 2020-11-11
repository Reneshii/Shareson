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

            Data.AccountModel model = new Data.AccountModel()
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

        public static string LoginToAccountRequestAsJson(AvailableMethodsOnServer Method, string Email, string Login, string Password)
        {
            string ServerMethod = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method);
            Data.AccountModel model = new Data.AccountModel()
            {
                Login = Login,
                Email = Email,
                Password = Password,
            };
            string jsonRequest = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method) + "<Meth>" + JsonConvert.SerializeObject(model);
            jsonRequest += "<EOS>";
            return jsonRequest;
        }

        public static string CreateImageRequestAsJson(AvailableMethodsOnServer Method, string PathToDirectory, string FileName, Data.AccountModel accountModel, string[] ExcludedExtensions = null)
        {
            JsonModel model = new JsonModel()
            {
                PathToDirectory = PathToDirectory,
                FileName = FileName,
                ExcludedExtensions = ExcludedExtensions
            };
            if(accountModel == null)
            {
                accountModel = new Data.AccountModel();
                accountModel.ID = string.Empty;
                accountModel.Name = string.Empty;
                accountModel.Email = string.Empty;
                accountModel.Login = string.Empty;
                accountModel.Surname = string.Empty;
                accountModel.Password = string.Empty;
            }

            string jsonRequest = System.Enum.GetName(typeof(AvailableMethodsOnServer), Method) 
                + "<Meth>" + JsonConvert.SerializeObject(model) 
                + "<Req>" + JsonConvert.SerializeObject(accountModel);

            jsonRequest += "<EOS>";
            return jsonRequest;
        }

    }
}
