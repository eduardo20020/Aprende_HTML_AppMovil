using Newtonsoft.Json;
using OxyPlot;
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
        private static readonly HttpClient client = new HttpClient();

        public dashboard2()
        {
            InitializeComponent();
            InitializeUI();
            // Crear un nuevo modelo de gráfico




            // Asignar el modelo al PlotView
            LoadContent();
        }

        private void InitializeUI()
        {
            _myStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                Spacing = 10
            };

            _myLabel = new Label { Text = "Primera Etiqueta" };
            _myStackLayout.Children.Add(_myLabel);

            // Envolver el StackLayout en un ScrollView
            ScrollView scrollView = new ScrollView
            {
                Content = _myStackLayout
            };
            Content = scrollView;
        }

        private async void LoadContent()
        {
            try
            {
                string url = "https://apibrandon.eastus.cloudapp.azure.com/api/secciones.php";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar el JSON
                Root root = JsonConvert.DeserializeObject<Root>(responseBody);

                if (root?.Lecciones != null)
                {
                    foreach (Leccion leccion in root.Lecciones)
                    {
                        _myStackLayout.Children.Add(new Label
                        {
                            Text = $"-{leccion.Titulo}-",
                            FontAttributes = FontAttributes.Bold,
                            FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                            HorizontalTextAlignment = TextAlignment.Center
                        });

                        foreach (Subtemas subtema in leccion.Subtemas)
                        {
                            _myStackLayout.Children.Add(new Label
                            {
                                Text = subtema.Subtema,
                                FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label))
                            });
                        }
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
            catch (Exception e)
            {
                _myLabel.Text = $"Error inesperado: {e.Message}";
            }
        }
    }

    public class Leccion
    {
        public string Titulo { get; set; }
        public List<Subtemas> Subtemas { get; set; }
    }

    public class Root
    {
        public List<Leccion> Lecciones { get; set; }
    }

    public class Subtemas
    {
        public string Subtema { get; set; }
        public string Titulo { get; set; }
        public string Concepto { get; set; }
        public string Ejemplo { get; set; }
        public string Video { get; set; }
    }
}
