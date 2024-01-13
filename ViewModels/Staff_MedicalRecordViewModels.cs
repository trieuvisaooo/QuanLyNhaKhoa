using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels
{
    public class Staff_MedicalRecordViewModels : INotifyPropertyChanged
    {

        private string _mrID;
        private string _cusID;
        private string _cusName;

        public string _MrID
        {
            set
            {
                _mrID = value;
                OnPropertyChanged(nameof(_mrID));

            }
            get { return _mrID; }
        }
        public string _CusID
        {
            set
            {
                _cusID = value;
                OnPropertyChanged(nameof(_cusID));

            }
            get { return _cusID; }
        }
        public string _CusName
        {
            set
            {
                _cusName = value;
                OnPropertyChanged(nameof(_cusName));
            }
            get { return _cusName; }
        }


        public Staff_MedicalRecordViewModels(string mrID, string cusID, string cusName)
        {
            _MrID = mrID;
            _CusID = cusID;
            _CusName = cusName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
