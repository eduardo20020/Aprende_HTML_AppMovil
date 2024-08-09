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
                CreateQuestionGrid("Pregunta 1: ¿Cuál es la importancia del análisis de costos en la planificación y ejecución de un proyecto?", "Q1",
                    new[] {
                        "A) El análisis de costos no tiene importancia en la planificación y ejecución de un proyecto.",
                        "B) El análisis de costos permite identificar, estimar y controlar los costos asociados, asegurando que el proyecto se mantenga dentro del presupuesto.",
                        "C) El análisis de costos solo es importante para proyectos a corto plazo.",
                        "D) El análisis de costos se realiza solo una vez al inicio del proyecto y luego se ignora."
                    }),
                CreateQuestionGrid("Pregunta 2: ¿Cómo se puede utilizar el método del valor ganado para evaluar el desempeño financiero de un proyecto?", "Q2",
                    new[] {
                        "A) Comparando el valor del trabajo realizado con el costo real y el presupuesto planificado para medir el progreso y el desempeño.",
                        "B) El método del valor ganado no se puede utilizar para evaluar el desempeño financiero de un proyecto.",
                        "C) Aplicando un porcentaje fijo de costos indirectos a los costos directos.",
                        "D) Utilizando solo el costo planificado sin considerar el trabajo realizado."
                    }),
                CreateQuestionGrid("Pregunta 3: Explique el concepto de control de costos y cómo se puede aplicar en un proyecto de desarrollo de software.", "Q3",
                    new[] {
                        "A) El control de costos no es aplicable en proyectos de desarrollo de software.",
                        "B) El control de costos implica monitorear y gestionar los gastos del proyecto para asegurarse de que no excedan el presupuesto asignado.",
                        "C) El control de costos significa reducir el presupuesto total del proyecto a la mitad.",
                        "D) El control de costos se realiza solo al final del proyecto."
                    }),
                CreateQuestionGrid("Pregunta 4: ¿Qué es un análisis de brechas y cómo puede ayudar a ajustar el presupuesto de un proyecto?", "Q4",
                    new[] {
                        "A) Un análisis de brechas es una herramienta para aumentar los costos de un proyecto.",
                        "B) Un análisis de brechas identifica las diferencias entre los costos planificados y reales, ayudando a ajustar el presupuesto y mejorar la precisión de futuras estimaciones.",
                        "C) Un análisis de brechas solo se utiliza después de que el proyecto ha finalizado.",
                        "D) Un análisis de brechas es irrelevante para la presupuestación de proyectos."
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
            if (q1 == "B) El análisis de costos permite identificar, estimar y controlar los costos asociados, asegurando que el proyecto se mantenga dentro del presupuesto.") score++;
            if (q2 == "A) Comparando el valor del trabajo realizado con el costo real y el presupuesto planificado para medir el progreso y el desempeño.") score++;
            if (q3 == "B) El control de costos implica monitorear y gestionar los gastos del proyecto para asegurarse de que no excedan el presupuesto asignado.") score++;
            if (q4 == "B) Un análisis de brechas identifica las diferencias entre los costos planificados y reales, ayudando a ajustar el presupuesto y mejorar la precisión de futuras estimaciones.") score++;

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
