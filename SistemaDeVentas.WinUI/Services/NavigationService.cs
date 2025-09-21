using System;
using Microsoft.UI.Xaml.Controls;
using SistemaDeVentas.WinUI.Pages;

namespace SistemaDeVentas.WinUI.Services
{
    public class NavigationService : INavigationService
    {
        private Frame? _frame;

        public Frame? Frame
        {
            get => _frame;
            set => _frame = value;
        }

        public bool CanGoBack => _frame?.CanGoBack ?? false;

        public void GoBack()
        {
            if (CanGoBack)
            {
                _frame?.GoBack();
            }
        }

        public void NavigateTo(Type pageType, object? parameter = null)
        {
            if (_frame == null)
                throw new InvalidOperationException("Frame no está configurado");

            _frame.Navigate(pageType, parameter);
        }

        public void NavigateToMain()
        {
            NavigateTo(typeof(MainShell));
        }

        public void NavigateToLogin()
        {
            NavigateTo(typeof(LoginPage));
        }

        public void NavigateToSales()
        {
            NavigateTo(typeof(SalesPage));
        }

        public void NavigateToInventory()
        {
            NavigateTo(typeof(InventoryPage));
        }

        public void NavigateToReports()
        {
            NavigateTo(typeof(ReportsPage));
        }

        public void NavigateToSettings()
        {
            NavigateTo(typeof(SettingsPage));
        }
    }
}