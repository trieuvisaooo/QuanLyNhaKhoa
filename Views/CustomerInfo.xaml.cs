using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.PhoneNumberFormatting;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerInfo : Page
    {
        public CustomerInfo()
        {
            this.InitializeComponent();
            customer = new Models.Customer();
        }

        public Models.Customer customer { get; private set; }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            Modify.Visibility = Visibility.Collapsed;
            DateOfBirth.Background.Opacity = 0;
            //DateOfBirth.IsReadOnly = false;
            PhoneNum.IsReadOnly = false;
            Addr.IsReadOnly = false;
            SaveAndCancel.Visibility = Visibility.Visible;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Modify.Visibility = Visibility.Visible;
            //DateOfBirth.IsReadOnly = true;
            PhoneNum.IsReadOnly = true;
            Addr.IsReadOnly = true;
            SaveAndCancel.Visibility = Visibility.Collapsed;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Modify.Visibility= Visibility.Visible;
            //DateOfBirth.IsReadOnly = true;
            PhoneNum.IsReadOnly = true;
            Addr.IsReadOnly = true;
            SaveAndCancel.Visibility= Visibility.Collapsed;
        }
    }
}
