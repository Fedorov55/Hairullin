using Hairullin.Entities;
using Microsoft.Maui.Media;
using Microsoft.Maui.Controls;
using System.IO;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;


namespace Hairullin
{
    public partial class AddTovarPage : ContentPage
    {
        private byte[] _imageData;  // Поле для хранения массива байтов изображения

        public AddTovarPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            var description = DescriptionEntry.Text;
            var price = PriceEntry.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(price))
            {
                await DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            if (_imageData == null || _imageData.Length == 0)
            {
                await DisplayAlert("Предупреждение", "Изображение не выбрано.  Товар будет сохранен без изображения.", "OK");
                
                return;
            }

            var newProduct = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                Image = _imageData // Сохраняем BLOB data (byte[])
            };

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Product.Add(newProduct);
                    await db.SaveChangesAsync();

                    await DisplayAlert("Успех", "Товар сохранен", "OK");

                    // Очищаем поля после успешного сохранения
                    NameEntry.Text = string.Empty;
                    DescriptionEntry.Text = string.Empty;
                    PriceEntry.Text = string.Empty;
                    _imageData = null; // Очищаем данные изображения
                    ImagePreview.Source = null; // Очищаем ImagePreview (если он у вас есть)
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось сохранить товар: {ex.Message}", "OK");
            }
        }

        private async void OnPickImageClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(); // Или TakePhotoAsync()

            if (result != null)
            {
                _imageData = await ConvertImageToByteArray(result); // Преобразуем FileResult в byte[]

                if (_imageData != null)
                {
                    // Отображаем превью изображения (если нужно)
                    ImagePreview.Source = ImageSource.FromStream(() => new MemoryStream(_imageData));
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось преобразовать изображение.", "OK");
                }
            }
        }

        // Вспомогательные методы (ConvertImageToByteArray и ReadFully)
        public async Task<byte[]> ConvertImageToByteArray(FileResult photo)
        {
            if (photo != null)
            {
                using (Stream stream = await photo.OpenReadAsync())
                {
                    byte[] imageData = await ReadFully(stream);
                    return imageData;
                }
            }
            return null;
        }

        public async Task<byte[]> ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await input.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuAdminPage());
        }
    }
}