using QuanLyNhaKhoa.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LogInHelper logInHelper = new LogInHelper();
        public string SelectedRole
        {
            get => Roles[(int)logInHelper.selectedRole];
            set
            {
                int roleIndex = Roles.IndexOf(value);
                Console.WriteLine(roleIndex);
                if (roleIndex != -1)
                {
                    logInHelper.selectedRole = (LogInHelper.Role)roleIndex;
                    NotifyPropertyChanged(nameof(SelectedRole));
                }
            }
        }
        public ObservableCollection<string> Roles { get; private set; } = new ObservableCollection<string>()
        {
            "Customer", "Receptionist", "Doctor", "Admin"
        };

        public string PhoneNumber
        {
            get => logInHelper.phoneNumber;
            set
            {
                logInHelper.phoneNumber = LogInHelper.NormalizeVietnamesePhoneNumber(value);
                NotifyPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string Password
        {
            get => logInHelper.password;
            set
            {
                logInHelper.password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        public LoginViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
