

using YourNamespace;

namespace Hairullin
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
            Routing.RegisterRoute(nameof(MenuAdminPage), typeof(MenuAdminPage));
            Routing.RegisterRoute(nameof(AddTovarPage), typeof(AddTovarPage));
            Routing.RegisterRoute(nameof(UserListPage), typeof(UserListPage));


        }
    }
}
