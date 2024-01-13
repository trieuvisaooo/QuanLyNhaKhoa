namespace QuanLyNhaKhoa.Models
{
    public class AppointmentCustomerView
    {
        public int id = 0;
        public string name = "No Name";

        public AppointmentCustomerView(int id, string name)
        {
            this.id = id;
            if (name is not null) { this.name = name; }
        }
    }
}
