using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels;
using QuanLyNhaKhoa.Models;
using System.Collections.ObjectModel;
using QuanLyNhaKhoa.Views.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerRecords : Page
    {
        public CustomerRecords()
        {
            this.InitializeComponent();
            customerInfo.GetCustomerInfo((App.Current as App).ConnectionString, customerInfo);
            recordList = customerRecords.GetRecords((App.Current as App).ConnectionString, customerInfo.CusID);
            RecordList.ItemsSource = recordList;
            //customerRecords.GetMedicine((App.Current as App).ConnectionString, recordList);   
        }

        private CustomerInfoViewModel customerInfo = new CustomerInfoViewModel();
        private ObservableCollection<CustomerRecordViewModel> recordList = new ObservableCollection<CustomerRecordViewModel>();
        private CustomerRecordViewModel customerRecords = new CustomerRecordViewModel();

        private void Search_click(object sender, RoutedEventArgs e)
        {
            customerInfo.GetCustomerInfo((App.Current as App).ConnectionString, customerInfo);
            ObservableCollection<CustomerRecordViewModel> listByDenName = new ObservableCollection<CustomerRecordViewModel>();
            listByDenName = customerRecords.GetRecordsByDenName((App.Current as App).ConnectionString, customerInfo.CusID, search_box.Text);
            RecordList.ItemsSource = listByDenName;
        }


        private void lvItemClick(object sender, ItemClickEventArgs e)
        {
            //string recordID = customerRecords.RecordID;
            //CustomerRecordViewModel detailRecord = new CustomerRecordViewModel(customerRecords.RecordID);
            var item = e.ClickedItem as CustomerRecordViewModel;

            if (item != null)
            {
                this.Frame.Navigate(typeof(CustomerRecordDetail), item);
            }

        }

        //private void ViewInvoice_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(CustomerInvoiceDetail), CRViewModel);
        //}

    }
}
