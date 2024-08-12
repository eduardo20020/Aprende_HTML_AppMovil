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
    public partial class getLecciones : ContentPage
    {
        private StackLayout _stackLayout = new StackLayout();
        private HttpClient _client = new HttpClient();
        private Label _lblInfo;
        public getLecciones()
        {
            InitializeComponent();
            ScrollView scrollview = new ScrollView();
            _lblInfo = new Label { 
                Text = "esto es un label de informacion"
            };
            _stackLayout.Children.Add(_lblInfo);
            _stackLayout.BackgroundColor = Color.LightSteelBlue;
            scrollview.Content = _stackLayout;
            peticionGet();
            Content = scrollview;

        }


        public async  void peticionGet()
        {
            try {
                string url = "https://apibrandon.eastus.cloudapp.azure.com/api/getlecciones.php";
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(responseBody);

                foreach (Root leccionCada in myDeserializedClass)
                {
                    // Crear un nuevo StackLayout para cada lección
                    var stackLayoutLeccion = new StackLayout();

                    // Crear un nuevo Label para mostrar el contenido de la lección (por ejemplo, el nombre de la lección)
                    var labelLeccion = new Label
                    {
                        Text = leccionCada.leccion, // Aquí deberías asignar la propiedad que deseas mostrar
                        FontSize = 20,
                        TextColor = Color.Blue,
                        HorizontalTextAlignment = TextAlignment.Center
                        
                    };


                    var lblcontenido = new Label
                    {
                        Text = leccionCada.contenido, // Aquí deberías asignar la propiedad que deseas mostrar
                        FontSize = 16,
                        TextColor = Color.Black
                    };

                    var lblejemplo = new Label
                    {
                        Text = leccionCada.ejemplo, // Aquí deberías asignar la propiedad que deseas mostrar
                        FontSize = 16,
                        TextColor = Color.Black
                    };

                    var lblrecursos = new Label
                    {
                        Text = leccionCada.recursos, // Aquí deberías asignar la propiedad que deseas mostrar
                        FontSize = 16,
                        TextColor = Color.Black

                    };

                    var separador = new BoxView
                    {
                        HeightRequest = 1,       // Altura del separador
                        BackgroundColor = Color.Gray, // Color del separador
                        HorizontalOptions = LayoutOptions.FillAndExpand, // Ocupar todo el ancho disponible
                        Margin = new Thickness(0, 10) // Espacio superior e inferior opcional
                    };




                    // Agregar el Label al StackLayout
                    stackLayoutLeccion.Children.Add(labelLeccion);
                    stackLayoutLeccion.Children.Add(lblcontenido);
                    stackLayoutLeccion.Children.Add(new Label { Text = "Ejemplo: ", TextColor = Color.White });
                    stackLayoutLeccion.Children.Add(lblejemplo);
                    stackLayoutLeccion.Children.Add(new Label { Text = "recursos: ", TextColor = Color.White });
                    stackLayoutLeccion.Children.Add(lblrecursos);
                    stackLayoutLeccion.Children.Add(separador);





                    // Agregar el StackLayout al StackLayout principal
                    _stackLayout.Children.Add(stackLayoutLeccion);
                }



            }
            catch (Exception ex)
            {

            }



        }



    }




    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Root
    {
        public string id_leccion { get; set; }
        public string leccion { get; set; }
        public string contenido { get; set; }
        public string ejemplo { get; set; }
        public string recursos { get; set; }
    }

}