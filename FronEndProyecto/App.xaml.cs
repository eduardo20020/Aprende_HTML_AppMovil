using FronEndProyecto.vistas.config;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new inicioConfig());
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
