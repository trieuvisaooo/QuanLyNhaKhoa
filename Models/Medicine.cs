using System;

namespace QuanLyNhaKhoa.Models
{
    public class Medicine
    {
        public string ID
        {
            get; set;
        } = Helpers.RandomId.GenerateRandomString();

        public string Name
        {
            get; set;
        }
        public int Count
        {
            get; set;
        }
        public int Price
        {
            get; set;
        }
        public int Total
        {
            get; set;
        }

        public string Unit
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public DateTime ExpiredDate { get; set; } = DateTime.Parse("2100-01-01");

        public Medicine()
        {

        }

        public Medicine(string name, int count, int price)
        {
            Name = name;
            Count = count;
            Price = price;
            Total = Count * Price;
        }
    }
}
