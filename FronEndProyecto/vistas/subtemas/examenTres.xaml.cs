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
    public partial class examenTres : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        private int currentQuestionIndex = 0;
        private List<ContentView> questions = new List<ContentView>();

        public examenTres()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestion();
        }

        private void LoadQuestions()
        {
            questions.Add(CreateQuestion1());
            questions.Add(CreateQuestion2());
            questions.Add(CreateQuestion3());
            questions.Add(CreateQuestion4());
        }

        private void DisplayQuestion()
        {
            QuestionContainer.Content = questions[currentQuestionIndex];
            PreviousButton.IsEnabled = currentQuestionIndex > 0;
            NextButton.Text = currentQuestionIndex < questions.Count - 1 ? "Siguiente" : "Enviar";
        }

        private ContentView CreateQuestion1()
        {
            return new ContentView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "1. ¿Cuál es la estructura básica de un formulario en HTML?" },
                        new RadioButton { GroupName = "Q1", Content = "A) Un formulario en HTML se estructura utilizando la etiqueta <form> y puede contener controles como <input>, <textarea>, y <select>." },
                        new RadioButton { GroupName = "Q1", Content = "B) Un formulario en HTML se estructura utilizando la etiqueta <div>." },
                        new RadioButton { GroupName = "Q1", Content = "C) Un formulario en HTML no necesita una estructura definida." },
                        new RadioButton { GroupName = "Q1", Content = "D) Un formulario en HTML se estructura solo con etiquetas <table>." }
                    }
                }
            };
        }

        private ContentView CreateQuestion2()
        {
            return new ContentView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "2. ¿Qué tipo de inputs existen en un formulario HTML?" },
                        new RadioButton { GroupName = "Q2", Content = "A) Solo hay un tipo de input en HTML." },
                        new RadioButton { GroupName = "Q2", Content = "B) Existen varios tipos de inputs, como texto, número, correo electrónico, etc." },
                        new RadioButton { GroupName = "Q2", Content = "C) Solo hay dos tipos de inputs en HTML: texto y número." },
                        new RadioButton { GroupName = "Q2", Content = "D) No existen inputs en HTML." }
                    }
                }
            };
        }

        private ContentView CreateQuestion3()
        {
            return new ContentView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "3. ¿Cuál es la importancia de la validación en un formulario HTML?" },
                        new RadioButton { GroupName = "Q3", Content = "A) La validación en un formulario HTML ayuda a asegurar que los datos ingresados sean válidos antes de enviar el formulario." },
                        new RadioButton { GroupName = "Q3", Content = "B) La validación en un formulario HTML no es importante." },
                        new RadioButton { GroupName = "Q3", Content = "C) La validación en un formulario HTML es importante solo en algunos casos." },
                        new RadioButton { GroupName = "Q3", Content = "D) La validación en un formulario HTML se realiza solo en el servidor." }
                    }
                }
            };
        }

        private ContentView CreateQuestion4()
        {
            return new ContentView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "4. ¿Cómo se envía y procesa un formulario en HTML?" },
                        new RadioButton { GroupName = "Q4", Content = "A) Un formulario en HTML se envía a través de la etiqueta <form> y se procesa en el servidor, donde se maneja la captura y validación de datos." },
                        new RadioButton { GroupName = "Q4", Content = "B) Un formulario en HTML no se puede enviar." },
                        new RadioButton { GroupName = "Q4", Content = "C) Un formulario en HTML se envía usando JavaScript." },
                        new RadioButton { GroupName = "Q4", Content = "D) Un formulario en HTML se envía solo con el botón 'Enviar' y no necesita procesamiento." }
                    }
                }
            };
        }

        private void OnPreviousClicked(object sender, EventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayQuestion();
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;
                DisplayQuestion();
            }
            else
            {
                OnSubmitClicked(sender, e);
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            int score = 0;
            // Verificar respuestas
            if (GetSelectedAnswer("Q1") == "A) Un formulario en HTML se estructura utilizando la etiqueta <form> y puede contener controles como <input>, <textarea>, y <select>.") score++;
            if (GetSelectedAnswer("Q2") == "B) Existen varios tipos de inputs, como texto, número, correo electrónico, etc.") score++;
            if (GetSelectedAnswer("Q3") == "A) La validación en un formulario HTML ayuda a asegurar que los datos ingresados sean válidos antes de enviar el formulario.") score++;
            if (GetSelectedAnswer("Q4") == "A) Un formulario en HTML se envía a través de la etiqueta <form> y se procesa en el servidor, donde se maneja la captura y validación de datos.") score++;

            if (score == 4)
            {
                await DisplayAlert("Pasaste", "Felicidades, obtuviste 4/4", "OK");
                // Lógica para manejar el caso de aprobación completa, como enviar datos al servidor o actualizar la UI
                /////////
                var progresoUsuario = new
                {
                    correo = Preferences.Get("correo", "sinNombre"),
                    progreso = "75%"
                };
                Preferences.Set("progreso", "75%");

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
            return questions
                .SelectMany(view => ((StackLayout)view.Content).Children.OfType<RadioButton>())
                .FirstOrDefault(rb => rb.GroupName == groupName && rb.IsChecked)?
                .Content.ToString();
        }
    }
}
