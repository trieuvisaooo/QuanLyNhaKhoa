using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.ViewModels.NhaSi;
using QuanLyNhaKhoa.Views.Pages;
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
        public DentistAppointment()
        {
            this.InitializeComponent();
            dentistInfo.GetDentistInfo((App.Current as App).ConnectionString, dentistInfo);
            AppointmentList.ItemsSource = dentistAppointmentViewModel.GetAppointments((App.Current as App).ConnectionString, dentistInfo.DenID);
        }

        private DentistAppointmentViewModel dentistAppointmentViewModel = new DentistAppointmentViewModel();
        private DentistInforViewModel dentistInfo = new DentistInforViewModel();

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DentistAddSchedule));
        }

        private void lvItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as DentistAppointment;

            if (item != null)
            {
                //this.Frame.Navigate(typeof(CusRecord), item);
            }
        }

    }
}
