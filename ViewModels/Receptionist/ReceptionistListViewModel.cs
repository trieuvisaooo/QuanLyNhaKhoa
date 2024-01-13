using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.ViewModels.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Receptionist
{
    // make ReceptionistListViewModel to be a list of ReceptionistViewModel but bindable and updateable
    public class ReceptionistListViewModel
    {
        public ObservableCollection<BriefInfoViewModel> receptionistList { get; set; } = new();
        public readonly static int PageSize = 10;
        public ReceptionistListViewModel()
        {
            UpdateSource();
        }

        public Task<bool> UpdateSource(string withName = null)
        {
            // get all books from database
            List<ReceptionistAccount> receps = ((App)Application.Current).CurrentAccount.GetReceptionists(withName);
            var disQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            disQueue.TryEnqueue(() =>
            {
                receptionistList.Clear();

                // add each book to the list
                for (int i = 0; i < receps.Count; i++)
                {
                    receptionistList.Add(new BriefInfoViewModel(receps[i]));
                };
            });
            return Task.FromResult(true);
        }

        internal void LockOrUnlock(int index)
        {
            ((App)Application.Current).CurrentAccount.
            LockOrUnlockAccount(
            receptionistList[index].GetAccount() as ReceptionistAccount,
            receptionistList[index].Status);
            receptionistList.ElementAt(index).Status = !receptionistList.ElementAt(index).Status;
            ReLoad(index);
        }

        public void ReLoad(int index)
        {
            receptionistList.Insert(index + 1, receptionistList[index]);
            receptionistList.RemoveAt(index);
        }

        //internal void Insert(Book book)
        //{
        //    ((App)Application.Current).databaseManagement.AddBook(book);
        //    UpdateSource();
        //}
    }
}
