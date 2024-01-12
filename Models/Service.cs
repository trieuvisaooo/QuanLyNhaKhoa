namespace QuanLyNhaKhoa.Models
{
    public class Service
    {
        public string ID
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public int Price
        {
            get; set;
        }

        public Service()
        {

        }

        public Service(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
