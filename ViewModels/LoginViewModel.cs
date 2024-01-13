using QuanLyNhaKhoa.Helpers;
using QuanLyNhaKhoa.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

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
            "Khách hàng", "Nhân viên", "Nha sĩ", "QTV"
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

        internal Task<bool> SignIn()
        {
            // call the database for authentication
            if (logInHelper.selectedRole == LogInHelper.Role.Admin)
            {
                Interfaces.Account AccountTemplate = new AdministratorAccount()
                {
                    PhoneNumber = (PhoneNumber.PadRight(15, ' ')).Substring(0, 15),
                    Password = (Password.PadRight(50, ' ')).Substring(0, 50),
                };
                try
                {
                    bool result = (App.Current as App).CurrentAccount.Login(AccountTemplate);
                    if (!result) { throw new ArgumentException("Số điện thoại hoặc mật khẩu sai."); }
                    return Task.FromResult(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (logInHelper.selectedRole == LogInHelper.Role.Customer)
            {
                // call the database for authentication
                throw new NotImplementedException();
            }
            else if (logInHelper.selectedRole == LogInHelper.Role.Dentist)
            {
                // call the database for authentication
                throw new NotImplementedException();
            }
            else if (logInHelper.selectedRole == LogInHelper.Role.Receptionist)
            {
                // call the database for authentication
                throw new NotImplementedException();
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
