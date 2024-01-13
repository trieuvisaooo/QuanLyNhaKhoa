namespace QuanLyNhaKhoa.Models
{
    public class AppointmentDentistView
    {
        public int id = 0;
        public string name = "No Name";

        public AppointmentDentistView(int id, string name)
        {
            this.id = id;
            if (name is not null) { this.name = name; }
        }
    }
}
