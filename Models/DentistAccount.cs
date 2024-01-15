using System;

namespace QuanLyNhaKhoa.Models
{
    public class DentistAccount : Interfaces.Account
    {
        private string _name;
        private string _id;
        private string _phoneNumber;
        private string _password;
        private string _address;
        private DateTime _birthday;
        private string _chuyenmon;
        private int _status;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
            }
        }
        public int Status { get; set; } = 1;

    }
}
