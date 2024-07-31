using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FronEndProyecto.vistas.config
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registrar : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        public registrar()
        {
            InitializeComponent();
        }

        private async void postRegistrar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nombreDos.Text) ||
                string.IsNullOrEmpty(correoDos.Text) ||
                string.IsNullOrEmpty(contraDos.Text))
            {
                await DisplayAlert("Error", "Rellena todos los campos", "OK");
                // No redirigir aquí, solo mostrar el alerta
                return;
            }

            var usuario = new
            {
                nombre = nombreDos.Text,
                correo = correoDos.Text,
                contra = contraDos.Text
            };

            string json = JsonSerializer.Serialize(usuario);

            try
            {
                var url = "https://apibrandon.eastus.cloudapp.azure.com/api/registrar.php"; // Reemplaza con tu URL
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody == "El correo ya existe")
                    {
                        await DisplayAlert("Error", "El correo ya existe", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Registrado", responseBody, "OK");
                        await Navigation.PushModalAsync(new login());
                    }
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo conectar con el servidor.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error al usuario
                await DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
            }
        }

        private void iniciarSesion(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new login());
        }
    }
}
