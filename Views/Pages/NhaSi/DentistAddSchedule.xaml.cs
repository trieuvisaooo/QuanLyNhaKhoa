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
    public partial class DentistAddSchedule : Page
    {
        private DentistAddScheduleViewModel dentistAddScheduleViewModel = new DentistAddScheduleViewModel();

        public DentistAddSchedule()
        {
            this.InitializeComponent();
            this.DataContext = dentistAddScheduleViewModel;
        }

        private void AddScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            // Code to handle the click event of AddAddScheduleButton
            // This will add the Add schedule to the database
        }
    }
}
