using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Customer
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly Models.CustomerAccount _customer = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get
            {
                return _customer.Id;
            }
            set
            {
                _customer.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get
            {
                return _customer.Name;
            }
            set
            {
                _customer.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public CustomerViewModel(Models.CustomerAccount customer)
        {
            Update(customer);
        }

        // property change function
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update(Models.CustomerAccount customer)
        {
            Id = customer.Id;
            Name = customer.Name;
        }
    }
}
