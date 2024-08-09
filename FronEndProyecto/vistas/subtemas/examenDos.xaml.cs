using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.subtemas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class examenDos : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        private int currentQuestionIndex;
        private Grid[] questionGrids;

        public examenDos()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayCurrentQuestion();
        }

        private void LoadQuestions()
        {
            questionGrids = new Grid[]
            {
                CreateQuestionGrid("Pregunta 1: ¿Cuál es la función del atributo 'color' en CSS?", "Q1",
                    new[] {
                        "A) Cambiar el color de fondo de un elemento.",
                        "B) Cambiar el color del texto de un elemento.",
                        "C) Cambiar el color del borde de un elemento.",
                        "D) Cambiar el color de todos los elementos en la página."
                    }),
                CreateQuestionGrid("Pregunta 2: ¿Cómo se aplica una clase en CSS a un elemento HTML?", "Q2",
                    new[] {
                        "A) Usando el símbolo '.' seguido del nombre de la clase en el archivo CSS.",
                        "B) Usando el símbolo '#' seguido del nombre de la clase en el archivo CSS.",
                        "C) Usando el símbolo '@' seguido del nombre de la clase en el archivo CSS.",
                        "D) Usando el símbolo '!' seguido del nombre de la clase en el archivo CSS."
                    }),
                CreateQuestionGrid("Pregunta 3: ¿Qué propiedad de CSS se utiliza para cambiar el tamaño del texto?", "Q3",
                    new[] {
                        "A) font-style",
                        "B) font-weight",
                        "C) font-size",
                        "D) font-family"
                    }),
                CreateQuestionGrid("Pregunta 4: ¿Cuál es la diferencia entre 'margin' y 'padding' en CSS?", "Q4",
                    new[] {
                        "A) Margin se refiere al espacio dentro de un elemento, mientras que Padding se refiere al espacio fuera de un elemento.",
                        "B) Margin se refiere al espacio fuera de un elemento, mientras que Padding se refiere al espacio dentro de un elemento.",
                        "C) Margin y Padding son lo mismo en CSS.",
                        "D) Margin se usa para el texto y Padding para las imágenes."
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
            if (q1 == "B) Cambiar el color del texto de un elemento.") score++;
            if (q2 == "A) Usando el símbolo '.' seguido del nombre de la clase en el archivo CSS.") score++;
            if (q3 == "C) font-size") score++;
            if (q4 == "B) Margin se refiere al espacio fuera de un elemento, mientras que Padding se refiere al espacio dentro de un elemento.") score++;

            if (score == 4)
            {
                await DisplayAlert("Pasaste", "Felicidades, obtuviste 4/4", "OK");

                var progresoUsuario = new
                {
                    correo = Preferences.Get("correo", "sinNombre"),
                    progreso = "50%"
                };
                Preferences.Set("progreso", "50%");

                string json = JsonSerializer.Serialize(progresoUsuario);

                try
                {
                    var url = "https://apibrandon.eastus.cloudapp.azure.com/api/progreso.php";
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var loginResponse = JsonSerializer.Deserialize<ProgresoResponse>(responseBody);
                        DisplayAlert("Progreso Actualizado", $"{responseBody}", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error al iniciar sesión", "OK");
                    }
                }
                catch
                {
                    // Manejo de excepciones si es necesario
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
}
