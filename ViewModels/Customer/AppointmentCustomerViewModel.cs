using QuanLyNhaKhoa.Models;
using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Customer
{
    public class AppointmentCustomerViewModel : INotifyPropertyChanged
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

        private AppointmentCustomerView appointmentCustomerView;

        public event PropertyChangedEventHandler PropertyChanged;

        public AppointmentCustomerViewModel(int id, string name)
        {
            appointmentCustomerView = new AppointmentCustomerView(id, name);
            Id = appointmentCustomerView.id;
            Name = appointmentCustomerView.name;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
