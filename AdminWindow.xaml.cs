using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Views;
using QuanLyNhaKhoa.Views.Pages.Administrator;
using QuanLyNhaKhoa.Views.Pages.Customer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminWindow : Window
    {
        internal ViewModels.BriefInfoViewModel infoViewModel = new((App.Current as App).CurrentAccount.StoredAccount);
        public AdminWindow()
        {
            this.InitializeComponent();
            this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            App.SetTitleBarColors(this);
            this.SetTitleBar(TitleBar);
            contentFrame.CacheSize = 4;
            //NvgtView.SelectedItem = NvgtView.MenuItems[0];
            FrameInflate(1);
        }

        private void FrameInflate(int index)
        {
            switch (index)
            {
                case 1:
                    NvgtView.Header = "Nhân Viên";
                    contentFrame.Navigate(typeof(AdministratorViewReceptionist));
                    break;
                case 2:
                    NvgtView.Header = "Khách Hàng";
                    contentFrame.Navigate(typeof(AdministratorViewCustomer));
                    break;
                case 3:
                    NvgtView.Header = "Nha Sĩ";
                    contentFrame.Navigate(typeof(AdministratorViewDentist));
                    break;
                case 4:
                    NvgtView.Header = "Thuốc";
                    contentFrame.Navigate(typeof(CustomerRecords));
                    break;
                case 5:
                    NvgtView.Header = "Quản Trị Viên";
                    contentFrame.Navigate(typeof(AdministratorViewAdministrator));
                    break;
            }
        }

        private void NvgtView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            // Inflate frame according to the index invoked
            switch (args.InvokedItemContainer.Tag)
            {
                case "1":
                    FrameInflate(1);
                    break;
                case "2":
                    FrameInflate(2);
                    break;
                case "3":
                    FrameInflate(3);
                    break;
                case "4":
                    FrameInflate(4);
                    break;
                case "5":
                    FrameInflate(5);
                    break;
                case "SignOut":
                    Window LogInWindow = new LogInWindow();
                    LogInWindow.Activate();
                    this.Close();
                    break;
            }
        }

        private void NvgtView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            // Inflate frame according to the index selected
            switch (args.SelectedItemContainer.Tag)
            {
                case "1":
                    FrameInflate(1);
                    break;
                case "2":
                    FrameInflate(2);
                    break;
                case "3":
                    FrameInflate(3);
                    break;
                case "4":
                    FrameInflate(4);
                    break;
                case "5":
                    FrameInflate(5);
                    break;
                case "SignOut":
                    break;
            }
        }
        private void Add_Account_Click(object sender, RoutedEventArgs e)
        {
            Window addAccountWindow = new AddAccountWindow();
            addAccountWindow.Activate();
        }

    }
}
