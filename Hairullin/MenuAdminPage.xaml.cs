using System.Collections.ObjectModel;
using Hairullin;
using Hairullin.Entities;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Hairullin;

public partial class MenuAdminPage : ContentPage
{
    public ObservableCollection<Product> Products { get; } = new();
    public ICommand DeleteProduct { get; set; }
    public MenuAdminPage()
	{
		InitializeComponent();
        BindingContext = this;
        DeleteProduct = new Command<int>(OnDeleteProduct);
        LoadProducts();
        
    }


    private async Task LoadProducts()
    {
        try
        {
            using (var db = new ApplicationDbContext())
            {
                var products = await db.Product.ToListAsync();

                Products.Clear();
                foreach (var product in products)
                {
                    // ����������� BLOB data (byte[]) � ImageSource ��� �����������
                    product.ImageSource = ConvertByteArrayToImageSource(product.Image);
                    Products.Add(product);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("������", ex.Message, "OK");
        }
    }

    public ImageSource ConvertByteArrayToImageSource(byte[] imageData)
    {
        if (imageData == null || imageData.Length == 0)
        {
            return null; // ��� ������� ����������� �� ���������
        }

        return ImageSource.FromStream(() => new MemoryStream(imageData));
    }


    public async void OnDeleteProduct(int productId)
    {
        if (productId <= 0) return; // �������� �� �������� Id

        try
        {
            using (var db = new ApplicationDbContext())
            {
                // ������� ������� � ���� ������ �� Id
                var productToDelete = await db.Product.FindAsync(productId);

                if (productToDelete == null)
                {
                    await DisplayAlert("������", "����� �� ������", "OK");
                    return;
                }

                bool confirm = await DisplayAlert("�������������",
                    $"������� ����� {productToDelete.Name}?", "��", "���");

                if (!confirm) return;

                db.Product.Remove(productToDelete);
                await db.SaveChangesAsync();

                // ������� ������� �� ObservableCollection (����� ������ ��� ����� ��������� �������� �� ��)
                var productToRemove = Products.FirstOrDefault(p => p.Id == productId);
                if (productToRemove != null)
                {
                    Products.Remove(productToRemove);
                }

                await DisplayAlert("�����", "����� ������", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("������", ex.Message, "OK");
        }
    }


    private async void GoAddTovarPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTovarPage());
    }

   
}



   