using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Receptionist
{
    public class Staff_DetailMedicalRecordViewModel : INotifyPropertyChanged
    {
        private string _mrID;
        private string _description;
        private string _dentistID;
        private string _dentistName;
        private DateOnly _dateVisit;
        private List<Models.Medicine> _medic;
        private List<Models.Service> _serviceUsed;
        private string _invoiceID;
        private int _totalPayment;
        private string _paymentStatus;

        public string MrID
        {
            get
            {
                return _mrID;
            }
            set
            {
                _mrID = value; OnPropertyChanged(nameof(_mrID));
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value; OnPropertyChanged(nameof(_description));
            }
        }
        public string DentistID
        {
            get
            {
                return _dentistID;
            }
            set
            {
                _dentistID = value; OnPropertyChanged(nameof(_dentistID));
            }
        }
        public string DentistName
        {
            get
            {
                return _dentistName;
            }
            set
            {
                _dentistName = value; OnPropertyChanged(nameof(_dentistName));
            }
        }
        public DateOnly DateVisit
        {
            get
            {
                return _dateVisit;
            }
            set
            {
                _dateVisit = value; OnPropertyChanged(nameof(_dateVisit));
            }
        }
        public List<Models.Medicine> MedicName
        {
            get => _medic;
            set
            {
                _medic = value; OnPropertyChanged(nameof(_medic));
            }
        }

        public List<Models.Service> ServiceUsed
        {
            get
            {
                return new List<Models.Service>(_serviceUsed);
            }
            set
            {
                _serviceUsed = value; OnPropertyChanged(nameof(_serviceUsed));
            }
        }
        public string InvoiceID
        {
            get
            {
                return _invoiceID;
            }
            set
            {
                _invoiceID = value; OnPropertyChanged(nameof(_invoiceID));
            }
        }
        public int TotalPayment
        {
            get
            {
                return _totalPayment;
            }
            set
            {
                _totalPayment = value; OnPropertyChanged(nameof(_totalPayment));
            }
        }
        public string PaymentStatus
        {
            get
            {
                return _paymentStatus;
            }
            set
            {
                _paymentStatus = value; OnPropertyChanged(nameof(_paymentStatus));
            }
        }


        public Staff_DetailMedicalRecordViewModel(string mrID, string description, string dentistID,
            string dentistName, DateOnly dateVisit, List<Models.Medicine> medic,
            List<Models.Service> serviceUsed, string invoiceID, int totalPayment, string paymentStatus)
        {
            _mrID = mrID;
            _description = description;
            _dentistID = dentistID;
            _dentistName = dentistName;
            _dateVisit = dateVisit;
            _medic = medic;
            _serviceUsed = serviceUsed;
            _invoiceID = invoiceID;
            _totalPayment = totalPayment;
            _paymentStatus = paymentStatus;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
