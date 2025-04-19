using Microsoft.Maui.Controls;
using SimpleProductApp.ViewModels;
using SimpleProductApp.Services;
using Hairullin;
using ProductApp.Services;

namespace SimpleProductApp.Views
{
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
        {
            InitializeComponent();
            var productService = new ProductService(new ApplicationDbContext());
            BindingContext = new ProductViewModel(productService);
        }
    }
}