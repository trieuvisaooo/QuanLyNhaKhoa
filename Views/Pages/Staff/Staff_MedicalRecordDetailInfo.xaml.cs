// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels.Receptionist;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Staff_MedicalRecordDetailInfo : Page
    {
        private Staff_MedicalRecordViewModels MRViewModel;
        public Staff_DetailMedicalRecordViewModel MRDViewModel;

        public Staff_MedicalRecordDetailInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MRViewModel = e.Parameter as Staff_MedicalRecordViewModels;

            string _mrID;
            string _description = "";
            string _dentistID = "";
            string _dentistName = "";
            DateOnly _dateVisit = DateOnly.MinValue;
            List<Models.Medicine> _medic = new List<Models.Medicine>();
            List<Models.Service> _serviceUsed = new List<Models.Service>();
            string _invoiceID = "";
            int _totalPayment = 0;
            string _paymentStatus = "";

            _mrID = MRViewModel._MrID;

            string query1 = $"SELECT BA.MOTA, BA.NGAYKHAM FROM BENH_AN BA WHERE BA.MABA = '{_mrID}'";
            string query2 = $"SELECT NS.MANS, NS.HOTEN FROM NHA_SI NS JOIN BENH_AN BA ON NS.MANS = BA.NHASITHUCHIEN WHERE BA.MABA = '{_mrID}'";
            string query3 = $"SELECT T.TENTHUOC, CTDT.DONGIA, CTDT.SOLUONG" +
                $" FROM BENH_AN BA JOIN CT_DON_THUOC CTDT ON BA.MABA = CTDT.MABA JOIN THUOC T ON T.MATHUOC = CTDT.MATHUOC WHERE BA.MABA = '{_mrID}'";

            string query4 = $"SELECT MAHD, TONGTIEN, TINHTRANG FROM HOA_DON HD JOIN BENH_AN BA ON BA.MABA = HD.MABA WHERE BA.MABA = '{_mrID}'";
            string query5 = $"SELECT DV.TENDICHVU, DV.DONGIA FROM BENH_AN BA JOIN PHIEU_DV PDV ON BA.MABA = PDV.MABA JOIN DICH_VU DV ON PDV.MADV = DV.MADV WHERE BA.MABA = '{_mrID}'";
            try
            {
                using (var conn = new SqlConnection((App.Current as App).ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query3, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _medic.Add(new Models.Medicine(reader.GetString(0), reader.GetInt32(2), reader.GetInt32(1)));
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand(query2, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _dentistID = reader.GetString(0);
                                _dentistName = reader.GetString(1);
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand(query1, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _description = reader.GetString(0);
                                _dateVisit = DateOnly.FromDateTime(reader.GetDateTime(1));
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand(query4, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _invoiceID = reader.GetString(0);
                                _totalPayment = reader.GetInt32(1);
                                _paymentStatus = (reader.GetInt32(2) == 1 ? "Đầy Đủ" : "Chưa Đầy Đủ");
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand(query5, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _serviceUsed.Add(new Models.Service(reader.GetString(0), reader.GetInt32(1)));
                            }
                        }
                    }

                    MRDViewModel = new Staff_DetailMedicalRecordViewModel(_mrID, _description, _dentistID, _dentistName, _dateVisit, _medic, _serviceUsed, _invoiceID, _totalPayment, _paymentStatus);

                }

            }
            catch (Exception eSql)
            {
                Debug.WriteLine($"Exception: {eSql.Message}");
            }

            donthuoc.ItemsSource = MRDViewModel.MedicName;
            dichvu.ItemsSource = MRDViewModel.ServiceUsed;
        }
        private void ViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Staff_InvoiceDetail), MRDViewModel);
        }

    }
}
