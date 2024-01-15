using System.ComponentModel;


namespace QuanLyNhaKhoa.ViewModels
{
    public class CustomerMakeAppointmentViewModel : INotifyPropertyChanged
    {
        private string _denID;
        private string _denName;

        public string DenID
        {
            get; set;
        }
        public string DenName
        {
            get; set;
        }


        public CustomerMakeAppointmentViewModel()
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
