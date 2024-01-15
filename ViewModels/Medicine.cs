namespace QuanLyNhaKhoa.ViewModels
{
    public class MedicineViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }

        public string ID
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


        public MedicineViewModel(string name, int count, int price)
        {
            Name = name;
            Count = count;
            Price = price;
            Total = Count * Price;
        }

        public MedicineViewModel()
        { }
    }
}
