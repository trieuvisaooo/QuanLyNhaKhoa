using QuanLyNhaKhoa.Interfaces;
using System;

namespace QuanLyNhaKhoa.Models
{
    public class AdministratorAccount : Interfaces.Account
    {
        string Account.PhoneNumber { get; set; }
        string Account.Password { get; set; }
        string Account.Address { get; set; }
        DateOnly Account.Birthday { get; set; }
    }
}
