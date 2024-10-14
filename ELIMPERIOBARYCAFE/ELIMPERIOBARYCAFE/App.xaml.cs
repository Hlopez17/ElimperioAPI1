namespace ELIMPERIOBARYCAFE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Reserva());
        }
    }
}
