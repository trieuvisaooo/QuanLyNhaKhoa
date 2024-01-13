using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerInfo : Page
    {
        private CustomerInfoViewModel customerInfo = new CustomerInfoViewModel();

        public CustomerInfo()
        {
            this.InitializeComponent();
            customerInfo.GetCustomerInfo((App.Current as App).ConnectionString, customerInfo);

        }

        private async void modifyInfo(object sender, RoutedEventArgs e, string connectionString)
        {
            //string phoneNum = ;
            //string address = ;
            SqlConnection con = new SqlConnection(@connectionString);
            try
            {
                con.Open();

                string update_statement = "UPDATE KHACH_HANG SET SDT = '" + PhoneNum.Text + "', DIACHI = N'" + Addr.Text + "', NGAYSINH = '" + ModifyDateOfBirth.Date + "' WHERE MAKH = '" + customerInfo.CusID + "'";
                Debug.WriteLine(update_statement);
                SqlCommand cmnd = new SqlCommand(update_statement, con);
                cmnd.ExecuteNonQuery();
                ContentDialog ModifiedDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Chỉnh Sửa Thông Tin",
                    Content = "Bạn đã chỉnh sửa thông tin thành công thành công!",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await ModifiedDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                ContentDialog FailDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Chỉnh Sửa Thông Tin",
                    Content = "Chỉnh sửa thất bại!",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await FailDialog.ShowAsync();

            }
            finally
            {
                con.Close();
            }
        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            Modify.Visibility = Visibility.Collapsed;
            DateOfBirth.Visibility = Visibility.Collapsed;
            ModifyDateOfBirth.Visibility = Visibility.Visible;
            DateTime dateTime = customerInfo.DateOfBirth.ToDateTime(TimeOnly.MinValue);
            ModifyDateOfBirth.Date = dateTime;
            DateRow.Spacing = 15;
            PhoneNum.IsReadOnly = false;
            Addr.IsReadOnly = false;
            SaveAndCancel.Visibility = Visibility.Visible;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            modifyInfo(sender, e, (App.Current as App).ConnectionString);
            this.Frame.Navigate(typeof(CustomerInfo));

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Modify.Visibility= Visibility.Visible;
            DateRow.Spacing = 30;
            DateOfBirth.Visibility = Visibility.Visible;
            ModifyDateOfBirth.Visibility = Visibility.Collapsed;
            PhoneNum.IsReadOnly = true;
            Addr.IsReadOnly = true;
            SaveAndCancel.Visibility= Visibility.Collapsed;
        }
    }
}
