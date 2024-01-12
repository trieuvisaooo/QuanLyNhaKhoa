using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuanLyNhaKhoa.ViewModels;
using QuanLyNhaKhoa.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.UserControls
{
    public sealed partial class NotificationSystem : UserControl
    {
        internal NotificationList NotificationList { get; set; } = new NotificationList();
        public NotificationSystem()
        {
            this.InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = FindVisualParent<ListViewItem>(sender as Button);

            int index = Notilist.IndexFromContainer(listViewItem);

            NotificationList.Destroy(NotificationList.Notifications[index].Id);
        }

        private T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                if (parent is T correctlyTyped)
                {
                    return correctlyTyped;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        public void WriteLine(string message)
        {
            NotificationList.WriteMesssage(message);
        }
    }
}
