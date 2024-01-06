using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuanLyNhaKhoa.ViewModels.Receptionist
{
    // make ReceptionistListViewModel to be a list of ReceptionistViewModel but bindable and updateable
    public class ReceptionistListViewModel
    {
        public ObservableCollection<BriefInfoViewModel> receptionistList { get; set; } = new();
        public ReceptionistListViewModel()
        {
            UpdateSource();
        }

        public void UpdateSource(string withName = null)
        {
            // get all books from database
            List<ReceptionistView> receps = ((App)Application.Current).CurrentAccount.GetReceptionists(withName);
            receptionistList.Clear();

            // add each book to the list
            for (int i = 0; i < receps.Count; i++)
            {
                receptionistList.Add(new BriefInfoViewModel(receps[i]));
            };
        }


        //internal void Insert(Book book)
        //{
        //    ((App)Application.Current).databaseManagement.AddBook(book);
        //    UpdateSource();
        //}
    }
}
