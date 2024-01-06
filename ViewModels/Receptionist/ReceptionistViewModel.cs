using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Receptionist
{
    public class ReceptionistViewModel : INotifyPropertyChanged
    {
        private readonly Models.ReceptionistView _receptionist = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get
            {
                return _receptionist.Id;
            }
            set
            {
                _receptionist.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get
            {
                return _receptionist.Name;
            }
            set
            {
                _receptionist.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ReceptionistViewModel(Models.ReceptionistView receptionist)
        {
            Update(receptionist);
        }

        // property change function
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update(Models.ReceptionistView receptionist)
        {
            Id = receptionist.Id;
            Name = receptionist.Name;
        }
    }
}
