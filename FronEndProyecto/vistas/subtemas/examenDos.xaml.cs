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
    public partial class examenDos : ContentPage
    {
        public examenDos()
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
            if (q1 == "B) El análisis de costos permite identificar, estimar y controlar los costos asociados, asegurando que el proyecto se mantenga dentro del presupuesto.") score++;
            if (q2 == "A) Comparando el valor del trabajo realizado con el costo real y el presupuesto planificado para medir el progreso y el desempeño.") score++;
            if (q3 == "B) El control de costos implica monitorear y gestionar los gastos del proyecto para asegurarse de que no excedan el presupuesto asignado.") score++;
            if (q4 == "B) Un análisis de brechas identifica las diferencias entre los costos planificados y reales, ayudando a ajustar el presupuesto y mejorar la precisión de futuras estimaciones.") score++;

            // Display result
            DisplayAlert("Resultado", $"Tu puntuación es {score}/4", "OK");
        }
    }
}