using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.Models
{
    public class Medicine
    {
        public string MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int MedicineQuantity { get; set; }
        public string MedicineUnit { get; set; }


        public Medicine() { } 

    }
}
