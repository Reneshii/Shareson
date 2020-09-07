using System.Windows.Input;

namespace Shareson.Model.ForViews
{
    public class CreateAccountModel
    {
        public ICommand _CreateAccount { get; set; }

        public string _Login { get; set; }
        public string _Email { get; set; }
        public string _Password { get; set; }
        public string _ErrorLogin { get; set; }
        public string _ErrorPassword { get; set; }
        public string _ErrorEmail { get; set; }
    }
}
