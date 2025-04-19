namespace Hairullin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            using var dbContext = new ApplicationDbContext();
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}
