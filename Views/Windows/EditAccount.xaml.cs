using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.ViewModels;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditAccountWindow : Window
    {
        public AccountViewModel accountViewModel { get; private set; } = new();
        private Interfaces.Account _account;
        public EditAccountWindow(Interfaces.Account account)
        {
            this.InitializeComponent();
            this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            App.SetTitleBarColors(this);
            this.SetTitleBar(TitleBar);
            _account = account;
            accountViewModel.Id = account.Id;
            accountViewModel.Name = account.Name;
            accountViewModel.Address = account.Address;
            accountViewModel.Password = account.Password;
            if (_account is CustomerAccount)
            {
                accountViewModel.SelectedRole = "Khách hàng";
            }
        }

        private async void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            LoadingRing.IsActive = true;
            (sender as Button).Visibility = Visibility.Collapsed;
            try
            {

                bool isSuccess = await Task.Run(() => accountViewModel.UpdateAccount());
                if (isSuccess)
                {
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
            accountViewModel.SelectedRole = menuFlyoutItem.Text;
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
