using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuanLyNhaKhoa.Models;
using QuanLyNhaKhoa.ViewModels.Shared;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.UserControls
{
    public sealed partial class AvatarCircle : UserControl
    {
        public BriefInfoViewModel InfoViewModel
        {
            get { return (BriefInfoViewModel)GetValue(SelectedAccount); }
            set { SetValue(SelectedAccount, value); }
        }
        public static BriefInfoViewModel defaultView = new(new AdministratorAccount());

        public static readonly DependencyProperty SelectedAccount =
        DependencyProperty.Register(nameof(InfoViewModel), typeof(BriefInfoViewModel), typeof(AvatarCircle),
        new PropertyMetadata(defaultView));
        public AvatarCircle()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Bindings.Update();
        }
    }
}
