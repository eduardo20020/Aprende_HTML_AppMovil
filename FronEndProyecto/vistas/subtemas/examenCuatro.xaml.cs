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
    public partial class examenCuatro : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        private int currentQuestionIndex = 0;
        private List<ContentView> questions = new List<ContentView>();

        public examenCuatro()
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
                        new Label
                        {
                            Text = "1. ¿Cuál es la estructura básica de un formulario HTML?",
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                        },
                        new RadioButton { GroupName = "Q1", Content = "A) Un formulario HTML está compuesto por etiquetas <form>, <input>, <label> y <button>, entre otras." },
                        new RadioButton { GroupName = "Q1", Content = "B) Un formulario HTML solo necesita una etiqueta <form> y nada más." },
                        new RadioButton { GroupName = "Q1", Content = "C) Un formulario HTML no tiene estructura." },
                        new RadioButton { GroupName = "Q1", Content = "D) Un formulario HTML se define solo con <input> y <button>." }
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
                        new Label
                        {
                            Text = "2. ¿Cómo se utiliza la etiqueta <input> en un formulario HTML y qué atributos son importantes?",
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                        },
                        new RadioButton { GroupName = "Q2", Content = "A) La etiqueta <input> se usa para crear controles interactivos en un formulario, y los atributos como type, name y value son esenciales." },
                        new RadioButton { GroupName = "Q2", Content = "B) La etiqueta <input> solo se usa para botones." },
                        new RadioButton { GroupName = "Q2", Content = "C) La etiqueta <input> no requiere atributos." },
                        new RadioButton { GroupName = "Q2", Content = "D) La etiqueta <input> es opcional en los formularios." }
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
                        new Label
                        {
                            Text = "3. ¿Qué es la validación de formularios en HTML y cómo se implementa?",
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                        },
                        new RadioButton { GroupName = "Q3", Content = "A) La validación de formularios es el proceso de verificar los datos ingresados por el usuario antes de enviarlos. Se puede implementar utilizando atributos como required, minlength y pattern." },
                        new RadioButton { GroupName = "Q3", Content = "B) La validación de formularios solo se hace en el servidor." },
                        new RadioButton { GroupName = "Q3", Content = "C) La validación de formularios no es necesaria." },
                        new RadioButton { GroupName = "Q3", Content = "D) La validación de formularios se realiza solo con JavaScript." }
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
                        new Label
                        {
                            Text = "4. ¿Cuál es la diferencia entre el método GET y POST en los formularios HTML?",
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                        },
                        new RadioButton { GroupName = "Q4", Content = "A) GET envía los datos del formulario en la URL, mientras que POST envía los datos en el cuerpo de la solicitud, siendo más seguro para enviar información sensible." },
                        new RadioButton { GroupName = "Q4", Content = "B) GET y POST son exactamente lo mismo." },
                        new RadioButton { GroupName = "Q4", Content = "C) POST envía los datos en la URL, mientras que GET los envía en el cuerpo de la solicitud." },
                        new RadioButton { GroupName = "Q4", Content = "D) GET y POST no se usan en formularios HTML." }
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
            if (GetSelectedAnswer("Q1") == "A) Un formulario HTML está compuesto por etiquetas <form>, <input>, <label> y <button>, entre otras.") score++;
            if (GetSelectedAnswer("Q2") == "A) La etiqueta <input> se usa para crear controles interactivos en un formulario, y los atributos como type, name y value son esenciales.") score++;
            if (GetSelectedAnswer("Q3") == "A) La validación de formularios es el proceso de verificar los datos ingresados por el usuario antes de enviarlos. Se puede implementar utilizando atributos como required, minlength y pattern.") score++;
            if (GetSelectedAnswer("Q4") == "A) GET envía los datos del formulario en la URL, mientras que POST envía los datos en el cuerpo de la solicitud, siendo más seguro para enviar información sensible.") score++;

            if (score == 4)
            {
                await DisplayAlert("Pasaste", "Felicidades, obtuviste 4/4", "OK");
                // Lógica para manejar el caso de aprobación completa, como enviar datos al servidor o actualizar la UI
                var progresoUsuario = new
                {
                    correo = Preferences.Get("correo", "sinNombre"),
                    progreso = "100%"
                };
                Preferences.Set("progreso", "100%");

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
                        await DisplayAlert("Error", "Error al actualizar el progreso", "OK");
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "Error en la conexión", "OK");
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
