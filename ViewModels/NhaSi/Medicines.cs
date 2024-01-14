using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels
{
    public class Medicines
    {
        public string ID
        {
            get; set;
        }

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

        public Medicines()
        {

        }

        public Medicines(string name, int count, int price)
        {
            Name = name;
            Count = count;
            Price = price;
            Total = Count * Price;
        }
    }
}