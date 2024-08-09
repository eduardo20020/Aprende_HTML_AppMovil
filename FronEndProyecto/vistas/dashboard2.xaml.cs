using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class dashboard2 : ContentPage
    {
        private Label _myLabel;
        private StackLayout _myStackLayout;
        public dashboard2()
        {
            InitializeComponent();
            _myStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                Spacing = 10
            };

            _myLabel = new Label { Text = "Primera Etiqueta" };
            pedirContenido();

            // Agregar elementos al StackLayout
            _myStackLayout.Children.Add(_myLabel);
            // Establecer el StackLayout como el contenido de la página
            // Envolver el StackLayout en un ScrollView
            ScrollView scrollView = new ScrollView
            {
                Content = _myStackLayout
            };
            Content = scrollView; ;

        }

        public async void pedirContenido()
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = "https://apibrandon.eastus.cloudapp.azure.com/api/secciones.php";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar el JSON como una lista de objetos 'Root'
                List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(responseBody);

                foreach (Root secciones in myDeserializedClass)
                {
                    _myStackLayout.Children.Add(new Label { Text = $"-{secciones.nombre}-", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center });

                    foreach (string subtema in secciones.subtemas)
                    {
                        _myStackLayout.Children.Add(new Label { Text = subtema, FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label)) });
                    }
                }

            }
            catch (HttpRequestException e)
            {
                _myLabel.Text = $"Error en la solicitud HTTP: {e.Message}";
            }
            catch (JsonException e)
            {
                _myLabel.Text = $"Error en la deserialización JSON: {e.Message}";
            }
        }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Root
    {
        public string nombre { get; set; }
        public List<string> subtemas { get; set; }
    }



}
