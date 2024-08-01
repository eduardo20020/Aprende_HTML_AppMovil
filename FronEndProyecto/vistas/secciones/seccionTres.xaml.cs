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
    public partial class seccionTres : ContentPage
    {
        public seccionTres()
        {
            InitializeComponent();
            CargarLeccion();
        }

        private void CargarLeccion()
        {
            var lecciones = new List<Leccionn>
            {
                new Leccionn { NombreLess = "Lección 1: Software de Gestión Financiera", Pagin = typeof(Page1)},
                new Leccionn { NombreLess = "Lección 2: Método del Valor Ganado", Pagin = typeof(Page2)},
                new Leccionn { NombreLess = "Lección 3: Análisis de Brechas", Pagin = typeof(Page3)},
                new Leccionn { NombreLess = "Lección 4: TPresupuestación Basada en Actividades", Pagin = typeof(Page4)},
                // Agrega más lecciones aquí
            };

            // Configurar el DataTemplate directamente en el código detrás
            LeccionesList.ItemTemplate = new DataTemplate(() =>
            {
                var textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, "NombreLess");
                return textCell;
            });

            LeccionesList.ItemsSource = lecciones;
        }

        private async void OnLessTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Leccionn leccion)
            {
                // Navegar a la página de la lección
                await Navigation.PushAsync((Page)Activator.CreateInstance(leccion.Pagin));
            }
        }
    }

    public class Leccionn
    {
        public string NombreLess { get; set; }
        public Type Pagin { get; set; }
    }
}
