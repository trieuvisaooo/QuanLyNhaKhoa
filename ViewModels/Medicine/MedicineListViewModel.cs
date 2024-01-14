using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Medicine
{
    // make AdministratorListViewModel to be a list of AdministratorViewModel but bindable and updateable
    public class MedicineListViewModel
    {
        public ObservableCollection<MedicineViewModel> MedicineList { get; set; } = new();
        public readonly static int PageSize = 10;
        public MedicineListViewModel()
        {
            UpdateSource();
        }

        public async Task<bool> UpdateSource(string withName = null)
        {
            // get all books from database
            List<Models.Medicine> medicines = ((App)Application.Current).MedicinesAndServices.GetMedicines(withName);
            var disQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            disQueue.TryEnqueue(() =>
            {

                MedicineList.Clear();
                // add each book to the list
                for (int i = 0; i < medicines.Count; i++)
                {
                    MedicineList.Add(new MedicineViewModel(medicines[i]));
                };
            });
            return true;
        }

        public void ReLoad(int index)
        {
            MedicineList.Insert(index + 1, MedicineList[index]);
            MedicineList.RemoveAt(index);
        }

        public void Remove(int index)
        {
            var isSuccessful = ((App)Application.Current).MedicinesAndServices.RemoveMedicine(MedicineList[index].GetMedicine());
            if (isSuccessful)
            {
                MedicineList.RemoveAt(index);
            }
        }
    }
}
