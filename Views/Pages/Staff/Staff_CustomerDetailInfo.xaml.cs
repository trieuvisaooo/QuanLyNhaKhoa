// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels.Receptionist;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Staff_CustomerDetailInfo : Page
    {
        public Staff_CustomerDetailInfo()
        {
            this.InitializeComponent();
        }

        public Staff_CustomerListViewModel staffDetailInfo;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            staffDetailInfo = e.Parameter as Staff_CustomerListViewModel;

        }

        private async void modifyInfo(object sender, RoutedEventArgs e, string connectionString)
        {
            string phoneNum = PhoneNum.Text;
            string address = Addr.Text;
            SqlConnection con = new SqlConnection(@connectionString);
            try
            {
                con.Open();

                string update_statement = "UPDATE KHACH_HANG SET SDT = '" + phoneNum + "', DIACHI = N'" + address + "' WHERE MAKH = '" + staffDetailInfo.CusID + "'";
                Debug.WriteLine(update_statement);
                SqlCommand cmnd = new SqlCommand(update_statement, con);
                cmnd.ExecuteNonQuery();
                ContentDialog ModifiedDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Chỉnh Sửa Thông Tin",
                    Content = "Bạn đã chỉnh sửa thông tin thành công!",
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
            DateOfBirth.Background.Opacity = 0;
            //DateOfBirth.IsReadOnly = false;
            PhoneNum.IsReadOnly = false;
            Addr.IsReadOnly = false;
            SaveAndCancel.Visibility = Visibility.Visible;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Modify.Visibility = Visibility.Visible;
            //DateOfBirth.IsReadOnly = true;
            PhoneNum.IsReadOnly = true;
            Addr.IsReadOnly = true;
            SaveAndCancel.Visibility = Visibility.Collapsed;
            modifyInfo(sender, e, (App.Current as App).ConnectionString);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Modify.Visibility = Visibility.Visible;
            //DateOfBirth.IsReadOnly = true;
            PhoneNum.IsReadOnly = true;
            Addr.IsReadOnly = true;
            SaveAndCancel.Visibility = Visibility.Collapsed;
        }

        private void make_appointment_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Staff_MakeAppointment), staffDetailInfo);
        }
    }
}