using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels
{
    public class Staff_CustomerListViewModel : INotifyPropertyChanged
    {
        private string cusID;
        private string cusName;
        private DateOnly dateOfBirth;
        private string phoneNumber;
        private string address;

        public string CusID
        {
            get
            {
                return cusID;
            }
            set
            {
                cusID = value;
                OnPropertyChanged(nameof(cusID));
            }
        }
        public string CusName
        {
            get
            {
                return cusName;
            }
            set
            {
                cusName = value;
                OnPropertyChanged(nameof(cusName));
            }
        }
        public DateOnly DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged(nameof(dateOfBirth));
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(phoneNumber));
            }
        }
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
                OnPropertyChanged(nameof(address));
            }
        }

        public Staff_CustomerListViewModel(string ID, string Name, DateOnly dob, string phonenum, string addr)
        {
            cusID = ID;
            cusName = Name;
            dateOfBirth = dob;
            phoneNumber = phonenum;
            address = addr;
        }

        public Staff_CustomerListViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
