using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels.Customer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Windows.ApplicationModel.Appointments;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class CustomerAppointment : Page
    {
        public CustomerAppointment()
        {
            this.InitializeComponent();
            customerInfo.GetCustomerInfo((App.Current as App).ConnectionString, customerInfo);
            AppointmentList.ItemsSource = customerAppointmentViewModel.GetAppointments((App.Current as App).ConnectionString, customerInfo.CusID);
        }

        private CustomerAppointmentViewModel customerAppointmentViewModel = new CustomerAppointmentViewModel();
        private CustomerInfoViewModel customerInfo = new CustomerInfoViewModel();

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerMakeAppointment));
        }

    }
}
