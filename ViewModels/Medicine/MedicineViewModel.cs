using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QuanLyNhaKhoa.ViewModels.Medicine
{
    public class MedicineViewModel : INotifyPropertyChanged
    {
        private readonly Models.Medicine _Medicine = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get
            {
                return _Medicine.ID;
            }
            set
            {
                _Medicine.ID = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get
            {
                return _Medicine.Name;
            }
            set
            {
                _Medicine.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Unit
        {
            get
            {
                return _Medicine.Unit;
            }
            set
            {
                _Medicine.Unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        public int Price
        {
            get
            {
                return _Medicine.Price;
            }
            set
            {
                _Medicine.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public int Quantity
        {
            get
            {
                return _Medicine.Count;
            }
            set
            {
                _Medicine.Count = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string Description
        {
            get
            {
                return _Medicine.Description;
            }
            set
            {
                _Medicine.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime ExpiredDate
        {
            get
            {
                return _Medicine.ExpiredDate;
            }
            set
            {
                _Medicine.ExpiredDate = value;
                OnPropertyChanged(nameof(ExpiredDate));
            }
        }

        public Models.Medicine GetMedicine()
        {
            return _Medicine;
        }


        public MedicineViewModel(Models.Medicine Medicine)
        {
            Update(Medicine);
        }

        // property change function
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update(Models.Medicine Medicine)
        {
            Id = Medicine.ID;
            Name = Medicine.Name;
            Unit = Medicine.Unit;
            Price = Medicine.Price;
            Quantity = Medicine.Count;
            Description = Medicine.Description;
            ExpiredDate = Medicine.ExpiredDate;
        }

        public Task<bool> AddMedicine()
        {
            var result = (App.Current as App).MedicinesAndServices.AddMedicine(_Medicine);
            return Task.FromResult(true);
        }
        public Task<bool> EditMedicine()
        {
            var result = (App.Current as App).MedicinesAndServices.EditMedicine(_Medicine);
            return Task.FromResult(true);
        }
    }
}
