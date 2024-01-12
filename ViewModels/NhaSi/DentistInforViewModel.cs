using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.NhaSi
{
    public class DentistInforViewModel : INotifyPropertyChanged
    {
        private string _dentistID;
        private string _dentistName;
        private DateOnly _dateOfBirth;
        private string _phoneNum;
        private string _addr;

        public string DentistID
        {
            get; set;
        }
        public string DentistName
        {
            get; set;
        }

        public DateOnly DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                NotifyPropertyChanged(nameof(DateOfBirth));
            }
        }
        public string PhoneNum
        {
            get
            {
                return _phoneNum;
            }
            set
            {
                _phoneNum = value;
                NotifyPropertyChanged(nameof(PhoneNum));
            }
        }
        public string Addr
        {
            get
            {
                return _addr;
            }
            set
            {
                _addr = value;
                NotifyPropertyChanged(nameof(Addr));
            }
        }

        public DentistInforViewModel()
        {

        }

        public DentistInforViewModel GetDentistInfo(string connectionString, DentistInforViewModel dentistInfo)
        {
            string DentistID = "NS0001";
            string GetDentistInfoQuery = "select MANS, HOTEN, NGAYSINH, SDT, DIACHI from NHA_SI " +
                                                "where MANS = " + "'" + DentistID + "'";

            try
            {
                using (var conn = new SqlConnection(@connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetDentistInfoQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dentistInfo.DentistID = reader.GetString(0);
                                    dentistInfo.DentistName = reader.GetString(1);
                                    DateTime date = reader.GetDateTime(2);
                                    dentistInfo.DateOfBirth = DateOnly.FromDateTime(date);
                                    dentistInfo.PhoneNum = reader.GetString(3);
                                    dentistInfo.Addr = reader.GetString(4);
                                }

                            }
                        }
                    }
                }
                return dentistInfo;

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

