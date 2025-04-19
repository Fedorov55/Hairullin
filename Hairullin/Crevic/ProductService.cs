using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Hairullin.Entities;
using Hairullin;

namespace ProductApp.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ObservableCollection<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.ImageSource = ConvertByteArrayToImageSource(product.Image);
            }
            return new ObservableCollection<Product>(products);
        }

        private ImageSource ConvertByteArrayToImageSource(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return null; // Или верните изображение по умолчанию
            }

            return ImageSource.FromStream(() => new MemoryStream(imageData));
        }
    }
}