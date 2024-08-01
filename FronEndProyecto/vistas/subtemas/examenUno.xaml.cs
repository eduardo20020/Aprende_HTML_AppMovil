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
    public partial class examenUno : ContentPage
    {
        public examenUno()
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
            if (q1 == "B) Los costos fijos permanecen constantes independientemente del nivel de producción, mientras que los costos variables cambian con el nivel de producción. Ejemplo de costos fijos: alquiler; ejemplo de costos variables: materiales.") score++;
            if (q2 == "A) Los costos directos se asignan directamente a un proyecto específico, mientras que los costos indirectos se reparten entre varios proyectos. Ejemplo de costo directo: materiales; ejemplo de costo indirecto: seguridad.") score++;
            if (q3 == "B) Un presupuesto incremental aumenta los gastos del año anterior en un porcentaje específico, mientras que un presupuesto de base cero justifica cada gasto desde cero.") score++;
            if (q4 == "B) El análisis de costos y beneficios ayuda a determinar si los beneficios superan los costos, lo que facilita la toma de decisiones informadas.") score++;

            // Display result
            DisplayAlert("Resultado", $"Tu puntuación es {score}/4", "OK");
        }
    }
}
