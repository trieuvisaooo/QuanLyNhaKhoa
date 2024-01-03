using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;


namespace QuanLyNhaKhoa.ViewModels
{
    public class CustomerRecordViewModel : INotifyPropertyChanged
    {
        private string _recordID;
        private string _denID;
        private string _denName;
        private string _description;
        private DateOnly _recordDate;
        private int _totalCost;
        //private ObservableCollection<Medicine> _medicine = new ObservableCollection<Medicine>();
        private List<Medicine> _medicines = new List<Medicine>();
        private List<Service> _services = new List<Service>();
        

        public string RecordID
        {
            get; set;
        }
        public string DenID
        {
            get
            {
                return _denID;
            }
            set
            {
                _denID = value;
                NotifyPropertyChanged(nameof(_denID));
            }
        }

        public string DenName
        {
            get
            {
                return _denName;
            }
            set
            {
                _denName = value;
                NotifyPropertyChanged(nameof(_denName));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged(nameof(_description));
            }
        }
        public DateOnly RecordDate
        {
            get
            {
                return _recordDate;
            }
            set
            {
                _recordDate = value;
                NotifyPropertyChanged(nameof(_recordDate));
            }
        }

        public int TotalCost
        {
            get
            {
                return _totalCost;
            }
            set
            {
                _totalCost = value;
                NotifyPropertyChanged(nameof(_totalCost));
            }
        }

        public List<Medicine> Medicines
        {
            get
            {
                return _medicines;
            }

            set 
            { 
                _medicines = value;
                NotifyPropertyChanged(nameof(_medicines));
            }
        }

        public List<Service> Services
        {
            get
            {
                return _services;
            }

            set
            {
                _services = value;
                NotifyPropertyChanged(nameof(_services));
            }
        }


        public ObservableCollection<CustomerRecordViewModel> GetRecords(string connectionString, string cusID)
        {
            //string CusID = "KH0002";
            string getRecordQuery = "SELECT BA.MABA, NS.MANS, NS.HOTEN, BA.MOTA, BA.NGAYKHAM, HD.TONGTIEN FROM BENH_AN BA JOIN NHA_SI NS ON BA.NHASITHUCHIEN = NS.MANS " +
                                                " JOIN HOA_DON HD ON HD.MABA = BA.MABA " +    
                                                "where BA.MAKH = " + "'" + cusID + "'";
            

            var records = new ObservableCollection<CustomerRecordViewModel>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = getRecordQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var CustomerRecord = new CustomerRecordViewModel();
                                    CustomerRecord.RecordID = reader.GetString(0);
                                    CustomerRecord.DenID = reader.GetString(1);
                                    CustomerRecord.DenName = reader.GetString(2);
                                    CustomerRecord.Description = reader.GetString(3);
                                    DateTime date = reader.GetDateTime(4);
                                    CustomerRecord.RecordDate = DateOnly.FromDateTime(date);
                                    CustomerRecord.TotalCost = reader.GetInt32(5);

                                    records.Add(CustomerRecord);
                                }
                            }
                        }
                    }
                }
                return records;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine($"Exception: {eSql.Message}");
            }
            return null;
        }

        public void GetMedicine(string connectionString, ObservableCollection<CustomerRecordViewModel> records)
        {

            foreach (var item in records)
            {
                string getMedicineQuery = "SELECT T.MATHUOC, T.TENTHUOC, CTDT.SOLUONG, T.DONVITINH FROM CT_DON_THUOC CTDT JOIN THUOC T ON CTDT.MATHUOC = T.MATHUOC WHERE CTDT.MABA = " +
                                            "'" + item.RecordID + "'";

                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = getMedicineQuery;
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        var medicine = new Medicine();
                                        medicine.MedicineId = reader.GetString(0);
                                        medicine.MedicineName = reader.GetString(1);
                                        medicine.MedicineQuantity = reader.GetInt32(2);
                                        medicine.MedicineUnit = reader.GetString(3);
                                        item.Medicines.Add(medicine);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception eSql)
                {
                    Debug.WriteLine($"Exception: {eSql.Message}");
                }

            }

        }

        public void GetService(string connectionString, ObservableCollection<CustomerRecordViewModel> records)
        {

            foreach (var item in records)
            {
                string getServiceQuery = "SELECT DV.MADV, DV.TENDICHVU, DV.DONGIA FROM PHIEU_DV PDV JOIN DICH_VU DV ON PDV.MADV = DV.MADV WHERE PDV.MABA = " +
                                            "'" + item.RecordID + "'";

                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = getServiceQuery;
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        var service = new Service();
                                        service.ServiceID = reader.GetString(0);
                                        service.ServiceName = reader.GetString(1);
                                        service.ServiceCost= reader.GetInt32(2);
                                        item.Services.Add(service);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception eSql)
                {
                    Debug.WriteLine($"Exception: {eSql.Message}");
                }

            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
