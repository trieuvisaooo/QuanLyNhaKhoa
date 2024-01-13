using QuanLyNhaKhoa.Helpers;
using QuanLyNhaKhoa.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public AccountHelper accountHelper = new();

        public string SelectedRole
        {
            get => Roles[(int)accountHelper.selectedRole];
            set
            {
                int roleIndex = Roles.IndexOf(value);
                Console.WriteLine(roleIndex);
                if (roleIndex != -1)
                {
                    accountHelper.selectedRole = (AccountHelper.Role)roleIndex;
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
            get => accountHelper.phoneNumber;
            set
            {
                accountHelper.phoneNumber = LogInHelper.NormalizeVietnamesePhoneNumber(value);
                NotifyPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string Password
        {
            get => accountHelper.password;
            set
            {
                accountHelper.password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        public string Name
        {
            get => accountHelper.name;
            set
            {
                accountHelper.name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public string Address
        {
            get => accountHelper.address;
            set
            {
                accountHelper.address = value;
                NotifyPropertyChanged(nameof(Address));
            }
        }

        public AccountViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal Task<bool> AddAccount()
        {
            Interfaces.Account AccountTemplate;
            // call the database for authentication
            if (accountHelper.selectedRole == AccountHelper.Role.Admin)
            {
                AccountTemplate = new AdministratorAccount();
            }
            else if (accountHelper.selectedRole == AccountHelper.Role.Customer)
            {
                // call the database for authentication
                AccountTemplate = new CustomerAccount();
            }
            else if (accountHelper.selectedRole == AccountHelper.Role.Dentist)
            {
                // call the database for authentication
                AccountTemplate = new DentistAccount();
            }
            else if (accountHelper.selectedRole == AccountHelper.Role.Receptionist)
            {
                // call the database for authentication
                AccountTemplate = new ReceptionistAccount();
            }
            else
            {
                return Task.FromResult(false);
            }

            AccountTemplate.PhoneNumber = (PhoneNumber.PadRight(15, ' ')).Substring(0, 15);
            AccountTemplate.Password = (Password.PadRight(50, ' ')).Substring(0, 50);
            AccountTemplate.Name = Name;
            AccountTemplate.Address = Address;
            AccountTemplate.Status = 1;
            bool result = (App.Current as App).CurrentAccount.AddAccount(AccountTemplate);
            return Task.FromResult(result);
        }
    }
}
