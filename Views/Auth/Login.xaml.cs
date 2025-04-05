using System;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
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
                await DisplayAlert("Error", "Debe ingresar email y contraseña", "OK");
                return;
            }

            try
            {
                ConexionFirebase conexionFirebase = new ConexionFirebase();
                var credenciales = await conexionFirebase.CargarUsuario(email, password);
                var uid = credenciales.User.Uid;

                // Guardar las credenciales en SecureStorage
                await SecureStorage.Default.SetAsync("email", email);
                await SecureStorage.Default.SetAsync("password", password);

                await DisplayAlert("Bienvenido", $"UID: {uid}", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnFingerprintClicked(object sender, EventArgs e)
        {
            var availability = await CrossFingerprint.Current.GetAvailabilityAsync();

            if (availability != FingerprintAvailability.Available)
            {
                await DisplayAlert("Error", "La autenticación biométrica no está disponible", "OK");
                return;
            }

            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration(
                "Autenticación requerida", "Usa tu huella para iniciar sesión"));

            if (result.Authenticated)
            {
                var email = await SecureStorage.Default.GetAsync("email");
                var password = await SecureStorage.Default.GetAsync("password");

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Error", "No se encontraron credenciales guardadas", "OK");
                    return;
                }

                try
                {
                    ConexionFirebase conexionFirebase = new ConexionFirebase();
                    var credenciales = await conexionFirebase.CargarUsuario(email, password);
                    var uid = credenciales.User.Uid;

                    await DisplayAlert("Bienvenido", $"UID: {uid}", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Autenticación cancelada o fallida", "OK");
            }
        }
    }
}
