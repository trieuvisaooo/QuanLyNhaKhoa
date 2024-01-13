using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using QuanLyNhaKhoa.Models;


namespace QuanLyNhaKhoa.ViewModels.Customer
{
    public class CustomerRecordViewModel : INotifyPropertyChanged
    {
        private string _recordID;
        private string _cusID;
        private string _denID;
        private string _denName;
        private string _description;
        private DateOnly _recordDate;
        private int _baseCost;
        private int _totalCost;
        private int _status;
        //private ObservableCollection<Medicine> _medicine = new ObservableCollection<Medicine>();
        private List<Models.Medicine> _medicines = new List<Models.Medicine>();
        private List<Service> _services = new List<Service>();

        public CustomerRecordViewModel()
        {

        }

        public CustomerRecordViewModel(string recordID)
        {
            _recordID = recordID;
        }


        public string RecordID
        {
            get
            {
                return _recordID;
            }
            set
            {
                _recordID = value;
                NotifyPropertyChanged(nameof(_recordID));
            }
        }

        public string CusID
        {
            get
            {
                return _cusID;
            }
            set
            {
                _cusID = value;
                NotifyPropertyChanged(nameof(_cusID));
            }
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
            get
            {
                return _description;
            }
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

        public int BaseCost
        {
            get
            {
                return _baseCost;
            }
            set
            {
                _baseCost = value;
                NotifyPropertyChanged(nameof(_baseCost));
            }
        }

        public int InvoiceStatus
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged(nameof(_status));
            }
        }

        public List<Models.Medicine> Medicines
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
            var getRecordQuery = "SELECT BA.MABA, NS.MANS, NS.HOTEN, BA.MOTA, BA.NGAYKHAM, HD.PHIKHAM, HD.TONGTIEN, HD.TINHTRANG FROM BENH_AN BA JOIN NHA_SI NS ON BA.NHASITHUCHIEN = NS.MANS " +
                                                " JOIN HOA_DON HD ON HD.MABA = BA.MABA " +
                                                "where BA.MAKH = " + "'" + cusID + "' ORDER BY BA.NGAYKHAM DESC";


            var records = new ObservableCollection<CustomerRecordViewModel>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = getRecordQuery;
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var CustomerRecord = new CustomerRecordViewModel();
                                    CustomerRecord.RecordID = reader.GetString(0);
                                    CustomerRecord.DenID = reader.GetString(1);
                                    CustomerRecord.DenName = reader.GetString(2);
                                    CustomerRecord.Description = reader.GetString(3);
                                    var date = reader.GetDateTime(4);
                                    CustomerRecord.RecordDate = DateOnly.FromDateTime(date);
                                    CustomerRecord.BaseCost = reader.GetInt32(5);
                                    CustomerRecord.TotalCost = reader.GetInt32(6);
                                    CustomerRecord.InvoiceStatus = reader.GetInt32(7);

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

        public ObservableCollection<CustomerRecordViewModel> GetRecordsByDenName(string connectionString, string cusID, string denName)
        {
            //string CusID = "KH0002";
            var getRecordQuery = "SELECT BA.MABA, NS.MANS, NS.HOTEN, BA.MOTA, BA.NGAYKHAM, HD.PHIKHAM, HD.TONGTIEN, HD.TINHTRANG FROM BENH_AN BA JOIN NHA_SI NS ON BA.NHASITHUCHIEN = NS.MANS " +
                                    "JOIN HOA_DON HD ON HD.MABA = BA.MABA " +
                                    "WHERE BA.MAKH = " + "'" + cusID + "' AND NS.HOTEN = N" + "'" + denName + "' ORDER BY BA.NGAYKHAM DESC";



            var records = new ObservableCollection<CustomerRecordViewModel>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = getRecordQuery;
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var CustomerRecord = new CustomerRecordViewModel();
                                    CustomerRecord.RecordID = reader.GetString(0);
                                    CustomerRecord.DenID = reader.GetString(1);
                                    CustomerRecord.DenName = reader.GetString(2);
                                    CustomerRecord.Description = reader.GetString(3);
                                    var date = reader.GetDateTime(4);
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

        public List<Models.Medicine> GetMedicine(string connectionString)
        {
            Medicines.Clear();
            var getMedicineQuery = "SELECT T.MATHUOC, T.TENTHUOC, CTDT.SOLUONG, T.DONVITINH, T.DONGIA FROM CT_DON_THUOC CTDT JOIN THUOC T ON CTDT.MATHUOC = T.MATHUOC WHERE CTDT.MABA = " +
                                        "'" + RecordID + "'";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = getMedicineQuery;
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var medicine = new Models.Medicine();
                                    medicine.ID = reader.GetString(0);
                                    medicine.Name = reader.GetString(1);
                                    medicine.Count = reader.GetInt32(2);
                                    medicine.Unit = reader.GetString(3);
                                    medicine.Price = reader.GetInt32(4);
                                    medicine.Total = medicine.Count * medicine.Price;
                                    Medicines.Add(medicine);
                                }
                            }
                        }
                    }
                }
                return Medicines;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine($"Exception: {eSql.Message}");
            }
            return null;

        }

        public List<Service> GetService(string connectionString)
        {
            Services.Clear();
            var getServiceQuery = "SELECT DV.MADV, DV.TENDICHVU, DV.DONGIA FROM PHIEU_DV PDV JOIN DICH_VU DV ON PDV.MADV = DV.MADV WHERE PDV.MABA = " +
                                        "'" + RecordID + "'";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = getServiceQuery;
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var service = new Service();
                                    service.ID = reader.GetString(0);
                                    service.Name = reader.GetString(1);
                                    service.Price = reader.GetInt32(2);
                                    Services.Add(service);
                                }
                            }
                        }
                    }
                }
                return Services;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine($"Exception: {eSql.Message}");
            }
            return null;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
