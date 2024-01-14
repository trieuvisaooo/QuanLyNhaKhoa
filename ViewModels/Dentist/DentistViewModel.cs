using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Dentist
{
    public class DentistViewModel : INotifyPropertyChanged
    {
        private readonly Models.DentistAccount _Dentist = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get
            {
                return _Dentist.Id;
            }
            set
            {
                _Dentist.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get
            {
                return _Dentist.Name;
            }
            set
            {
                _Dentist.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DentistViewModel(Models.DentistAccount Dentist)
        {
            Update(Dentist);
        }

        // property change function
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update(Models.DentistAccount Dentist)
        {
            Id = Dentist.Id;
            Name = Dentist.Name;
        }
    }
}
