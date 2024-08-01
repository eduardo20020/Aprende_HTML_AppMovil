using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.subtemas
{
    public partial class examenDos : ContentPage
    {
        private int currentQuestionIndex = 0;
        private List<ContentView> questions = new List<ContentView>();

        public examenDos()
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
                        new Label { Text = "1. ¿Cuál es la importancia del análisis de costos en la planificación y ejecución de un proyecto?" },
                        new RadioButton { GroupName = "Q1", Content = "A) El análisis de costos no tiene importancia en la planificación y ejecución de un proyecto." },
                        new RadioButton { GroupName = "Q1", Content = "B) El análisis de costos permite identificar, estimar y controlar los costos asociados, asegurando que el proyecto se mantenga dentro del presupuesto." },
                        new RadioButton { GroupName = "Q1", Content = "C) El análisis de costos solo es importante para proyectos a corto plazo." },
                        new RadioButton { GroupName = "Q1", Content = "D) El análisis de costos se realiza solo una vez al inicio del proyecto y luego se ignora." }
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
                        new Label { Text = "2. ¿Cómo se puede utilizar el método del valor ganado para evaluar el desempeño financiero de un proyecto?" },
                        new RadioButton { GroupName = "Q2", Content = "A) Comparando el valor del trabajo realizado con el costo real y el presupuesto planificado para medir el progreso y el desempeño." },
                        new RadioButton { GroupName = "Q2", Content = "B) El método del valor ganado no se puede utilizar para evaluar el desempeño financiero de un proyecto." },
                        new RadioButton { GroupName = "Q2", Content = "C) Aplicando un porcentaje fijo de costos indirectos a los costos directos." },
                        new RadioButton { GroupName = "Q2", Content = "D) Utilizando solo el costo planificado sin considerar el trabajo realizado." }
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
                        new Label { Text = "3. Explique el concepto de control de costos y cómo se puede aplicar en un proyecto de desarrollo de software." },
                        new RadioButton { GroupName = "Q3", Content = "A) El control de costos no es aplicable en proyectos de desarrollo de software." },
                        new RadioButton { GroupName = "Q3", Content = "B) El control de costos implica monitorear y gestionar los gastos del proyecto para asegurarse de que no excedan el presupuesto asignado." },
                        new RadioButton { GroupName = "Q3", Content = "C) El control de costos significa reducir el presupuesto total del proyecto a la mitad." },
                        new RadioButton { GroupName = "Q3", Content = "D) El control de costos se realiza solo al final del proyecto." }
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
                        new Label { Text = "4. ¿Qué es un análisis de brechas y cómo puede ayudar a ajustar el presupuesto de un proyecto?" },
                        new RadioButton { GroupName = "Q4", Content = "A) Un análisis de brechas es una herramienta para aumentar los costos de un proyecto." },
                        new RadioButton { GroupName = "Q4", Content = "B) Un análisis de brechas identifica las diferencias entre los costos planificados y reales, ayudando a ajustar el presupuesto y mejorar la precisión de futuras estimaciones." },
                        new RadioButton { GroupName = "Q4", Content = "C) Un análisis de brechas solo se utiliza después de que el proyecto ha finalizado." },
                        new RadioButton { GroupName = "Q4", Content = "D) Un análisis de brechas es irrelevante para la presupuestación de proyectos." }
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