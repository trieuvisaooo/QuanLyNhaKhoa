using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLyNhaKhoa.ViewModels
{
    public class BriefInfoViewModel : INotifyPropertyChanged
    {
        private Interfaces.Account _account;
        public string Id
        {
            get
            {
                return _account.Id;
            }

            set
            {
                _account.Id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get
            {
                return _account.Name;
            }

            set
            {
                _account.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public BriefInfoViewModel(Interfaces.Account account)
        {
            _account = account;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
