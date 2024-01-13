using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels.Customer;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MakeAppointmentPage : Page
    {
        public AppointmentCustomerViewModel[] appointmentCustomerViewModels { get; set; }
        // set up blank page
        public MakeAppointmentPage()
        {
            this.InitializeComponent();
            appointmentCustomerViewModels = new AppointmentCustomerViewModel[5] {
                new AppointmentCustomerViewModel(20120172, "Vân Duy Quang"),
                new AppointmentCustomerViewModel(21120231, "Bùi Hoàng Duy"),
                new AppointmentCustomerViewModel(21120252, "Võ Hoàng Nam Hưng"),
                new AppointmentCustomerViewModel(21120332, "Đào Cẩm Thanh"),
                new AppointmentCustomerViewModel(21120347, "Nguyễn Khắc Triệu")
            };
            this.DataContext = this.appointmentCustomerViewModels;
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            SetAppointment.Content = "Clicked";
            appointmentCustomerViewModels[0].Name = "Vân Duy Quang Khùng";
        }
    }
}
