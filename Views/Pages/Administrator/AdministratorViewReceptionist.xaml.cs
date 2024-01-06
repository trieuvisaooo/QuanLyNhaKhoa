using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels.Receptionist;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.Pages.Administrator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdministratorViewReceptionist : Page
    {
        internal ReceptionistListViewModel ReceptionistList { get; set; } = new();
        public AdministratorViewReceptionist()
        {
            this.InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {
                edit_btn.IsEnabled = true;
                remove_btn.IsEnabled = true;
            }
            else
            {
                edit_btn.IsEnabled = false;
                remove_btn.IsEnabled = false;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReQuery_Change(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            ReceptionistList.UpdateSource((sender as AutoSuggestBox).Text);
        }
    }
}
