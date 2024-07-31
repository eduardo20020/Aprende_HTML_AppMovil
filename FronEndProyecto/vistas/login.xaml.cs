using FronEndProyecto.vistas.config;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        public login()
        {
            InitializeComponent();
        }

        private async void IniciarSesion(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(correoDos.Text) || string.IsNullOrEmpty(contraDos.Text))
            {
                await DisplayAlert("Error", "Rellena todos los campos", "OK");
                return;
            }

            var usuario = new
            {
                correo = correoDos.Text,
                contra = contraDos.Text
            };

            string json = JsonSerializer.Serialize(usuario);

            try
            {
                var url = "https://apibrandon.eastus.cloudapp.azure.com/api/iniciarSesion.php"; // Reemplaza con tu URL
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody);

                    if (loginResponse.mensaje == "Inicio de sesion correcto")
                    {
                        await DisplayAlert("Correcto", $"{loginResponse.mensaje}\nNombre: {loginResponse.nombre}\nCorreo: {loginResponse.correo}", "OK");
                        await Navigation.PushModalAsync(new SectionsPage());
                        Preferences.Set("nombre", loginResponse.nombre);
                        Preferences.Set("correo", loginResponse.correo);
                        Preferences.Set("progreso", loginResponse.progreso);


                    }
                    else if (loginResponse.mensaje == "nombre o contrase na incorrectods")
                    {
                        await DisplayAlert("INcorrecto", $"nombre o contrasena incorrectos", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", responseBody, "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Error al iniciar sesión", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private void Registrar(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new registrar());
        }
    }

    public class LoginResponse
    {
        public string mensaje { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string progreso { get; set; }
    }
}
