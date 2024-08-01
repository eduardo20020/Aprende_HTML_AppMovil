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
        public examenTres()
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
            if (q1 == "A) Un software de gestión financiera puede automatizar la entrada de datos, generar informes, y facilitar el seguimiento y análisis de los presupuestos.") score++;
            if (q2 == "A) Se aplica comparando el valor del trabajo realizado con el costo real y el presupuesto planificado. Ventajas: proporciona una visión precisa del progreso; desventajas: puede ser complejo de implementar.") score++;
            if (q3 == "A) Es el proceso de comparar los costos planificados con los reales para identificar desviaciones y ajustar el presupuesto en consecuencia.") score++;
            if (q4 == "B) Es importante porque asigna costos a actividades específicas, proporcionando una mayor precisión en el control de costos. Se implementa identificando actividades y asignando costos a cada una.") score++;

            // Display result
            DisplayAlert("Resultado", $"Tu puntuación es {score}/4", "OK");
        }
    }
}