using System.ComponentModel;
using System.Windows.Input;
using Hairullin.Entities;
using Microsoft.Maui.Controls;
using static Android.Util.EventLogTags;
using static Java.Util.Jar.Attributes;

namespace YourNamespace.ViewModels;

public class ProductDetailsViewModel : INotifyPropertyChanged
{
    private Product _product;
    private ImageSource _imageSource;  // ImageSource

    //...

    public ProductDetailsViewModel()
    {
        //...
        LoadProductData(); // Загружаем данные и преобразуем изображение
    }

    //Добавим метод асинхронной загрузки, чтобы не блокировать UI
    private async Task LoadProductData()
    {
        Product product = await GetProductFromDatabase(123); // ID товара (пример)

        if (product != null)
        {
            _product = product;
            Name = product.Name;
            Description = product.Description;
            Price = decimal.Parse(product.Price);

            // Преобразуем byte[] в ImageSource
            ImageSource = ByteArrayToImageSource(product.Image);

        }
    }
    //Теперь свойство imageSource используется
    public ImageSource ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged(nameof(ImageSource));
        }
    }

    //Временный метод, в нем вы должны загрузить данные из вашей БД
    private async Task<Product> GetProductFromDatabase(int productId)
    {
        //Создаем тестовый продукт
        return new Product()
        {
            Id = 123,
            Name = "Test",
            Description = "Desc",
            Price = "100",
            Image = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29 }
        };
    }
}