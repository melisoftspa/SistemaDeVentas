using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SistemaDeVentas.WinUI.Models;
using SistemaDeVentas.Core.Application.Interfaces;
using WinUIProduct = SistemaDeVentas.WinUI.Models.Product;

namespace SistemaDeVentas.WinUI.ViewModels
{
    public class InventoryViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        public InventoryViewModel(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));

            Title = "Inventario";
            Products = new ObservableCollection<Product>();
            
            LoadProductsCommand = new RelayCommand(async () => await LoadProductsAsync());
            SearchCommand = new RelayCommand(async () => await SearchProductsAsync());
            AddProductCommand = new RelayCommand(async () => await AddProductAsync());
            EditProductCommand = new RelayCommand<WinUIProduct>(async (product) => await EditProductAsync(product));
            DeleteProductCommand = new RelayCommand<WinUIProduct>(async (product) => await DeleteProductAsync(product));
            
            // Cargar productos al inicializar
            _ = LoadProductsAsync();
        }

        private ObservableCollection<WinUIProduct> _products = new();
        public ObservableCollection<WinUIProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        private ObservableCollection<SistemaDeVentas.WinUI.Models.Category> _categories = new();
        public ObservableCollection<SistemaDeVentas.WinUI.Models.Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    _ = SearchProductsAsync();
                }
            }
        }

        private SistemaDeVentas.WinUI.Models.Category? _selectedCategory;
        public SistemaDeVentas.WinUI.Models.Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    _ = ApplyFiltersAsync();
                }
            }
        }

        private string _selectedStatus = "Todos";
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (SetProperty(ref _selectedStatus, value))
                {
                    _ = ApplyFiltersAsync();
                }
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private int _totalPages = 1;
        public int TotalPages
        {
            get => _totalPages;
            set => SetProperty(ref _totalPages, value);
        }

        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set => SetProperty(ref _totalItems, value);
        }

        private const int PageSize = 20;

        // Propiedades computadas
        public string ProductCount => $"{TotalItems} productos encontrados";
        public string PaginationInfo => $"Mostrando {Math.Min((CurrentPage - 1) * PageSize + 1, TotalItems)} - {Math.Min(CurrentPage * PageSize, TotalItems)} de {TotalItems}";
        public bool CanGoPrevious => CurrentPage > 1;
        public bool CanGoNext => CurrentPage < TotalPages;

        // Comandos
        public ICommand LoadProductsCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        private async Task LoadProductsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ClearError();

                var products = await _productService.GetAllProductsAsync();

                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product.ToWinUIProduct());
                }

                TotalItems = products.Count();
                TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                CurrentPage = 1;

                OnPropertyChanged(nameof(ProductCount));
                OnPropertyChanged(nameof(PaginationInfo));
                OnPropertyChanged(nameof(CanGoPrevious));
                OnPropertyChanged(nameof(CanGoNext));
            }
            catch (Exception ex)
            {
                SetError($"Error al cargar productos: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SearchProductsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ClearError();

                var allProducts = await _productService.GetAllProductsAsync();

                var filteredProducts = allProducts.Where(p =>
                    string.IsNullOrEmpty(SearchText) ||
                    p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    (p.Barcode != null && p.Barcode.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

                Products.Clear();
                foreach (var product in filteredProducts)
                {
                    Products.Add(product.ToWinUIProduct());
                }

                TotalItems = filteredProducts.Count();
                TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                CurrentPage = 1;

                OnPropertyChanged(nameof(ProductCount));
                OnPropertyChanged(nameof(PaginationInfo));
                OnPropertyChanged(nameof(CanGoPrevious));
                OnPropertyChanged(nameof(CanGoNext));
            }
            catch (Exception ex)
            {
                SetError($"Error al buscar productos: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ApplyFiltersAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ClearError();

                var allProducts = await _productService.GetAllProductsAsync();

                var filteredProducts = allProducts.Where(p =>
                {
                    // Aplicar filtro de categoría
                    if (SelectedCategory != null && p.IdCategory != SelectedCategory.Id)
                        return false;

                    // Aplicar filtro de estado
                    if (SelectedStatus == "Activos" && !p.IsActive)
                        return false;
                    if (SelectedStatus == "Inactivos" && p.IsActive)
                        return false;

                    // Aplicar filtro de búsqueda
                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        if (!p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) &&
                            (p.Barcode == null || !p.Barcode.Contains(SearchText, StringComparison.OrdinalIgnoreCase)))
                            return false;
                    }

                    return true;
                });

                Products.Clear();
                foreach (var product in filteredProducts)
                {
                    Products.Add(product.ToWinUIProduct());
                }

                TotalItems = filteredProducts.Count();
                TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                CurrentPage = 1;

                OnPropertyChanged(nameof(ProductCount));
                OnPropertyChanged(nameof(PaginationInfo));
                OnPropertyChanged(nameof(CanGoPrevious));
                OnPropertyChanged(nameof(CanGoNext));
            }
            catch (Exception ex)
            {
                SetError($"Error al aplicar filtros: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddProductAsync()
        {
            // TODO: Implementar navegación a formulario de creación de producto
            // _navigationService.NavigateTo("AddProduct");
            await Task.CompletedTask;
        }

        private async Task EditProductAsync(WinUIProduct? product)
        {
            if (product == null) return;

            // TODO: Implementar navegación a formulario de edición de producto
            // _navigationService.NavigateTo("EditProduct", product.Id);
            await Task.CompletedTask;
        }

        private async Task DeleteProductAsync(WinUIProduct? product)
        {
            if (product == null) return;

            try
            {
                IsBusy = true;
                ClearError();

                // TODO: Mostrar confirmación antes de eliminar
                // var confirmed = await ShowConfirmationAsync("¿Está seguro de eliminar este producto?");
                // if (!confirmed) return;

                await _productService.DeleteProductAsync(product.Id);

                // Recargar la lista
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                SetError($"Error al eliminar producto: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}