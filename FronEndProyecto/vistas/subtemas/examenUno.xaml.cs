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
                CreateQuestionGrid("Pregunta 1: ¿Cuál es la diferencia entre costos fijos y costos variables?", "Q1",
                    new[] {
                        "A) Los costos fijos cambian con el nivel de producción, mientras que los costos variables permanecen constantes.",
                        "B) Los costos fijos permanecen constantes independientemente del nivel de producción, mientras que los costos variables cambian con el nivel de producción. Ejemplo de costos fijos: alquiler; ejemplo de costos variables: materiales.",
                        "C) Ambos costos permanecen constantes independientemente del nivel de producción."
                    }),
                CreateQuestionGrid("Pregunta 2: ¿Cuál es la diferencia entre costos directos e indirectos?", "Q2",
                    new[] {
                        "A) Los costos directos se asignan directamente a un proyecto específico, mientras que los costos indirectos se reparten entre varios proyectos. Ejemplo de costo directo: materiales; ejemplo de costo indirecto: seguridad.",
                        "B) Los costos indirectos se asignan directamente a un proyecto específico, mientras que los costos directos se reparten entre varios proyectos.",
                        "C) Ambos tipos de costos se reparten entre varios proyectos."
                    }),
                CreateQuestionGrid("Pregunta 3: ¿Qué es un presupuesto incremental?", "Q3",
                    new[] {
                        "A) Un presupuesto incremental justifica cada gasto desde cero.",
                        "B) Un presupuesto incremental aumenta los gastos del año anterior en un porcentaje específico, mientras que un presupuesto de base cero justifica cada gasto desde cero.",
                        "C) Un presupuesto incremental es lo mismo que un presupuesto de base cero."
                    }),
                CreateQuestionGrid("Pregunta 4: ¿Cuál es el propósito del análisis de costos y beneficios?", "Q4",
                    new[] {
                        "A) El análisis de costos y beneficios se utiliza para determinar el costo total de un proyecto.",
                        "B) El análisis de costos y beneficios ayuda a determinar si los beneficios superan los costos, lo que facilita la toma de decisiones informadas.",
                        "C) El análisis de costos y beneficios solo se utiliza en proyectos gubernamentales."
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
            if (q1 == "B) Los costos fijos permanecen constantes independientemente del nivel de producción, mientras que los costos variables cambian con el nivel de producción. Ejemplo de costos fijos: alquiler; ejemplo de costos variables: materiales.") score++;
            if (q2 == "A) Los costos directos se asignan directamente a un proyecto específico, mientras que los costos indirectos se reparten entre varios proyectos. Ejemplo de costo directo: materiales; ejemplo de costo indirecto: seguridad.") score++;
            if (q3 == "B) Un presupuesto incremental aumenta los gastos del año anterior en un porcentaje específico, mientras que un presupuesto de base cero justifica cada gasto desde cero.") score++;
            if (q4 == "B) El análisis de costos y beneficios ayuda a determinar si los beneficios superan los costos, lo que facilita la toma de decisiones informadas.") score++;


            if (score == 4)
            {
                DisplayAlert("Pasaste", "felicidades hiciste 4/4", "ok");
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






                /////////
            }
            else if (score < 4)
            {
                DisplayAlert("No Aprobado", $"Tu puntuación es {score}/4", "ok");
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