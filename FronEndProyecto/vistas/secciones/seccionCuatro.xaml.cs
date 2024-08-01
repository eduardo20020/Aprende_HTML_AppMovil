using FronEndProyecto.vistas.subtemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.secciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class seccionCuatro : ContentPage
    {
        public seccionCuatro()
        {
            InitializeComponent();
            CargarLeccion();
        }

        private void CargarLeccion()
        {
            var less = new List<Less>
            {
                new Less {  NameLess = "Lección 1: Planificación de Costos", Page = typeof(sub1)},
                new Less {  NameLess = "Leccion 2:Estructura de Desglose de Trabajo (WBS)", Page = typeof(sub2)},
                new Less {  NameLess = "Lección 3: Control de Costos", Page = typeof(sub3)},
                new Less {  NameLess = "Lección 4: Análisis de Variaciones", Page = typeof(sub4)},
                new Less {  NameLess = "Examen  4:  Evaluacion Leccion 4", Page = typeof(examenCuatro)},
                // Agrega más lecciones aquí
            };

            // Configurar el DataTemplate directamente en el código detrás
            LessList.ItemTemplate = new DataTemplate(() =>
            {
                var textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, "NameLess");
                return textCell;
            });

            LessList.ItemsSource = less;
        }

        private async void OnLeccTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Less leccion)
            {
                // Navegar a la página de la lección
                await Navigation.PushAsync((Page)Activator.CreateInstance(leccion.Page));
            }
        }
    }

    public class Less
    {
        public string NameLess { get; set; }
        public Type Page { get; set; }
    }
}
