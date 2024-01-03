using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using QuanLyNhaKhoa.DataAccess;
using QuanLyNhaKhoa.Views;
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
        private string connectionString = @"Data Source=.\SQLSERVER;Initial Catalog=QLPK;Integrated Security=True";
        public string ConnectionString { get => connectionString; set => connectionString = value; }
        private Interfaces.Account _currentAccount;
        private DatabaseManagement _databaseManagement;

        public Interfaces.Account CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {

            }
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            //this._databaseManagement = new DatabaseManagement();
            Debug.WriteLine(connectionString);
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            //Window mainWindow = new MainWindow();
            //mainWindow.Activate();
            Window CusWindow = new CustomerWindow();
            CusWindow.Activate();
            //Window _loginWindow = new LogInWindow();
            //_loginWindow.Activate();
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
    }
}
