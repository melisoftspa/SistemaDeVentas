using System;
using Microsoft.UI.Xaml.Controls;

namespace SistemaDeVentas.WinUI.Services
{
    public interface INavigationService
    {
        void NavigateTo(Type pageType, object? parameter = null);
        void NavigateToMain();
        void NavigateToLogin();
        void NavigateToSales();
        void NavigateToInventory();
        void NavigateToReports();
        void NavigateToSettings();
        bool CanGoBack { get; }
        void GoBack();
        Frame? Frame { get; set; }
    }
}