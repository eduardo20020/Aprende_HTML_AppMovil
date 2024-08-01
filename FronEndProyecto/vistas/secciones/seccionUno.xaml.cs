using FronEndProyecto.vistas.subtemas;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FronEndProyecto.vistas.secciones
{
    public partial class seccionUno : ContentPage
    {
        public seccionUno()
        {
            InitializeComponent();
            CargarLecciones();
        }

        private void CargarLecciones()
        {
            var lecciones = new List<Leccion>
            {
                new Leccion { NombreLeccion = "Lección 1: Definición de Presupuestación de Proyectos", Pagina = typeof(Leccion1Page) },
                new Leccion { NombreLeccion = "Lección 2: Importancia del Presupuesto", Pagina = typeof(Leccion2Page) },
                new Leccion { NombreLeccion = "Lección 3: Componentes del Presupuesto", Pagina = typeof(Leccion3Page)},
                new Leccion { NombreLeccion = "Lección 4: Tipos de Presupuesto", Pagina = typeof(Leccion4Page)},
                new Leccion { NombreLeccion = "Examen  1: Evaluacion Leccion Uno", Pagina = typeof(examenUno)},
                // Agrega más lecciones aquí
            };

            // Configurar el DataTemplate directamente en el código detrás
            LeccionesListView.ItemTemplate = new DataTemplate(() =>
            {
                var textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, "NombreLeccion");
                return textCell;
            });

            LeccionesListView.ItemsSource = lecciones;
        }

        private async void OnLeccionTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Leccion leccion)
            {
                // Navegar a la página de la lección
                await Navigation.PushAsync((Page)Activator.CreateInstance(leccion.Pagina));
            }
        }
    }

    public class Leccion
    {
        public string NombreLeccion { get; set; }
        public Type Pagina { get; set; }
    }
}
