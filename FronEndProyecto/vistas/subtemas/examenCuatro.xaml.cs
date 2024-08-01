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
        public examenCuatro()
        {
            InitializeComponent();
        }


        private void OnSubmitClicked(object sender, EventArgs e)
        {
            // Get all RadioButton groups and their selected values
            var q1 = ((StackLayout)this.Content).Children
                .OfType<RadioButton>().FirstOrDefault(r => r.GroupName == "Q1" && r.IsChecked)?.Content.ToString();
            var q2 = ((StackLayout)this.Content).Children
                .OfType<RadioButton>().FirstOrDefault(r => r.GroupName == "Q2" && r.IsChecked)?.Content.ToString();
            var q3 = ((StackLayout)this.Content).Children
                .OfType<RadioButton>().FirstOrDefault(r => r.GroupName == "Q3" && r.IsChecked)?.Content.ToString();
            var q4 = ((StackLayout)this.Content).Children
                .OfType<RadioButton>().FirstOrDefault(r => r.GroupName == "Q4" && r.IsChecked)?.Content.ToString();

            // Check answers
            int score = 0;
            if (q1 == "B) Definir el alcance del proyecto, estimar los costos, establecer un presupuesto y monitorear los gastos.") score++;
            if (q2 == "B) Una WBS descompone el proyecto en tareas más pequeñas, permitiendo una mejor estimación y control de costos.") score++;
            if (q3 == "A) Monitoreando regularmente los gastos, comparándolos con el presupuesto y ajustando según sea necesario. Ejemplo: utilizando software de gestión financiera para seguir los costos de un proyecto de construcción.") score++;
            if (q4 == "A) Métodos como el análisis de valor ganado, análisis de variaciones y análisis de brechas. Se aplican comparando los costos planificados con los reales y ajustando el presupuesto en consecuencia.") score++;

            // Display result
            DisplayAlert("Resultado", $"Tu puntuación es {score}/4", "OK");
        }
    }
}