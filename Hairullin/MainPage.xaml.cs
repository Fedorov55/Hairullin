using YourNamespace;

namespace Hairullin
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToDetailPage(object sender, EventArgs e)
        {
            string username = UserName.Text;
            string password = UserPass.Text;

            if (username == "1" && password == "1")
            {
                await Navigation.PushAsync(new AdminPage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
            }
        }

        private async void GoToAdminPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminPage());
        }

        private async void GoToUserListPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserListPage());
        }
    }

}
