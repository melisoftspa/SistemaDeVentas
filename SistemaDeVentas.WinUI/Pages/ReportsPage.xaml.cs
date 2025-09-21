using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using SistemaDeVentas.Core.ViewModels.ViewModels;

namespace SistemaDeVentas.WinUI.Pages
{
    public sealed partial class ReportsPage : Page
    {
        public ReportsViewModel ViewModel { get; }

        public ReportsPage()
        {
            this.InitializeComponent();
            ViewModel = ((App)Application.Current).Services.GetRequiredService<ReportsViewModel>();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Start page load animation
            var storyboard = (Storyboard)Resources["PageLoadStoryboard"];
            storyboard?.Begin();
        }
    }
}