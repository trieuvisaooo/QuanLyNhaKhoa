using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using QuanLyNhaKhoa.Views;
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
            //this.InitializeComponent();
        }

        private void ViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            //ViewInvoice.Content = "Clicked";
        }
    }
}