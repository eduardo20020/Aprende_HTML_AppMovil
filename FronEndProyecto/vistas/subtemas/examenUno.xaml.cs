using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.subtemas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class examenUno : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        private int currentQuestionIndex;
        private Grid[] questionGrids;

        public examenUno()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayCurrentQuestion();
        }


        private void LoadQuestions()
        {
            questionGrids = new Grid[]
            {
        CreateQuestionGrid("Pregunta 1: ¿Qué es HTML?", "Q1",
            new[] {
                "A) HTML es un lenguaje de programación.",
                "B) HTML es un lenguaje de marcado utilizado para estructurar y presentar contenido en la web.",
                "C) HTML es una hoja de estilo."
            }),
        CreateQuestionGrid("Pregunta 2: ¿Cuál es la estructura básica de un documento HTML?", "Q2",
            new[] {
                "A) Un documento HTML no tiene una estructura específica.",
                "B) La estructura básica incluye las etiquetas <html>, <head>, y <body>.",
                "C) Un documento HTML solo necesita la etiqueta <html>."
            }),
        CreateQuestionGrid("Pregunta 3: ¿Cuál es la función de los atributos en HTML?", "Q3",
            new[] {
                "A) Los atributos proporcionan información adicional sobre los elementos HTML.",
                "B) Los atributos se usan para crear nuevas etiquetas HTML.",
                "C) Los atributos no tienen función en HTML."
            }),
        CreateQuestionGrid("Pregunta 4: ¿Qué diferencia hay entre los elementos de bloque y los elementos inline en HTML?", "Q4",
            new[] {
                "A) Los elementos de bloque se presentan en la misma línea que otros elementos.",
                "B) Los elementos inline ocupan todo el ancho disponible.",
                "C) Los elementos de bloque ocupan todo el ancho disponible y se presentan en una nueva línea, mientras que los elementos inline solo ocupan el espacio necesario."
            })
            };
        }


        private Grid CreateQuestionGrid(string questionText, string groupName, string[] options)
        {
            var grid = new Grid
            {
                Padding = new Thickness(10),
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            grid.Children.Add(new Label
            {
                Text = questionText,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalOptions = LayoutOptions.Center
            }, 0, 0);

            int row = 1;
            foreach (var option in options)
            {
                var radioButton = new RadioButton
                {
                    Content = option,
                    GroupName = groupName,
                    Margin = new Thickness(0, 5, 0, 5),
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(RadioButton))
                };
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.Children.Add(radioButton, 0, row);
                row++;
            }

            return grid;
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < questionGrids.Length)
            {
                QuestionContainer.Content = questionGrids[currentQuestionIndex];
                PreviousButton.IsEnabled = currentQuestionIndex > 0;
                NextButton.Text = currentQuestionIndex < questionGrids.Length - 1 ? "Siguiente" : "Enviar";
            }
        }

        private void OnPreviousClicked(object sender, EventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayCurrentQuestion();
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questionGrids.Length - 1)
            {
                currentQuestionIndex++;
                DisplayCurrentQuestion();
            }
            else
            {
                OnSubmitClicked(sender, e);
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Obtener todas las respuestas seleccionadas
            var q1 = GetSelectedAnswer("Q1");
            var q2 = GetSelectedAnswer("Q2");
            var q3 = GetSelectedAnswer("Q3");
            var q4 = GetSelectedAnswer("Q4");

            // Comprobar respuestas
            int score = 0;
            if (q1 == "B) HTML es un lenguaje de marcado utilizado para estructurar y presentar contenido en la web.") score++;
            if (q2 == "B) La estructura básica incluye las etiquetas <html>, <head>, y <body>.") score++;
            if (q3 == "A) Los atributos proporcionan información adicional sobre los elementos HTML.") score++;
            if (q4 == "C) Los elementos de bloque ocupan todo el ancho disponible y se presentan en una nueva línea, mientras que los elementos inline solo ocupan el espacio necesario.") score++;

            if (score == 4)
            {
                await DisplayAlert("Pasaste", "Felicidades, obtuviste 4/4", "OK");
                // Lógica para manejar el caso de aprobación completa, como enviar datos al servidor o actualizar la UI
                /////////
                var progresoUsuario = new
                {
                    correo = Preferences.Get("correo", "sinNombre"),
                    progreso = "25%"
                };
                Preferences.Set("progreso", "25%");

                string json = JsonSerializer.Serialize(progresoUsuario);

                try
                {
                    var url = "https://apibrandon.eastus.cloudapp.azure.com/api/progreso.php"; // Reemplaza con tu URL
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var loginResponse = JsonSerializer.Deserialize<ProgresoResponse>(responseBody);
                        DisplayAlert("progreso actualizado", $"{responseBody}", "ok");

                    }
                    else
                    {
                        await DisplayAlert("Error", "Error al iniciar sesión", "OK");
                    }

                }
                catch
                {

                }

            }
            else
            {
                await DisplayAlert("No Aprobado", $"Tu puntuación es {score}/4", "OK");
            }
        }




        private string GetSelectedAnswer(string groupName)
        {
            return questionGrids
                .SelectMany(grid => grid.Children.OfType<RadioButton>())
                .FirstOrDefault(r => r.GroupName == groupName && r.IsChecked)?
                .Content.ToString();
        }
    }





    public class ProgresoResponse
    {
        public string error { get; set; }

        public string mensaje { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string progreso { get; set; }
    }
}