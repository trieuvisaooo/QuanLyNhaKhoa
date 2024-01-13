using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.ViewModels.Receptionist;
using System;

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
        private static Microsoft.UI.Dispatching.DispatcherQueueTimer _typeTimer = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread().CreateTimer();
        public AdministratorViewReceptionist()
        {
            this.InitializeComponent();
            _typeTimer.Interval = TimeSpan.FromMilliseconds(100);
        }

        private void ListView_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {
                bool isLocked = ReceptionistList.receptionistList[(sender as ListView).SelectedIndex].Status;
                LockContent.Visibility = isLocked ? Visibility.Visible : Visibility.Collapsed;
                UnLockContent.Visibility = isLocked ? Visibility.Collapsed : Visibility.Visible;
                edit_btn.IsEnabled = true;
                remove_btn.IsEnabled = true;
            }
            else
            {
                edit_btn.IsEnabled = false;
                remove_btn.IsEnabled = false;
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            throw new NotFiniteNumberException();

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (RecListView.SelectedIndex == -1)
            {
                edit_btn.IsEnabled = false;
                remove_btn.IsEnabled = false;
            }
            else
            {
                ReceptionistList.LockOrUnlock(RecListView.SelectedIndex);
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
            finally
            {
                LoadingBar.Visibility = Visibility.Collapsed;
            }
        }

        private async void PerfomingQuery(AutoSuggestBox sender)
        {
            string textSearch = sender.Text;
            await ReceptionistList.UpdateSource(textSearch);
        }
    }
}
