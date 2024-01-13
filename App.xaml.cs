using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using QuanLyNhaKhoa.DataAccess;
using QuanLyNhaKhoa.Views;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Windows.Graphics;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa
{

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        //conect to db in sql server
        private string connectionString = @"Data Source=NHOXIU21\SQLEXPRESS;Initial Catalog=QLPK;Integrated Security=True";
        public string ConnectionString { get => connectionString; set => connectionString = value; }
        private DatabaseManagement _databaseManagement;
        public AccountData CurrentAccount;
        //public MedicinesAndServices MedicinesAndServices;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this._databaseManagement = new DatabaseManagement();
            this.CurrentAccount = new AccountData(_databaseManagement.ConnectionString);
            //this.MedicinesAndServices = new MedicinesAndServices(_databaseManagement.ConnectionString);
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            //Window _mWindow = new AdminWindow();
            //_mWindow.Activate();
            //Window mainWindow = new AdminWindow();
            //mainWindow.Activate();
            //Window CusWindow = new CustomerWindow();
            //CusWindow.Activate();
            //Window _loginWindow = new LogInWindow();
            //_loginWindow.Activate();
            Window DenWindow = new DentistWindow();
            DenWindow.Activate();
        }

        public static bool SetTitleBarColors(Window window)
        {
            if (window is null)
            {
                return false;
            }
            window.SystemBackdrop = new MicaBackdrop();
            if (AppWindowTitleBar.IsCustomizationSupported())
            {

                var titleBar = window.AppWindow.TitleBar;
                titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
                titleBar.SetDragRectangles(new RectInt32[]
                {
                    new RectInt32(40, 0, 10000, 48)
                });
                // Set active window colors
                // Note: No effect when app is running on Windows 10 since color customization is not
                // supported
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonHoverBackgroundColor = Color.FromArgb(30, 255, 255, 255);
                titleBar.ButtonPressedBackgroundColor = Color.FromArgb(40, 0, 0, 0);

                // Set inactive window colors
                // Note: No effect when app is running on Windows 10 since color customization is not
                // supported.
                titleBar.InactiveBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0, 0, 0, 0);
                return true;
            }

            return false;
        }

        public static void SetResizability(Window window, bool isResizable = true)
        {
            var _presenter = window.AppWindow.Presenter as OverlappedPresenter;
            _presenter.IsResizable = isResizable;
            _presenter.IsMaximizable = isResizable;
            _presenter.IsMinimizable = isResizable;
        }


        //public static void SetDragRegion(Window window, NonClientRegionKind nonClientRegionKind, params FrameworkElement[] frameworkElements)
        //{
        //    var nonClientInputSrc = InputNonClientPointerSource.GetForWindowId(window.AppWindow.Id);
        //    List<Windows.Graphics.RectInt32> rects = new List<Windows.Graphics.RectInt32>();

        //    foreach (var frameworkElement in frameworkElements)
        //    {
        //        GeneralTransform transformElement = frameworkElement.TransformToVisual(null);
        //        Windows.Foundation.Rect bounds = transformElement.TransformBounds(new Windows.Foundation.Rect(0, 0, frameworkElement.ActualWidth, frameworkElement.ActualHeight));
        //        var transparentRect = new Windows.Graphics.RectInt32(
        //            _X: (int)Math.Round(bounds.X),
        //            _Y: (int)Math.Round(bounds.Y),
        //            _Width: (int)Math.Round(bounds.Width),
        //            _Height: (int)Math.Round(bounds.Height)
        //        );
        //        rects.Add(transparentRect);
        //    }

        //    nonClientInputSrc.SetRegionRects(nonClientRegionKind, rects.ToArray());
        //}
    }
}
