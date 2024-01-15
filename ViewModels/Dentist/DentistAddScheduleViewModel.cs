using System;
using System.ComponentModel;

namespace QuanLyNhaKhoa.ViewModels.Dentist
{
    public class DentistAddScheduleViewModel : INotifyPropertyChanged
    {
        private string _denID;
        private DateTime _workDate;
        private TimeSpan _startTime;
        private TimeSpan _endTime;

        public string DenID
        {
            get
            {
                return _denID;
            }
            set
            {
                _denID = value;
                NotifyPropertyChanged(nameof(DenID));
            }
        }

        public DateTime WorkDate
        {
            get
            {
                return _workDate;
            }
            set
            {
                _workDate = value;
                NotifyPropertyChanged(nameof(WorkDate));
            }
        }

        public TimeSpan StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                NotifyPropertyChanged(nameof(StartTime));
            }
        }

        public TimeSpan EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                NotifyPropertyChanged(nameof(EndTime));
            }
        }

        public DentistAddScheduleViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
