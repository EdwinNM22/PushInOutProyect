using PushInOutProyect.Conexion;

namespace PushInOutProyect.Views.Auth;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    private async void OnRegisterClicked(object sender, EventArgs e)
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
            var usuario = await conexionFirebase.CrearUsuario(email, password);
            await DisplayAlert("Éxito", "Usuario registrado con éxito", "OK");

            // Puedes redirigir a la página de inicio de sesión si lo deseas
            await Navigation.PushAsync(new Login());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}