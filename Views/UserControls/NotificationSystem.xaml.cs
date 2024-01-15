// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using QuanLyNhaKhoa.ViewModels.Notification;

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

        public void WriteLine(string message, bool isBad = false)
        {
            NotificationList.WriteMesssage(message, isBad);
        }
    }
}
