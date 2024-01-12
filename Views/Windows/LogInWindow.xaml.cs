using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInWindow : Window
    {
        public LoginViewModel loginViewModel { get; private set; } = new LoginViewModel();
        public LogInWindow()
        {
            this.InitializeComponent();
            this.AppWindow.Resize(new Windows.Graphics.SizeInt32(720, 500));
            this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            App.SetTitleBarColors(this);
            App.SetResizability(this, false);
            this.SetTitleBar(TitleBar);
            //DispatcherQueue.TryEnqueue(() =>
            //{
            //    App.SetDragRegion(this, Microsoft.UI.Input.NonClientRegionKind.Passthrough, new FrameworkElement[] { InfoButton });
            //});
            // Reset the stored account
            (App.Current as App).CurrentAccount.StoredAccount = null;
        }

        private async void LogInSummit_Click(object sender, RoutedEventArgs e)
        {
            LoadingRing.IsActive = true;
            (sender as Button).Visibility = Visibility.Collapsed;
            try
            {
                bool isSuccess = await Task.Run(() => loginViewModel.SignIn());
                if (isSuccess)
                {
                    Window mainWindow = new AdminWindow();
                    mainWindow.Activate();
                    this.Close();
                }
            }
            catch (System.Exception) { }
            LoadingRing.IsActive = false;
            (sender as Button).Visibility = Visibility.Visible;

        }

        private void RoleSelected_Click(object sender, RoutedEventArgs e)
        {
            var menuFlyoutItem = sender as Microsoft.UI.Xaml.Controls.MenuFlyoutItem;
            loginViewModel.SelectedRole = menuFlyoutItem.Text;
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is Microsoft.UI.Xaml.Controls.TextBox)
            {
                (sender as Microsoft.UI.Xaml.Controls.TextBox).SelectAll();
            }
            else if (sender is Microsoft.UI.Xaml.Controls.PasswordBox)
            {
                (sender as Microsoft.UI.Xaml.Controls.PasswordBox).SelectAll();
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            Window signUpWindow = new SignUpAccountWindow();
            signUpWindow.Activate();
            this.Close();
        }

    }
}
