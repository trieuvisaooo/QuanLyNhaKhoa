using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Dentist
{
    public class AddNewRecordViewModel : INotifyPropertyChanged
    {
        private string _denID;
        private string _cusName;
        private string _phoneNum;
        private string _medicine;
        private string _service;
        public string DenID
        {
            get
            {
                return _denID;
            }
            set
            {
                _denID = value;
                NotifyPropertyChanged(nameof(DenID));
            }
        }

        public string CusName
        {
            get
            {
                return _cusName;
            }
            set
            {
                _cusName = value;
                NotifyPropertyChanged(nameof(CusName));
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

        public string Medicine
        {
            get
            {
                return _medicine;
            }
            set
            {
                _medicine = value;
                NotifyPropertyChanged(nameof(Medicine));
            }
        }

        public string Service
        {
            get
            {
                return _service;
            }
            set
            {
                _service = value;
                NotifyPropertyChanged(nameof(Service));
            }
        }

        public AddNewRecordViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
