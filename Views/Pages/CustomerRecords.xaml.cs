using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels;
using QuanLyNhaKhoa.Models;
using System.Collections.ObjectModel;

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
            customerRecords.GetMedicine((App.Current as App).ConnectionString, recordList);
            
        }

        private CustomerInfoViewModel customerInfo = new CustomerInfoViewModel();
        private ObservableCollection<CustomerRecordViewModel> recordList = new ObservableCollection<CustomerRecordViewModel>();
        private CustomerRecordViewModel customerRecords = new CustomerRecordViewModel();



        private void ViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
