using System.Windows.Input;

namespace Shareson.Model
{
    public class MainWindowModel
    {
        public object _MainWindowContentControl { get; set; }
        public ICommand _CallImagesPage  { get; set; }
        public ICommand _CallOptionsPage { get; set; }
        public ICommand _CallContactPage { get; set; }
        public ICommand _CallAccountOptionsPage { get; set; }
    }
}
