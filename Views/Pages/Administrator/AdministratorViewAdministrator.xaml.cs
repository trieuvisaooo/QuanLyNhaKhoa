using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels.Administrator;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.Pages.Administrator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdministratorViewAdministrator : Page
    {
        internal AdministratorListViewModel AdministratorListViewModels { get; set; } = new();
        private static Microsoft.UI.Dispatching.DispatcherQueueTimer _typeTimer = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread().CreateTimer();
        public AdministratorViewAdministrator()
        {
            this.InitializeComponent();
            _typeTimer.Interval = TimeSpan.FromMilliseconds(100);
        }

        private void ListView_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {
                bool isLocked = AdministratorListViewModels.AdministratorList[(sender as ListView).SelectedIndex].Status;
                LockContent.Visibility = isLocked ? Visibility.Visible : Visibility.Collapsed;
                UnLockContent.Visibility = isLocked ? Visibility.Collapsed : Visibility.Visible;
                remove_btn.IsEnabled = true;
                reset_btn.IsEnabled = true;
            }
            else
            {
                remove_btn.IsEnabled = false;
                reset_btn.IsEnabled = false;
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (RecListView.SelectedIndex == -1)
            {
                remove_btn.IsEnabled = false;
                reset_btn.IsEnabled = false;
            }
            else
            {
                bool isLocked = AdministratorListViewModels.AdministratorList[(RecListView).SelectedIndex].Status;
                AdministratorListViewModels.LockOrUnlock(RecListView.SelectedIndex);
                LockContent.Visibility = isLocked ? Visibility.Visible : Visibility.Collapsed;
                UnLockContent.Visibility = isLocked ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void ReQuery_Change(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            LoadingBar.Visibility = Visibility.Visible;
            _typeTimer.Stop();
            _typeTimer.Start();
            try
            {

                _typeTimer.Tick += (_, __) =>
                {
                    _typeTimer.Stop();
                    PerfomingQuery(sender);
                };
            }
            catch (System.Exception) { }
        }

        private async void PerfomingQuery(AutoSuggestBox sender)
        {
            string textSearch = sender.Text;
            await AdministratorListViewModels.UpdateSource(textSearch);
            if (LoadingBar.Visibility == Visibility.Visible)
            {
                LoadingBar.Visibility = Visibility.Collapsed;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (RecListView.SelectedIndex == -1)
            {
                remove_btn.IsEnabled = false;
                reset_btn.IsEnabled = false;
            }
            else
            {
                AdministratorListViewModels.ResetPassword(RecListView.SelectedIndex);
                Notify.WriteLine("Đã tái thiết lập mật khẩu mặc định.");
            }
        }
    }
}
