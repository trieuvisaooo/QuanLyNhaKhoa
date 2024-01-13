using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Receptionist
{
    // make ReceptionistListViewModel to be a list of ReceptionistViewModel but bindable and updateable
    public class CustomerListViewModel
    {
        public ObservableCollection<BriefInfoViewModel> CustomerList { get; set; } = new();
        public readonly static int PageSize = 10;
        public CustomerListViewModel()
        {
            UpdateSource();
        }

        public async Task<bool> UpdateSource(string withName = null)
        {
            // get all books from database
            List<CustomerAccount> receps = ((App)Application.Current).CurrentAccount.GetCustomers(withName);
            var disQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            disQueue.TryEnqueue(() =>
            {

                CustomerList.Clear();
                // add each book to the list
                for (int i = 0; i < receps.Count; i++)
                {
                    CustomerList.Add(new BriefInfoViewModel(receps[i]));
                };
            });
            return true;
        }

        internal void LockOrUnlock(int index)
        {
            ((App)Application.Current).CurrentAccount.
            LockOrUnlockAccount(
            CustomerList[index].GetAccount() as CustomerAccount,
            CustomerList[index].Status);
            CustomerList.ElementAt(index).Status = !CustomerList.ElementAt(index).Status;
        }

        public void ReLoad(int index)
        {
            CustomerList.Insert(index + 1, CustomerList[index]);
            CustomerList.RemoveAt(index);
        }

        public void ResetPassword(int index)
        {
            ((App)Application.Current).CurrentAccount.
            ResetPassword(
            CustomerList[index].GetAccount() as CustomerAccount);
        }
    }
}
