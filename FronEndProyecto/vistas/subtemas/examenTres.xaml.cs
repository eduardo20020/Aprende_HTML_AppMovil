using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.subtemas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class examenTres : ContentPage
    {
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
                        new Label { Text = "1. ¿Qué funciones ofrece un software de gestión financiera y cómo puede facilitar la creación y gestión de presupuestos?" },
                        new RadioButton { GroupName = "Q1", Content = "A) Un software de gestión financiera puede automatizar la entrada de datos, generar informes, y facilitar el seguimiento y análisis de los presupuestos." },
                        new RadioButton { GroupName = "Q1", Content = "B) Un software de gestión financiera solo sirve para la contabilidad." },
                        new RadioButton { GroupName = "Q1", Content = "C) Un software de gestión financiera no ofrece funciones útiles para la creación de presupuestos." },
                        new RadioButton { GroupName = "Q1", Content = "D) Un software de gestión financiera es solo para grandes corporaciones." }
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
                        new Label { Text = "2. ¿Cómo se aplica el método del valor ganado en la práctica y cuáles son sus principales ventajas y desventajas?" },
                        new RadioButton { GroupName = "Q2", Content = "A) Se aplica comparando el valor del trabajo realizado con el costo real y el presupuesto planificado. Ventajas: proporciona una visión precisa del progreso; desventajas: puede ser complejo de implementar." },
                        new RadioButton { GroupName = "Q2", Content = "B) No se aplica en la práctica, solo en teoría." },
                        new RadioButton { GroupName = "Q2", Content = "C) Se aplica utilizando un porcentaje fijo para todos los costos. Ventajas: es simple; desventajas: no es preciso." },
                        new RadioButton { GroupName = "Q2", Content = "D) Se aplica solo al final del proyecto. Ventajas: es fácil de calcular; desventajas: no proporciona información en tiempo real." }
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
                        new Label { Text = "3. ¿Qué es un análisis de brechas y cómo se realiza en el contexto de la presupuestación de proyectos?" },
                        new RadioButton { GroupName = "Q3", Content = "A) Es el proceso de comparar los costos planificados con los reales para identificar desviaciones y ajustar el presupuesto en consecuencia." },
                        new RadioButton { GroupName = "Q3", Content = "B) Es el proceso de aumentar el presupuesto del proyecto." },
                        new RadioButton { GroupName = "Q3", Content = "C) Es irrelevante para la presupuestación de proyectos." },
                        new RadioButton { GroupName = "Q3", Content = "D) Es solo un ejercicio teórico sin aplicación práctica." }
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
                        new Label { Text = "4. ¿Por qué es importante la presupuestación basada en actividades y cómo se implementa en un proyecto?" },
                        new RadioButton { GroupName = "Q4", Content = "A) No es importante para la presupuestación de proyectos." },
                        new RadioButton { GroupName = "Q4", Content = "B) Es importante porque asigna costos a actividades específicas, proporcionando una mayor precisión en el control de costos. Se implementa identificando actividades y asignando costos a cada una." },
                        new RadioButton { GroupName = "Q4", Content = "C) Es importante solo para proyectos grandes." },
                        new RadioButton { GroupName = "Q4", Content = "D) Se implementa aumentando el presupuesto total del proyecto." }
                    }
                }
            };
        }

        private void OnPreviousClicked(object sender, System.EventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayQuestion();
            }
        }

        private void OnNextClicked(object sender, System.EventArgs e)
        {
            if (currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;
                DisplayQuestion();
            }
            else
            {
                // Aquí puedes manejar la lógica para cuando el usuario presiona "Enviar".
                DisplayAlert("Examen completado", "Has completado el examen.", "OK");
            }
        }
    }
}