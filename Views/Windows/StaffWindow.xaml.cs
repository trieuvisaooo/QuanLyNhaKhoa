// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using QuanLyNhaKhoa.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            this.InitializeComponent();
            FrameInflate(0);
        }

        private void FrameInflate(int index)
        {
            switch (index)
            {
                case 0:
                    //NvgtView.Header = "DANH SÁCH KHÁCH HÀNG";
                    contentFrame.Navigate(typeof(Staff_CustomerList));
                    break;
                case 1:
                    //NvgtView.Header = "DANH SÁCH BỆNH ÁN";
                    contentFrame.Navigate(typeof(Staff_MedicalRecord));
                    break;

            }
        }
        private void NvgtView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            // Inflate frame according to the index invoked
            switch (args.InvokedItemContainer.Tag)
            {
                case "0":
                    FrameInflate(0);
                    break;
                case "1":
                    FrameInflate(1);
                    break;
                case "SignOut":
                    Window LogInWindow = new LogInWindow();
                    LogInWindow.Activate();
                    this.Close();
                    break;
            }
        }

        private void NvgtView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            // Inflate frame according to the index selected
            switch (args.SelectedItemContainer.Tag)
            {
                case "0":
                    FrameInflate(0);
                    break;
                case "1":
                    FrameInflate(1);
                    break;
                case "SignOut":
                    break;

            }
        }

    }
}
