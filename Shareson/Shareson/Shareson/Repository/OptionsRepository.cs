using Shareson.Model;
using System.Linq;

namespace Shareson.Repository
{
    public class OptionsRepository
    {
        public void SaveSettings(OptionsModel tempModel) // options -> global settings
        {
            Properties.Settings.Default.LogsFilePath = tempModel._LogsFilePath;
            Properties.Settings.Default.DNSorIP = tempModel._DNSorIP;
            Properties.Settings.Default.Port = tempModel._Port;
            Properties.Settings.Default.ExcludedExtensionsList = tempModel._ExcludedExtensionsList;

            Properties.Settings.Default.Save();
        }

        public OptionsModel LoadSettings() // global settings -> options 
        {
            OptionsModel model = new OptionsModel();

            model._LogsFilePath = Properties.Settings.Default.LogsFilePath;
            model._DNSorIP = Properties.Settings.Default.DNSorIP;
            model._Port = Properties.Settings.Default.Port;
            model._ExcludedExtensionsList = Properties.Settings.Default.ExcludedExtensionsList;

            return model;
        }

        public void ClearSettings()
        {
            Properties.Settings.Default.PathToServerFolder = string.Empty;
            Properties.Settings.Default.LogsFilePath = string.Empty;
            Properties.Settings.Default.DNSorIP = string.Empty;
            Properties.Settings.Default.Port = 0;
            Properties.Settings.Default.ExcludedExtensionsList.Clear();


            Properties.Settings.Default.Save();
        }

        public System.Collections.ObjectModel.ObservableCollection<Excluded> RefreshList(System.Collections.ObjectModel.ObservableCollection<Excluded> list)
        {
            if (list == null)
            {
                list = new System.Collections.ObjectModel.ObservableCollection<Excluded>();
                foreach (var item in Support.ExcludedExtensions.Excluded)
                {
                    list.Add(new Excluded() { CheckedBox = false, ExcludedExtension = item });
                }
            }
            else
            {
                var tempList = list.Select(f => f.ExcludedExtension);
                if (tempList.Count() < Support.ExcludedExtensions.Excluded.Length)
                {
                    foreach (var missingItem in Support.ExcludedExtensions.Excluded)
                    {
                        if (!tempList.Contains(missingItem))
                        {
                            list.Add(new Excluded() { CheckedBox = false, ExcludedExtension = missingItem });
                        }
                    }
                }
                else if (list.Count() > Support.ExcludedExtensions.Excluded.Length)
                {
                    list = new System.Collections.ObjectModel.ObservableCollection<Excluded>();
                    foreach (var item in Support.ExcludedExtensions.Excluded)
                    {
                        list.Add(new Excluded() { ExcludedExtension = item, CheckedBox = false });
                    }
                }
            }
            return list;
        }
    }
}
