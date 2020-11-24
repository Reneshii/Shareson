using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Shareson.Model
{
    public class OptionsModel
    {
        //public string _PathToServerFolder { get; set; }
        public int _Port { get; set; }
        public string _DNSorIP { get; set; }
        public string _LogsFilePath { get; set; }
        public bool _ConnectionMode { get; set; }
        public ObservableCollection<Excluded> _ExcludedExtensionsList { get; set; }
        public ICommand _SaveSettings { get; set; }
    }
}
