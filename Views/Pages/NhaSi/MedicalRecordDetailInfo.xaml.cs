// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public sealed partial class MedicalRecordDetailInfo : Page
    {
        private MedicalRecordViewModels MRViewModel;
        public DetailMedicalRecordViewModel MRDViewModel;

        public MedicalRecordDetailInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MRViewModel = e.Parameter as MedicalRecordViewModels;

            string _mrID = MRViewModel._MrID;

            try
            {
                using (var conn = new SqlConnection((App.Current as App).ConnectionString))
                {
                    conn.Open();

                    string _description = GetDescription(conn, _mrID);
                    DateOnly _dateVisit = GetDateVisit(conn, _mrID);
                    List<Medicine> _medic = GetMedicines(conn, _mrID);
                    (string _dentistID, string _dentistName) = GetDentistInfo(conn, _mrID);
                    (string _invoiceID, int _totalPayment, string _paymentStatus) = GetInvoiceInfo(conn, _mrID);
                    List<Service> _serviceUsed = GetServicesUsed(conn, _mrID);

                    MRDViewModel = new DetailMedicalRecordViewModel(_mrID, _description, _dentistID, _dentistName, _dateVisit, _medic, _serviceUsed, _invoiceID, _totalPayment, _paymentStatus);
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine($"Exception: {eSql.Message}");
                // Add appropriate error handling here.
            }

            donthuoc.ItemsSource = MRDViewModel.MedicName;
            dichvu.ItemsSource = MRDViewModel.ServiceUsed;
        }

        private string GetDescription(SqlConnection conn, string mrID)
        {
            string query = $"SELECT BA.MOTA FROM BENH_AN BA WHERE BA.MABA = @mrID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@mrID", mrID);
                return cmd.ExecuteScalar().ToString();
            }
        }

        private DateOnly GetDateVisit(SqlConnection conn, string mrID)
        {
            string query = $"SELECT BA.NGAYKHAM FROM BENH_AN BA WHERE BA.MABA = @mrID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@mrID", mrID);
                return DateOnly.FromDateTime((DateTime)cmd.ExecuteScalar());
            }
        }

        private List<Medicine> GetMedicines(SqlConnection conn, string mrID)
        {
            string query = $"SELECT T.TENTHUOC, CTDT.DONGIA, CTDT.SOLUONG FROM BENH_AN BA JOIN CT_DON_THUOC CTDT ON BA.MABA = CTDT.MABA JOIN THUOC T ON T.MATHUOC = CTDT.MATHUOC WHERE BA.MABA = @mrID";
            List<Medicine> medicines = new List<Medicine>();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@mrID", mrID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        medicines.Add(new Medicine(reader.GetString(0), reader.GetInt32(2), reader.GetInt32(1)));
                    }
                }
            }
            return medicines;
        }

        private (string, string) GetDentistInfo(SqlConnection conn, string mrID)
        {
            string query = $"SELECT NS.MANS, NS.HOTEN FROM NHA_SI NS JOIN BENH_AN BA ON NS.MANS = BA.NHASITHUCHIEN WHERE BA.MABA = @mrID";
            string dentistID = "";
            string dentistName = "";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@mrID", mrID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dentistID = reader.GetString(0);
                        dentistName = reader.GetString(1);
                    }
                }
            }
            return (dentistID, dentistName);
        }

        private (string, int, string) GetInvoiceInfo(SqlConnection conn, string mrID)
        {
            string query = $"SELECT MAHD, TONGTIEN, TINHTRANG FROM HOA_DON HD JOIN BENH_AN BA ON BA.MABA = HD.MABA WHERE BA.MABA = @mrID";
            string invoiceID = "";
            int totalPayment = 0;
            string paymentStatus = "";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@mrID", mrID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoiceID = reader.GetString(0);
                        totalPayment = reader.GetInt32(1);
                        paymentStatus = (reader.GetInt32(2) == 1 ? "Đầy Đủ" : "Chưa Đầy Đủ");
                    }
                }
            }
            return (invoiceID, totalPayment, paymentStatus);
        }

        private List<Service> GetServicesUsed(SqlConnection conn, string mrID)
        {
            string query = $"SELECT DV.TENDICHVU, DV.DONGIA FROM BENH_AN BA JOIN PHIEU_DV PDV ON BA.MABA = PDV.MABA JOIN DICH_VU DV ON PDV.MADV = DV.MADV WHERE BA.MABA = @mrID";
            List<Service> services = new List<Service>();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@mrID", mrID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add(new Service(reader.GetString(0), reader.GetInt32(1)));
                    }
                }
            }
            return services;
        }

        private void ViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Staff_InvoiceDetail), MRDViewModel);
        }

    }    
}
