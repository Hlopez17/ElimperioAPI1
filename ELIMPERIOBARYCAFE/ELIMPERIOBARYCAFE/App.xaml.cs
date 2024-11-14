using ELIMPERIOBARYCAFE.MVVM.View;

namespace ELIMPERIOBARYCAFE
{
    public partial class App : Application
    {
        public string token;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Mesero());
        }
    }
}
