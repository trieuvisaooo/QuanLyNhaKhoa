using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }

        public Customer()
        {
            Id = "KH0061";
            Name = "Võ Quốc Trụ";
            DateOfBirth = new DateTime(1984, 04, 03);
            PhoneNum = "0957377131";
            Address = "1/1, Huyện ABC, Tỉnh DEF";
        }


    }
}
