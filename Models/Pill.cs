using System;

namespace QuanLyNhaKhoa.Models
{
    public class Pill
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public string Specification { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
