using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuanLyNhaKhoa.Views.UserControls
{
    public sealed partial class AvatarCircle : UserControl
    {
        public ViewModels.BriefInfoViewModel InfoViewModel { get; set; }
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
