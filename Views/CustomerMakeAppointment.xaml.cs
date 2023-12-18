using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using Windows.UI.Notifications;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerMakeAppointment : Page
    {
        public CustomerMakeAppointment()
        {
            this.InitializeComponent();
            customer = new Models.Customer();
        }

        public Models.Customer customer { get; private set; }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-QHS1R0BJ;Initial Catalog=QLPK;Integrated Security=True");

        private async void makeAppointment_Click(object sender, RoutedEventArgs e)
        {
            string appointID = "LH7777";
            try
            {
                string insert_statement = "INSERT LICH_HEN(MALICHHEN, MAKH, NGAYKHAM, GIOKHAM, NHASI) VALUES" +
                                            "('" + appointID + "','" + customer.Id + "','" + Date.Date + "','" + Time.Time + "','" + DenID.Text + "');";
                SqlCommand cmnd = new SqlCommand(insert_statement, con);
                con.Open();
                cmnd.ExecuteNonQuery();
                this.Frame.Navigate(typeof(CustomerAppointment));
                ContentDialog MadeAppointmentDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Đăng Kí Lịch Hẹn",
                    Content = "Bạn đã đăng kí lịch hẹn thành công!",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await MadeAppointmentDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                ContentDialog FailDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Đăng Kí Lịch Hẹn",
                    Content = "Đăng kí thất bại!",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await FailDialog.ShowAsync();

            } 
            finally
            {
                con.Close();
            }


        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerAppointment));
        }
    }
}
