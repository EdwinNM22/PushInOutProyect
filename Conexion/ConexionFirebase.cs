using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace PushInOutProyect.Conexion
{
    internal class ConexionFirebase
    {
        private static FirebaseAuthClient ConexFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyAXrrqB8I9SsUkSXsYHykE3pdEiitV84c0",
                AuthDomain = "pushinoutproyect.web.app",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider(), // Soporta la autenticación por email y contraseña
                }
            };

            return new FirebaseAuthClient(config);
        }

        // Método para registrar un nuevo usuario
        public async Task<UserCredential> CrearUsuario(string email, string password)
        {
            var cliente = ConexFirebase();
            return await cliente.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        // Método para iniciar sesión con email y contraseña
        public async Task<UserCredential> CargarUsuario(string email, string password)
        {
            var cliente = ConexFirebase();
            return await cliente.SignInWithEmailAndPasswordAsync(email, password);
        }
    }
}