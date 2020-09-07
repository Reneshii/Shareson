using System.Windows.Input;

namespace Shareson.Model
{
    public class AccountControlModel
    {
        public string _Name { get; set; }
        public string _Surname { get; set; }
        public string _Login { get; set; }
        public string _Password { get; set; }
        public string _Email { get; set; }
        public string _ID { get; set; }
        public bool _Logged { get; set; }
        public bool _LogInEnable { get; set; }
        public ICommand _LogInBtn { get; set; }
    }
}
