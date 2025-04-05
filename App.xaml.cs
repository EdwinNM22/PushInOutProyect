using PushInOutProyect.Views.Auth;

namespace PushInOutProyect
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Usamos NavigationPage para permitir la navegación
            MainPage = new NavigationPage(new Login());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Este código no es necesario para la navegación, puedes quitarlo
            return base.CreateWindow(activationState);
        }
    }

}