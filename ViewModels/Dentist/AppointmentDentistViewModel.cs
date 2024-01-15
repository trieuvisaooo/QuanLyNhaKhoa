using QuanLyNhaKhoa.Models;
using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Dentist
{
    public class AppointmentDentistViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private AppointmentDentistView appointmentDentistView;

        public event PropertyChangedEventHandler PropertyChanged;

        public AppointmentDentistViewModel(int id, string name)
        {
            appointmentDentistView = new AppointmentDentistView(id, name);
            Id = appointmentDentistView.id;
            Name = appointmentDentistView.name;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
