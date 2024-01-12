using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Administrator
{
    public class AdministratorViewModel : INotifyPropertyChanged
    {
        private readonly Models.AdministratorAccount _Administrator = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get
            {
                return _Administrator.Id;
            }
            set
            {
                _Administrator.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get
            {
                return _Administrator.Name;
            }
            set
            {
                _Administrator.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public AdministratorViewModel(Models.AdministratorAccount Administrator)
        {
            Update(Administrator);
        }

        // property change function
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update(Models.AdministratorAccount Administrator)
        {
            Id = Administrator.Id;
            Name = Administrator.Name;
        }
    }
}
