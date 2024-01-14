using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Windows.ApplicationModel.Appointments;

namespace QuanLyNhaKhoa.Views
{
    public partial class DentistAddSchedule : Page
    {
        private DentistInforViewModel dentist = new DentistInforViewModel();
        internal ViewModels.BriefInfoViewModel infoViewModel = new((App.Current as App).CurrentAccount.StoredAccount);
        public DentistAddSchedule()
        {
            this.InitializeComponent();
            dentist = dentist.GetDentistInfo((App.Current as App).ConnectionString, dentist);
        }

        private async void addSchedule_Click(object sender, RoutedEventArgs e)
        {
            //var builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @".\SQLSERVER";
            //builder.InitialCatalog = "QLPK";
            //builder.IntegratedSecurity = true;
            //string connectionString = builder.ConnectionString;
            string connectionString = (App.Current as App).ConnectionString;
            SqlConnection con = new SqlConnection(@connectionString);
            Debug.WriteLine(@connectionString);
            try
            {
                con.Open();

                //dentist.DenID = "NS0001";
                string insert_statement = "EXEC sp_ThemLichCaNhanNhaSi '" + infoViewModel.Id + "', '" + Date.Date + "', '" + StartTime.Time + "', '" + EndTime.Time + "'";
                SqlCommand cmnd = new SqlCommand(insert_statement, con);
                cmnd.ExecuteNonQuery();
                this.Frame.Navigate(typeof(DentistAppointment));
                ContentDialog AddScheduleDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Thêm lịch cá nhân",
                    Content = "Bạn đã thêm lịch cá nhân thành công!",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await AddScheduleDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                ContentDialog FailDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Thêm lịch cá nhân",
                    Content = "Thêm lịch cá nhân thất bại!",
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
            this.Frame.Navigate(typeof(DentistAppointment));
        }
    }
}