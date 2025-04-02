using System;
using System.Threading.Tasks;
using PushInOutProyect.Conexion;

namespace PushInOutProyect.Views.Auth
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailEntry?.Text?.Trim();
            string password = passwordEntry?.Text?.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Debe ingresar email y contrase�a", "OK");
                return;
            }

            try
            {
                ConexionFirebase conexionFirebase = new ConexionFirebase();
                var credenciales = await conexionFirebase.CargarUsuario(email, password);
                var uid = credenciales.User.Uid;

                await DisplayAlert("Bienvenido", $"UID: {uid}", "OK");

                // Redirigir a la p�gina principal despu�s de un inicio de sesi�n exitoso
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
    
}
