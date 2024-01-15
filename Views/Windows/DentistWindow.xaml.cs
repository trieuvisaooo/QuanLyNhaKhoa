using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.Views;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DentistWindow : Window
    {
        public DentistWindow()
        {
            this.InitializeComponent();
            App.SetTitleBarColors(this);
            this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(TitleBar);
            contentFrame.CacheSize = 4;
            NvgtView.SelectedItem = NvgtView.MenuItems[0];
            FrameInflate(0);
        }

        //public DentistAccount customer = new DentistAccount();

        private void FrameInflate(int index)
        {
            switch (index)
            {
                case 0:
                    NvgtView.Header = "Lịch hẹn";
                    contentFrame.Navigate(typeof(DentistAppointment));
                    break;
                case 1:
                    NvgtView.Header = "Khách hàng";
                    contentFrame.Navigate(typeof(MedicalRecord));
                    break;
            }
        }

        private void NvgtView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            // Inflate frame according to the index invoked
            switch (args.InvokedItemContainer.Tag)
            {
                case "0":
                    FrameInflate(0);
                    break;
                case "1":
                    FrameInflate(1);
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
                case "0":
                    FrameInflate(0);
                    break;
                case "1":
                    FrameInflate(1);
                    break;
                case "SignOut":                  
                    break;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.GoBack(new SuppressNavigationTransitionInfo());
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            BackButton.Visibility = contentFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
