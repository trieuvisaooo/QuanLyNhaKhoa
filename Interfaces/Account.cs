using System;

namespace QuanLyNhaKhoa.Interfaces
{
    public interface Account
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateOnly Birthday { get; set; }

        // if the info shown above matches the info in the database, return true
        // and accepts the `set` assignment of App.CurrentAccount
        public bool LogInQuery()
        {
            return true;
        }
    }
}
