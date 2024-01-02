using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            App.SetTitleBarColors(this);
            this.SetTitleBar(TitleBar);
            contentFrame.CacheSize = 4;
            NvgtView.SelectedItem = NvgtView.MenuItems[0];
            FrameInflate(1);
        }

        private void FrameInflate(int index)
        {
            switch (index)
            {
                case 1:
                    NvgtView.Header = "Lịch Hẹn";
                    contentFrame.Navigate(typeof(MakeAppointmentPage));
                    break;
                case 2:
                    NvgtView.Header = "Hồ Sơ Bệnh Án";
                    contentFrame.Navigate(typeof(CustomerRecords));
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
            }
        }

    }
}
