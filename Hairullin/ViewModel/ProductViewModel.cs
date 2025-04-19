using System.Collections.ObjectModel;
using Hairullin.Entities;
using ProductApp.Services;
using Hairullin.Entities;
using Hairullin.Crevic;

namespace SimpleProductApp.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public ProductViewModel(ProductService productService)
        {
            _productService = productService;
            LoadProducts();
        }

        private async void LoadProducts()
        {
            Products = await _productService.GetProductsAsync();
        }
    }
}