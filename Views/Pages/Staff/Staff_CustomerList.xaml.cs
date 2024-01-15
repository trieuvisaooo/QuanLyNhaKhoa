// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;
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
    public sealed partial class Staff_CustomerList : Page
    {
        public Staff_CustomerList()
        {
            this.InitializeComponent();
        }

        private void onClick(object sender, RoutedEventArgs e)
        {
            List<Staff_CustomerListViewModel> list = new List<Staff_CustomerListViewModel>();
            list = GetStaffInfoViewModel((App.Current as App).ConnectionString, mTextbox.Text.ToUpper());
            danhsach.ItemsSource = list;
        }

        public static void GetServerName()
        {
            string serverName = Environment.MachineName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        Debug.WriteLine("HELLO: " + serverName + "\\" + instanceName);
                        serverName = serverName + "\\" + instanceName;
                        break;
                    }
                }
            }
        }

        public List<Staff_CustomerListViewModel> GetStaffInfoViewModel(String connectionString, String ID)
        {
            List<Staff_CustomerListViewModel> list = new List<Staff_CustomerListViewModel>();
            String getStaffInfoQuery;
            if (ID != "")
                getStaffInfoQuery = "select MAKH, HOTEN, NGAYSINH, SDT, DIACHI from KHACH_HANG where HOTEN = '" + ID + "'";
            else
                getStaffInfoQuery = "select MAKH, HOTEN, NGAYSINH, SDT, DIACHI from KHACH_HANG";

            Debug.WriteLine(getStaffInfoQuery);

            String staffID = "";
            String staffName = "";
            String phoneNumber = "";
            DateOnly dateOfBirth = DateOnly.MinValue;
            String address = "";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getStaffInfoQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                staffID = reader.GetString(0);
                                staffName = reader.GetString(1);
                                dateOfBirth = DateOnly.FromDateTime(reader.GetDateTime(2));
                                phoneNumber = reader.GetString(3);
                                address = reader.GetString(4);


                                list.Add(new Staff_CustomerListViewModel(staffID, staffName, dateOfBirth, phoneNumber, address));
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
            var item = e.ClickedItem as Staff_CustomerListViewModel;
            
            if (item != null)
            {
                this.Frame.Navigate(typeof(Staff_CustomerDetailInfo), item);
            }

        }
    }
}
