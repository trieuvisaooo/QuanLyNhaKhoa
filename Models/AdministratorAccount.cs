using QuanLyNhaKhoa.Interfaces;
using System;

namespace QuanLyNhaKhoa.Models
{
    public class AdministratorAccount : Interfaces.Account
    {
        public string Account.PhoneNumber { get; set; }
        public string Account.Password { get; set; }
        public string Account.Address { get; set; }
        DateOnly Account.Birthday { get; set; }
    }
}
