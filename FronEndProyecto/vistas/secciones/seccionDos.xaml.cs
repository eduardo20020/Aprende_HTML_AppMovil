using FronEndProyecto.vistas.subtemas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.secciones
{
    public partial class SeccionDos : ContentPage
    {
        public SeccionDos()
        {
            InitializeComponent();
        }

        private async void OnSection1Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Section1Page());
        }

        private async void OnSection2Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Section2Page());
        }

        private async void OnSection3Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Section3Page());
        }

        private async void OnSection4Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Section4Page());
        }

        private async void OnSection5Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Section5Page());
        }
    }
}