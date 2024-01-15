namespace QuanLyNhaKhoa.ViewModels
{
    public class ServiceViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public ServiceViewModel(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public ServiceViewModel()
        {

        }
    }
}
