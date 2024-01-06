using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace QuanLyNhaKhoa.ViewModels
{
    public class CustomerAppointmentViewModel : INotifyPropertyChanged
    {
        private string _appoID;
        private string _denName;
        private string _denID;
        private DateOnly _appoDate;
        private TimeOnly _appoTime;

        public string AppoID
        {
            get
            {
                return _appoID;
            }
            set
            {
                _appoID = value;
                NotifyPropertyChanged(nameof(_appoID));
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
                NotifyPropertyChanged(nameof(DenName));
            }
        }
        public string DenID
        {
            get { return _denID; } 
            set {  
                _denID = value; 
                NotifyPropertyChanged(nameof(DenID));
            }
        }
        public DateOnly AppoDate
        {
            get { 
                return _appoDate;
            } 
            set { 
                _appoDate = value;
                NotifyPropertyChanged(nameof(AppoDate));
            }
        }
        public TimeOnly AppoTime
        {
            get
            {
                return _appoTime;
            } 
            set
            {
                _appoTime = value;
                NotifyPropertyChanged(nameof(AppoTime));
            }
        }


        public ObservableCollection<CustomerAppointmentViewModel> GetAppointments(string connectionString, string cusID)
        {
            //string CusID = "KH0002";
            string GetAppointmentQuery = "select LH.MALICHHEN, NS.HOTEN, NS.MANS, LH.GIOKHAM, LH.NGAYKHAM from LICH_HEN LH JOIN NHA_SI NS ON LH.NHASI = NS.MANS " +
                                                "where LH.MAKH = " + "'" + cusID + "' order by LH.NGAYKHAM desc, LH.GIOKHAM desc";

            var appointments = new ObservableCollection<CustomerAppointmentViewModel>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetAppointmentQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var CustomerAppointment = new CustomerAppointmentViewModel();
                                    CustomerAppointment.AppoID = reader.GetString(0);
                                    CustomerAppointment.DenName = reader.GetString(1);
                                    CustomerAppointment.DenID = reader.GetString(2);
                                    TimeSpan time = reader.GetTimeSpan(3);
                                    CustomerAppointment.AppoTime = TimeOnly.FromTimeSpan(time);
                                    DateTime date = reader.GetDateTime(4);
                                    CustomerAppointment.AppoDate = DateOnly.FromDateTime(date);


                                    appointments.Add(CustomerAppointment);
                                }
                            }
                        }
                    }
                }
                return appointments;
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
