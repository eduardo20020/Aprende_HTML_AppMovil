using FronEndProyecto.vistas.config;
using FronEndProyecto.vistas.subtemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas
{
    public partial class SectionsPage : ContentPage
    {
        public SectionsPage()
        {
            InitializeComponent();
            registrarButton.Clicked += OnRegistrarButtonClicked;
            dashboardButton.Clicked += OnDashboardButtonClicked;
            sectionsButton.Clicked += OnSectionsButtonClicked;
        }


        private async void seccion1(object sender, EventArgs e)
        {
            DisplayAlert($"Seccion 1", $"Descripción de la sección 1", "Entrar", "Cancelar");
            await Navigation.PushAsync(new examenUno());

        }
        private async void seccion2(object sender, EventArgs e)
        {
            DisplayAlert($"Seccin 2", $"Descripción de la sección 2", "Entrar", "Cancelar");
            await Navigation.PushAsync(new examenDos());

        }
        private async void seccion3(object sender, EventArgs e)
        {
            DisplayAlert($"Seccin 3", $"Descripción de la sección 3", "Entrar", "Cancelar");
            await Navigation.PushAsync(new examenTres());

        }
        private async void seccion4(object sender, EventArgs e)
        {
            DisplayAlert($"Seccin 4", $"Descripción de la sección 4", "Entrar", "Cancelar");
            await Navigation.PushAsync(new examenCuatro());
        }
        private async void OnRegistrarButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new registrar()); // Asegúrate de que registrar exista
        }

        private async void OnDashboardButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new contenido()); // Esto recarga la página actual
        }

        private async void OnSectionsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SectionsPage()); // Asegúrate de que SectionsPage exista
        }

        private void cambiarCuenta(object sender, EventArgs e)
        {
            Navigation.PushAsync(new login());
        }
        private void irDashboard(object sender, EventArgs e)
        {
            Navigation.PushAsync(new contenido());
        }

    }
}