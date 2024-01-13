using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Dentist
{
    // make DentistListViewModel to be a list of DentistViewModel but bindable and updateable
    public class DentistListViewModel
    {
        public ObservableCollection<BriefInfoViewModel> DentistList { get; set; } = new();
        public readonly static int PageSize = 10;
        public DentistListViewModel()
        {
            UpdateSource();
        }

        public async Task<bool> UpdateSource(string withName = null)
        {
            // get all books from database
            List<DentistAccount> dentists = ((App)Application.Current).CurrentAccount.GetDentists(withName);
            var disQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            disQueue.TryEnqueue(() =>
            {

                DentistList.Clear();
                // add each book to the list
                for (int i = 0; i < dentists.Count; i++)
                {
                    DentistList.Add(new BriefInfoViewModel(dentists[i]));
                };
            });
            return true;
        }

        internal void LockOrUnlock(int index)
        {
            ((App)Application.Current).CurrentAccount.
            LockOrUnlockAccount(
            DentistList[index].GetAccount() as DentistAccount,
            DentistList[index].Status);
            DentistList.ElementAt(index).Status = !DentistList.ElementAt(index).Status;
        }

        public void ReLoad(int index)
        {
            DentistList.Insert(index + 1, DentistList[index]);
            DentistList.RemoveAt(index);
        }

        public void ResetPassword(int index)
        {
            ((App)Application.Current).CurrentAccount.
            ResetPassword(
            DentistList[index].GetAccount() as DentistAccount);
        }
    }
}
