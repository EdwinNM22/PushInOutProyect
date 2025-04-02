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
            await DisplayAlert("Error", "Debe ingresar email y contrase�a", "OK");
            return;
        }

        try
        {
            ConexionFirebase conexionFirebase = new ConexionFirebase();
            var usuario = await conexionFirebase.CrearUsuario(email, password);
            await DisplayAlert("�xito", "Usuario registrado con �xito", "OK");

            // Puedes redirigir a la p�gina de inicio de sesi�n si lo deseas
            await Navigation.PushAsync(new Login());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}