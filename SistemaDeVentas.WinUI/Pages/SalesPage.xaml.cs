using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using SistemaDeVentas.Core.ViewModels.ViewModels;

namespace SistemaDeVentas.WinUI.Pages
{
    public sealed partial class SalesPage : Page
    {
        public SalesViewModel ViewModel { get; }

        public SalesPage()
        {
            this.InitializeComponent();
            ViewModel = ((App)Application.Current).Services.GetRequiredService<SalesViewModel>();
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