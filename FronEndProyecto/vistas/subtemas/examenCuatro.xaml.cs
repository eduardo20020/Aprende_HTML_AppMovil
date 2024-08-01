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
    public partial class examenCuatro : ContentPage
    {
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
                        new Label { Text = "1. ¿Qué pasos deben seguirse para una correcta planificación de costos en un proyecto?", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                        new RadioButton { GroupName = "Q1", Content = "A) Ignorar los costos indirectos y enfocarse solo en los directos." },
                        new RadioButton { GroupName = "Q1", Content = "B) Definir el alcance del proyecto, estimar los costos, establecer un presupuesto y monitorear los gastos." },
                        new RadioButton { GroupName = "Q1", Content = "C) Establecer un presupuesto y olvidarse de los detalles." },
                        new RadioButton { GroupName = "Q1", Content = "D) Aumentar el presupuesto total para cubrir imprevistos." }
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
                        new Label { Text = "2. Explique cómo una estructura de desglose de trabajo (WBS) puede facilitar la gestión de costos en un proyecto de construcción.", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                        new RadioButton { GroupName = "Q2", Content = "A) Una WBS no tiene ninguna relación con la gestión de costos." },
                        new RadioButton { GroupName = "Q2", Content = "B) Una WBS descompone el proyecto en tareas más pequeñas, permitiendo una mejor estimación y control de costos." },
                        new RadioButton { GroupName = "Q2", Content = "C) Una WBS solo se utiliza en proyectos de software." },
                        new RadioButton { GroupName = "Q2", Content = "D) Una WBS aumenta la complejidad del proyecto sin beneficios claros." }
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
                        new Label { Text = "3. ¿Cómo se puede controlar eficazmente los costos de un proyecto en curso? Proporcione un ejemplo práctico.", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                        new RadioButton { GroupName = "Q3", Content = "A) Monitoreando regularmente los gastos, comparándolos con el presupuesto y ajustando según sea necesario. Ejemplo: utilizando software de gestión financiera para seguir los costos de un proyecto de construcción." },
                        new RadioButton { GroupName = "Q3", Content = "B) Dejando que los costos se controlen solos." },
                        new RadioButton { GroupName = "Q3", Content = "C) Estableciendo un presupuesto inicial y no haciendo seguimiento." },
                        new RadioButton { GroupName = "Q3", Content = "D) Aumentando el presupuesto total a mitad del proyecto." }
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
                        new Label { Text = "4. ¿Qué métodos se utilizan para analizar variaciones en el presupuesto y cómo se aplican en la práctica?", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                        new RadioButton { GroupName = "Q4", Content = "A) Métodos como el análisis de valor ganado, análisis de variaciones y análisis de brechas. Se aplican comparando los costos planificados con los reales y ajustando el presupuesto en consecuencia." },
                        new RadioButton { GroupName = "Q4", Content = "B) Métodos como la eliminación de actividades y la reducción de calidad." },
                        new RadioButton { GroupName = "Q4", Content = "C) No hay métodos específicos para analizar variaciones en el presupuesto." },
                        new RadioButton { GroupName = "Q4", Content = "D) Métodos como la duplicación del presupuesto inicial y la eliminación de todas las actividades no esenciales." }
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