using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.ViewModels;

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
            this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            App.SetTitleBarColors(this);
            this.SetTitleBar(TitleBar);
        }

        private async void LogInSummit_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
