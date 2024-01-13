using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using System.Collections.ObjectModel;
using QuanLyNhaKhoa.Views.Pages;
using QuanLyNhaKhoa.ViewModels.Customer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerRecords : Page
    {
        private ObservableCollection<CustomerRecordViewModel> recordList = new ObservableCollection<CustomerRecordViewModel>();
        private CustomerRecordViewModel customerRecords = new CustomerRecordViewModel();

        public CustomerRecords()
        {
            this.InitializeComponent();
            recordList = customerRecords.GetRecords((App.Current as App).ConnectionString, (App.Current as App).CurrentAccount.StoredAccount.Id);
            RecordList.ItemsSource = recordList;
        }



        private void Search_click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<CustomerRecordViewModel> listByDenName = new ObservableCollection<CustomerRecordViewModel>();
            listByDenName = customerRecords.GetRecordsByDenName((App.Current as App).ConnectionString, (App.Current as App).CurrentAccount.StoredAccount.Id, search_box.Text);
            RecordList.ItemsSource = listByDenName;
        }


        private void lvItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as CustomerRecordViewModel;

            if (item != null)
            {
                this.Frame.Navigate(typeof(CustomerRecordDetail), item);
            }

        }

    }
}
