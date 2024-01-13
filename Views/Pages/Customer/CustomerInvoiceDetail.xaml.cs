using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels.Customer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.Pages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CustomerInvoiceDetail : Page
{
    private CustomerRecordViewModel CRViewModel;

    public CustomerInvoiceDetail()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        CRViewModel = e.Parameter as CustomerRecordViewModel;

        medicine_list.ItemsSource = CRViewModel.Medicines;
        service_list.ItemsSource = CRViewModel.Services;
    }

    private void Status_Loaded(object sender, RoutedEventArgs e)
    {
        if (CRViewModel.InvoiceStatus == 0)
        {
            status_txtb.Text = "Chưa thanh toán";
            Color red = Color.FromArgb(255, 255, 0, 0);
            status_txtb.Foreground = new SolidColorBrush(red);
        }
        else
        {
            Color green = Color.FromArgb(255, 0, 255, 0);
            status_txtb.Foreground = new SolidColorBrush(green);
            status_txtb.Text = "Đã thanh toán";
        }
    }


    private void ViewRecord_Click(object sender, RoutedEventArgs e)
    {
        this.Frame.Navigate(typeof(CustomerRecordDetail), CRViewModel);
    }
}
