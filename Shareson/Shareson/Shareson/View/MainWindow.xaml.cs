using Shareson.Model;
using Shareson.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Shareson.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //MainWindowViewModel_IIS mainWindowViewModel_IIS = new MainWindowViewModel_IIS();
        public MainWindow(Data.AccountModel accountModel = null)
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
            base.DataContext = new MainWindowViewModel(accountModel);
            CenterScreen();
        }

        private void CenterScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}