// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels.Dentist;
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
    public sealed partial class MedicalRecord : Page
    {
        public MedicalRecord()
        {
            this.InitializeComponent();
        }

        private void onClick(object sender, RoutedEventArgs e)
        {
            List<MedicalRecordViewModels> list = new List<MedicalRecordViewModels> ();
            list = GetMedicalRecordViewModels((App.Current as App).ConnectionString, mTextbox.Text.ToUpper());
            danhsach.ItemsSource = list;
        }

        private List<MedicalRecordViewModels> GetMedicalRecordViewModels(string connectionString, string ID)
        {
            List<MedicalRecordViewModels> list = new List<MedicalRecordViewModels>();
            String getPatientNameQuery = "SELECT BA.MABA, BA.MAKH, KH.HOTEN FROM BENH_AN BA JOIN KHACH_HANG KH ON BA.MAKH = KH.MAKH";

            if (!string.IsNullOrEmpty(ID))
            {
                getPatientNameQuery += " WHERE KH.HOTEN = @ID";
            }

            string _mrID;
            string _cusID;
            string _cusName;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getPatientNameQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _mrID = reader.GetString(0);
                                _cusID = reader.GetString(1);
                                _cusName = reader.GetString(2);

                                list.Add(new MedicalRecordViewModels(_mrID, _cusID, _cusName));
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine($"Exception: {eSql.Message}");
            }
            return null;
        }


        private void lvItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as MedicalRecordViewModels;

            if (item != null)
            {
                this.Frame.Navigate(typeof(MedicalRecordDetailInfo), item);
            }

        }
        private void onCreateNewRecord(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof());
        }

    }
}
