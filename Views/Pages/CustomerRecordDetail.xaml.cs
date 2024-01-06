using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerRecordDetail : Page
    {
        private CustomerRecordViewModel CRViewModel;

        public CustomerRecordDetail()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CRViewModel = e.Parameter as CustomerRecordViewModel;
            medicine_list.ItemsSource = CRViewModel.GetMedicine((App.Current as App).ConnectionString);
            service_list.ItemsSource = CRViewModel.GetService((App.Current as App).ConnectionString);
        }

        private void ViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerInvoiceDetail), CRViewModel);
        }
    }
}
