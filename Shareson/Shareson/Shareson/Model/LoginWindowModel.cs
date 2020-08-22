using System.Windows.Input;

namespace Shareson.Model
{
    public class LoginWindowModel 
    {
        public bool _ServerStatus { get; set; }
        public bool _IsCreateAccountAvailable { get; set; }
        public object _LoginWindowContentControl { get; set; }
        
        public ICommand _CallLogInControl { get; set; }
        public ICommand _CallLimitedOptionsControl { get; set; }
        public ICommand _CallContactControl { get; set; }
        public ICommand _CallCreateAccountControl { get; set; }
    }
}