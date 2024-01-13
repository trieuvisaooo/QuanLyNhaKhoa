using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.ObjectModel;
using QuanLyNhaKhoa.ViewModels.Customer;

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
            customer = customer.GetCustomerInfo((App.Current as App).ConnectionString, customer);
            DenList.ItemsSource = getDentists((App.Current as App).ConnectionString);
        }

        private CustomerInfoViewModel customer = new CustomerInfoViewModel();

        public List<string> DenNameList = new List<string>();
        public List<string> getDentists(string connectionString)
        {
            string getDenListQuery = "select HOTEN from NHA_SI";

            var dentists = new ObservableCollection<CustomerMakeAppointmentViewModel>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = getDenListQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    DenNameList.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
                return DenNameList;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine($"Exception: {eSql.Message}");
            }
            return null;
        }


        private async void makeAppointment_Click(object sender, RoutedEventArgs e)
        {
            //var builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @".\SQLSERVER";
            //builder.InitialCatalog = "QLPK";
            //builder.IntegratedSecurity = true;
            //string connectionString = builder.ConnectionString;
            string connectionString = (App.Current as App).ConnectionString;
            SqlConnection con = new SqlConnection(@connectionString);
            Debug.WriteLine(@connectionString);
            string denName = (string)DenList.SelectedValue;
            try
            {
                con.Open();

                string insert_statement = "EXEC sp_themLHCoTenNS '" + customer.CusID + "', '" + AppoDate.Date + "', '" + AppoTime.Time + "', N'" + denName + "'";
                SqlCommand cmnd = new SqlCommand(insert_statement, con);
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
                Debug.WriteLine($"Exception: {ex.Message}");
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
