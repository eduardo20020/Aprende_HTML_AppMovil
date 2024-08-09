using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class grafica : ContentPage
	{
		public grafica ()
		{
			InitializeComponent ();
            InitializeComponent();

            // Crear un nuevo modelo de gráfico
            var plotModel = new PlotModel { Title = "Ejemplo de Gráfico" };

            // Configurar el eje X
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Eje X",
                Minimum = 0,
                Maximum = 10
            });

            // Configurar el eje Y
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Eje Y",
                Minimum = 0,
                Maximum = 10
            });

            // Crear una serie de datos
            var lineSeries = new LineSeries
            {
                Title = "Serie 1",
                ItemsSource = new List<DataPoint>
            {
                new DataPoint(0, 0),
                new DataPoint(1, 2),
                new DataPoint(2, 4),
                new DataPoint(3, 8),
                new DataPoint(4, 16),
                new DataPoint(5, 32)
            }
            };

            // Agregar la serie al modelo
            plotModel.Series.Add(lineSeries);

            // Asignar el modelo a la vista
            plotView.Model = plotModel;
        }
	}
}