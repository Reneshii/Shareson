

using Shareson.Support;
using System.Windows.Input;

namespace Shareson.Model
{
    public class LoginWindowModel 
    {
        public object _LoginWindowContentControl { get; set; }
        public ICommand _CallAccountPage { get; set; }
        public ICommand _CallOptionsPage { get; set; }
        public ICommand _CallContactPage { get; set; }
    }
}