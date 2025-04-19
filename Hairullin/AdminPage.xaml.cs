namespace Hairullin;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
	}

  

    private async void GoToAdminMenuPage(object sender, EventArgs e)
    {
        string username = UserName.Text;
        string password = UserPass.Text;

        if (username == "login" && password == "123")
        {
            await Navigation.PushAsync(new MenuAdminPage());
        }
        else
        {
            await DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
        }
    }
}