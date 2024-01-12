using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels.Medicine;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.Pages.Administrator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditMedicine : Page
    {
        public MedicineViewModel medicineViewModel { get; set; }
        public static MedicineViewModel medicineViewModelTemp { get; set; }
        public EditMedicine()
        {
            medicineViewModel = medicineViewModelTemp;
            this.InitializeComponent();
            DatePickerControl.Date = medicineViewModel.ExpiredDate;
        }

        private async void EditMedc_Click(object sender, RoutedEventArgs e)
        {
            LoadingRing.IsActive = true;
            (sender as Button).Visibility = Visibility.Collapsed;
            try
            {
                medicineViewModel.ExpiredDate = DatePickerControl.SelectedDate.Value.Date;
                bool isSuccess = await Task.Run(() => medicineViewModel.EditMedicine());
                if (isSuccess)
                {
                    Frame.GoBack();
                }
            }
            catch (System.Exception) { }
            LoadingRing.IsActive = false;
            (sender as Button).Visibility = Visibility.Visible;

        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is Microsoft.UI.Xaml.Controls.TextBox)
            {
                (sender as Microsoft.UI.Xaml.Controls.TextBox).SelectAll();
            }
            else if (sender is Microsoft.UI.Xaml.Controls.PasswordBox)
            {
                (sender as Microsoft.UI.Xaml.Controls.PasswordBox).SelectAll();
            }
        }
    }
}
