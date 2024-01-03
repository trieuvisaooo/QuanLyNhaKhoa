using QuanLyNhaKhoa.Interfaces;
using System;

namespace QuanLyNhaKhoa.Models
{
    public class CustomerAccount : Interfaces.Account
    {
        string Account.ID { get; set; }
        string Account.Name { get; set; }
        string Account.PhoneNumber { get; set; }
        string Account.Password { get; set; }
        string Account.Address { get; set; }
        DateOnly Account.Birthday { get; set; }
    }
}
