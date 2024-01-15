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
    public sealed partial class Staff_InvoiceDetail : Page
    {
        public Staff_DetailMedicalRecordViewModel MR;

        public Staff_InvoiceDetail()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MR = e.Parameter as Staff_DetailMedicalRecordViewModel;
            medicine_list.ItemsSource = MR.MedicName;
            service_list.ItemsSource = MR.ServiceUsed;
            
        }
        private async void modify_Click(object sender, RoutedEventArgs e)
        {
            int idx;
            if (status_txtb.Text == "Đầy Đủ")
            {
                idx = 1;
            }   
            else
            {
                idx = 0;
            }

            Debug.WriteLine(idx);
            string updateCommand = $"UPDATE HOA_DON SET TINHTRANG = {idx} WHERE HOA_DON.MAHD = '{MR.InvoiceID}'";
            Debug.WriteLine(updateCommand);
            SqlConnection con = new SqlConnection((App.Current as App).ConnectionString);

            try
            {
                con.Open();
                SqlCommand cmnd = new SqlCommand(updateCommand, con);
                cmnd.ExecuteNonQuery();

                ContentDialog MadeAppointmentDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Cập nhật hóa đơn",
                    Content = "Bạn đã cập nhật hóa đơn thành công!",
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
                    Title = "Cập nhật hóa đơn",
                    Content = "Cập nhật hóa đơn thất bại",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await FailDialog.ShowAsync();
            }
            finally
            {
                con.Close();
            }

        }
    }
}
