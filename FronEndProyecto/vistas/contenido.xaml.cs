using FronEndProyecto.modelos;
using FronEndProyecto.vistas.secciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class contenido : ContentPage
    {
        private StackLayout _stackLayout;
        public HttpClient client = new HttpClient();
        private Label _mylabel;
        public contenido()
        {
            InitializeComponent();

            var scrollView = new ScrollView();

            _stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10),
                Spacing = 10
            };

            _mylabel = new Label { Text = "aqui va algun error " };

            _stackLayout.Children.Add(_mylabel);
            _stackLayout.Children.Add(new Label { Text = "Primer Label" });
            _stackLayout.Children.Add(new Label { Text = "Segundo Label" });
            _stackLayout.Children.Add(new Button { Text = "Un botón" });

            scrollView.Content = _stackLayout;
            Content = scrollView;
            cargarContenido();

        }


        private async void cargarContenido()
        {
            try
            {
                string url = "https://apibrandon.eastus.cloudapp.azure.com/api/secciones.php";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar el JSON
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseBody);

                // Limpiar el StackLayout antes de agregar nuevo contenido
                _stackLayout.Children.Clear();

                // Recorrer las lecciones y agregar su contenido al StackLayout
                foreach (var leccion in myDeserializedClass.Lecciones)
                {
                    // Agregar el título de la lección
                    var leccionLabel = new Label
                    {
                        Text = leccion.Titulo,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 26,
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    _stackLayout.Children.Add(leccionLabel);

                    // Recorrer los subtemas de la lección
                    foreach (var subtema in leccion.Subtemas)
                    {
                        // Agregar el título del subtema
                        var subtemaLabel = new Label
                        {
                            Text = subtema.Titulo,
                            FontAttributes = FontAttributes.Italic,
                            FontSize = 18,
                            TextColor = Color.White
                        };
                        _stackLayout.Children.Add(subtemaLabel);

                        // Agregar el concepto
                        var conceptoLabel = new Label
                        {
                            Text = $"Concepto: {subtema.Concepto}",
                            FontSize = 16
                        };
                        _stackLayout.Children.Add(conceptoLabel);

                        // Agregar el ejemplo
                        var ejemploLabel = new Label
                        {
                            Text = $"Ejemplo: {subtema.Ejemplo}",
                            FontSize = 16
                        };
                        _stackLayout.Children.Add(ejemploLabel);

                        // Agregar el video (puedes enlazarlo o mostrarlo de otra forma si es necesario)
                        var videoLabel = new Label
                        {
                            Text = $"Video: {subtema.Video}",
                            FontSize = 16,
                            TextColor = Color.Blue
                        };
                        _stackLayout.Children.Add(videoLabel);
                    }
                }

            }
            catch (HttpRequestException e)
            {
                _mylabel.Text = $"Error en la solicitud HTTP: {e.Message}";
            }
            catch (JsonException e)
            {
                _mylabel.Text = $"Error en la deserialización JSON: {e.Message}";
            }
            catch (Exception e)
            {
                _mylabel.Text = $"Error inesperado: {e.Message}";
            }
        }











        public class Leccione
        {
            public string Leccion { get; set; }
            public string Titulo { get; set; }
            public List<Subtemas> Subtemas { get; set; }
        }

        public class Root
        {
            public List<Leccione> Lecciones { get; set; }
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
}
