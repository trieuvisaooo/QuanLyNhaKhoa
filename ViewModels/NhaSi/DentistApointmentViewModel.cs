using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System;

namespace QuanLyNhaKhoa.ViewModels.NhaSi
{
    public class DentistAppointmentViewModel : INotifyPropertyChanged
    {
        private string _denID;
        private string _denName;
        private DateOnly _appoDate;
        private TimeOnly _appoTime;

        public string DenID
        {
            get { return _denID; }
            set
            {
                _denID = value;
                NotifyPropertyChanged(nameof(DenID));
            }
        }

        public string DenName
        {
            get { return _denName; }
            set
            {
                _denName = value;
                NotifyPropertyChanged(nameof(DenName));
            }
        }

        public DateOnly AppoDate
        {
            get { return _appoDate; }
            set
            {
                _appoDate = value;
                NotifyPropertyChanged(nameof(AppoDate));
            }
        }

        public TimeOnly AppoTime
        {
            get { return _appoTime; }
            set
            {
                _appoTime = value;
                NotifyPropertyChanged(nameof(AppoTime));
            }
        }

        public ObservableCollection<DentistAppointmentViewModel> GetAppointments(string connectionString)
        {         
            string GetAppointmentQuery = "SELECT KH.MAKH, KH.HOTEN, LH.NGAYKHAM, LH.GIOKHAM FROM LICH_HEN LH JOIN KHACH_HANG KH ON LH.MAKH = KH.MAKH ORDER BY LH.NGAYKHAM DESC, LH.GIOKHAM DESC";

            var appointments = new ObservableCollection<DentistAppointmentViewModel>();
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
                                    var dentistAppointment = new DentistAppointmentViewModel();
                                    dentistAppointment.DenID = reader.GetString(0);
                                    dentistAppointment.DenName = reader.GetString(1);
                                    DateTime date = reader.GetDateTime(2);
                                    dentistAppointment.AppoDate = DateOnly.FromDateTime(date);
                                    TimeSpan time = reader.GetTimeSpan(3);
                                    dentistAppointment.AppoTime = TimeOnly.FromTimeSpan(time);

                                    appointments.Add(dentistAppointment);
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
