using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace SistemaDeVentas.WinUI.Pages;

public sealed partial class MainShell : Page
{
    public MainShell()
    {
        this.InitializeComponent();
        NavView.SelectionChanged += NavView_SelectionChanged;
        ContentFrame.Navigated += ContentFrame_Navigated;

        // Navigate to default page (SalesPage for logged in users)
        ContentFrame.Navigate(typeof(SalesPage));
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItemContainer is NavigationViewItem item && item.Tag is string tag)
        {
            System.Type? pageType = tag switch
            {
                "home" => typeof(SalesPage), // Default to SalesPage
                "sales" => typeof(SalesPage),
                "inventory" => typeof(InventoryPage),
                "reports" => typeof(ReportsPage),
                _ => null
            };

            if (pageType is not null)
            {
                ContentFrame.Navigate(pageType);
            }
        }
    }

    private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
    {
        // Update breadcrumb based on current page
        string breadcrumb = e.SourcePageType.Name switch
        {
            "SalesPage" => "Ventas",
            "InventoryPage" => "Inventario",
            "ReportsPage" => "Reportes",
            _ => "Inicio"
        };

        BreadcrumbText.Text = breadcrumb;

        // Update selected item in NavigationView
        foreach (NavigationViewItem item in NavView.MenuItems)
        {
            if (item.Tag is string tag)
            {
                bool isSelected = (tag == "sales" && e.SourcePageType == typeof(SalesPage)) ||
                                  (tag == "inventory" && e.SourcePageType == typeof(InventoryPage)) ||
                                  (tag == "reports" && e.SourcePageType == typeof(ReportsPage));

                item.IsSelected = isSelected;
            }
        }
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        // Navigate back to login page
        Frame.Navigate(typeof(LoginPage));
    }
}
