﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels
{
    public class Service
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Service(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
