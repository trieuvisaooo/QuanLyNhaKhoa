using QuanLyNhaKhoa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Dentist
{
    public class DentistInforViewModel : INotifyPropertyChanged
    {
        private string _denID;
        private string _denName;
        private DateOnly _dateOfBirth;
        private string _phoneNum;
        private string _addr;

        public string DenID
        {
            get; set;
        }
        public string DenName
        {
            get; set;
        }

        public DateOnly DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
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

        public DentistInforViewModel(DentistAccount dentistAccount)
        {
            DenID = dentistAccount.Id;
            DenName = dentistAccount.Name;
            var date = dentistAccount.Birthday;
            DateOfBirth = DateOnly.FromDateTime(date);
            PhoneNum = dentistAccount.PhoneNumber;
            Addr = dentistAccount.Address;
        }

        public DentistInforViewModel GetDentistInfo(string connectionString, DentistInforViewModel dentistInfo)
        {
            //string DenID = "NS0001";
            var GetDentistInfoQuery = "select NS.MANS, NS.HOTEN, NS.NGAYSINH, NS.SDT, NS.DIACHI from NHA_SI NS " +
                             "where NS.MANS = '" + DenID + "'";


            try
            {
                using (var conn = new SqlConnection(@connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetDentistInfoQuery;
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dentistInfo.DenID = reader.GetString(0);
                                    dentistInfo.DenName = reader.GetString(1);
                                    var date = reader.GetDateTime(2);
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

