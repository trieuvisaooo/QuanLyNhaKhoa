using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MakeAppointmentPage : Page
    {
        // set up blank page
        public MakeAppointmentPage()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            SetAppointment.Content = "Clicked";
        }
    }
}
