using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.UI.Xaml.Data;


namespace QuanLyNhaKhoa.Models
{
    public class Appointments : INotifyPropertyChanged
    {
        public string AppoID { get; set; }
        public string CusID { get; set; }
        public string Date { get; set; }
        public string Time { get; set;}
        public string DenID { get; set; }
        public string DenName { get; set;}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Appointments> GetAppoints(string connectionString)
        {
            // QUERY DATA

            const string GetAppointmentsQuery = "SELECT  " +
               " UnitPrice, UnitsInStock, Products.CategoryID " +
               " FROM CUOC_HEN CH JOIN NHA_SI NS ON CH.NHASI = NS.MANS" +
               " WHERE MAKH = `KH0001` ";

            var appointments = new ObservableCollection<Appointments>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetAppointmentsQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var appointment = new Appointments();
                                    appointment.AppoID = reader.GetString(1);
                                    appointment.Date = reader.GetString(2);
                                    appointment.Time = reader.GetString(3);
                                    appointment.DenID = reader.GetString(4);
                                    appointment.DenName = reader.GetString(5);

                                    appointments.Add(appointment);   
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
    }
}
