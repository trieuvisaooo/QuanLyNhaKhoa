using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.ViewModels.NhaSi;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Windows.ApplicationModel.Appointments;

namespace QuanLyNhaKhoa.Views
{
    public partial class DentistAppointment : Page
    {
        private DentistAppointmentViewModel dentistAppointmentViewModel = new DentistAppointmentViewModel();
        private DentistInforViewModel dentistInfo = new DentistInforViewModel();

        public DentistAppointment()
        {
            this.InitializeComponent();
            dentistInfo.GetDentistInfo((App.Current as App).ConnectionString, dentistInfo);
            AppointmentList.ItemsSource = dentistAppointmentViewModel.GetAppointments((App.Current as App).ConnectionString);

            // Tạo danh sách các cuộc hẹn mẫu
            //var appointments = new ObservableCollection<DentistAppointmentViewModel>
            //{
            //    new DentistAppointmentViewModel { DenName = "Nguyễn Văn A", DenID = "KH0001", AppoTime = TimeOnly.FromDateTime(DateTime.Now), AppoDate = DateOnly.FromDateTime(DateTime.Now) },
            //    new DentistAppointmentViewModel { DenName = "Trần Thị B", DenID = "KH0002", AppoTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)), AppoDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)) },
            //    // Thêm nhiều cuộc hẹn mẫu khác nếu cần
            //};

            // Gán danh sách cuộc hẹn mẫu cho ItemsSource của ListView
            //AppointmentList.ItemsSource = appointments;
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DentistAddSchedule));
        }
    }
}
