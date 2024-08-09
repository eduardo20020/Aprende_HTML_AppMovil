using FronEndProyecto.vistas.config;
using FronEndProyecto.vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboarOne : ContentPage
    {
        public DashboarOne()
        {
            InitializeComponent();

            registrarButton.Clicked += OnRegistrarButtonClicked;
            dashboardButton.Clicked += OnDashboarButtonClicked;
            sectionsButton.Clicked += OnSectionsButtonClicked;
        }
        private async void OnRegistrarButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new registrar()); // Asegúrate de que registrar exista
        }

        private async void OnDashboarButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboarOne()); // Esto recarga la página actual
        }

        private async void OnSectionsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SectionsPage()); // Asegúrate de que SectionsPage exista
        }
    }
}