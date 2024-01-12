using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Administrator
{
    // make AdministratorListViewModel to be a list of AdministratorViewModel but bindable and updateable
    public class AdministratorListViewModel
    {
        public ObservableCollection<BriefInfoViewModel> AdministratorList { get; set; } = new();
        public readonly static int PageSize = 10;
        public AdministratorListViewModel()
        {
            UpdateSource();
        }

        public async Task<bool> UpdateSource(string withName = null)
        {
            // get all books from database
            List<AdministratorAccount> Administrators = ((App)Application.Current).CurrentAccount.GetAdministrators(withName);
            var disQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            disQueue.TryEnqueue(() =>
            {

                AdministratorList.Clear();
                // add each book to the list
                for (int i = 0; i < Administrators.Count; i++)
                {
                    AdministratorList.Add(new BriefInfoViewModel(Administrators[i]));
                };
            });
            return true;
        }

        internal void LockOrUnlock(int index)
        {
            ((App)Application.Current).CurrentAccount.
            LockOrUnlockAccount(
            AdministratorList[index].GetAccount() as AdministratorAccount,
            AdministratorList[index].Status);
            AdministratorList.ElementAt(index).Status = !AdministratorList.ElementAt(index).Status;
        }

        public void ReLoad(int index)
        {
            AdministratorList.Insert(index + 1, AdministratorList[index]);
            AdministratorList.RemoveAt(index);
        }

        public void ResetPassword(int index)
        {
            ((App)Application.Current).CurrentAccount.
            ResetPassword(
            AdministratorList[index].GetAccount() as AdministratorAccount);
        }
    }
}
