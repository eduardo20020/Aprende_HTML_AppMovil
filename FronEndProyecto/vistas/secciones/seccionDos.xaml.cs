using FronEndProyecto.vistas.subtemas;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.secciones
{
    public partial class SeccionDos : ContentPage
    {
        public SeccionDos()
        {
            InitializeComponent();
            ChargeLecciones();

        }


        private void ChargeLecciones()
        {
            var lecciones = new List<Lecci>
            {
                new Lecci { NomLess = "Lección 1: Definición de Presupuestación de Proyectos", Pag = typeof(Page1) },
                new Lecci { NomLess = "Lección 2: Importancia del Presupuesto", Pag = typeof(Page2) },
                new Lecci { NomLess = "Lección 3: Componentes del Presupuesto", Pag = typeof(Page3)},
                new Lecci { NomLess = "Lección 4: Tipos de Presupuesto", Pag = typeof(Page4)},
                new Lecci { NomLess = "Examen:  Evaluacion", Pag = typeof(examenDos)},
                // Agrega más lecciones aquí
            };

            // Configurar el DataTemplate directamente en el código detrás
            LessView.ItemTemplate = new DataTemplate(() =>
            {
                var textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, "NomLess");
                return textCell;
            });

            LessView.ItemsSource = lecciones;
        }

        private async void LeccionTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Lecci leccion)
            {
                // Navegar a la página de la lección
                await Navigation.PushAsync((Page)Activator.CreateInstance(leccion.Pag));
            }
        }
    }

    public class Lecci
    {
        public string NomLess { get; set; }
        public Type Pag { get; set; }
    }
}
