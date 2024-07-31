using FronEndProyecto.vistas;
using FronEndProyecto.vistas.config;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Preferences.Get("correo", string.Empty) == string.Empty)
            {
                MainPage = new NavigationPage(new inicioConfig());
            }
            else
            {
                MainPage = new NavigationPage(new SectionsPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
