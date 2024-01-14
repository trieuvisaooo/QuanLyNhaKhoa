using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class CustomerAppointment : Page
    {
        private CustomerAppointmentViewModel customerAppointmentViewModel = new CustomerAppointmentViewModel();

        public CustomerAppointment()
        {
            this.InitializeComponent();
            AppointmentList.ItemsSource = customerAppointmentViewModel.GetAppointments((App.Current as App).ConnectionString, (App.Current as App).CurrentAccount.StoredAccount.Id);
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerMakeAppointment));
        }

    }
}
