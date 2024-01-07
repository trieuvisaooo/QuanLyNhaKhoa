using System;

namespace QuanLyNhaKhoa.Models
{
    public class CustomerAccount : Interfaces.Account
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public int Status { get; set; } = 1;

    }
}
