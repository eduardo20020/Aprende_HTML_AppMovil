using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas
{
    public partial class SectionsPage : ContentPage
    {
        public SectionsPage()
        {
            InitializeComponent();
        }

        private async void OnSectionTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var label = frame?.Content as Label;
            int sectionNumber = -1;

            if (label != null)
            {
                // Determina el número de sección basado en el texto del label
                if (label.Text.Contains("Sección 1"))
                    sectionNumber = 1;
                else if (label.Text.Contains("Sección 2"))
                    sectionNumber = 2;
                else if (label.Text.Contains("Sección 3"))
                    sectionNumber = 3;
                else if (label.Text.Contains("Sección 4"))
                    sectionNumber = 4;
                else if (label.Text.Contains("Sección 5"))
                    sectionNumber = 5;
                else if (label.Text.Contains("Sección 6"))
                    sectionNumber = 6;
                else if (label.Text.Contains("Sección 7"))
                    sectionNumber = 7;
                else if (label.Text.Contains("Sección 8"))
                    sectionNumber = 8;
                else if (label.Text.Contains("Sección 9"))
                    sectionNumber = 9;
                else if (label.Text.Contains("Sección 10"))
                    sectionNumber = 10;

                // Muestra un DisplayAlert
                if (await DisplayAlert($"Sección {sectionNumber}", $"Descripción de la sección {sectionNumber}", "Entrar", "Cancelar"))
                {
                    // Navegar a la página de contenido de la sección
                    await Navigation.PushAsync(new SectionContentPage(sectionNumber));
                }
            }
        }

    }
}