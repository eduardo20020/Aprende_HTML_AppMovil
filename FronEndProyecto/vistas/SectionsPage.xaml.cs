﻿using FronEndProyecto.vistas.secciones;
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


        }
        protected override void OnAppearing()
        {
            // Lógica para actualizar la vista
            lblProgreso.Text = $"Tu progreso es {Preferences.Get("progreso", string.Empty)}!";
            lblInicial.Text = $"Bienvenido {Preferences.Get("nombre", string.Empty)}!";
        }

        private async void seccion1(object sender, EventArgs e)
        {
            DisplayAlert($"Seccion 1", $"Descripción de la sección 1", "Entrar", "Cancelar");
            await Navigation.PushAsync(new seccionUno());

        }
        private async void seccion2(object sender, EventArgs e)
        {
            DisplayAlert($"Seccin 2", $"Descripción de la sección 2", "Entrar", "Cancelar");
            await Navigation.PushAsync(new SeccionDos());

        }
        private async void seccion3(object sender, EventArgs e)
        {
            DisplayAlert($"Seccin 3", $"Descripción de la sección 3", "Entrar", "Cancelar");
            await Navigation.PushAsync(new seccionTres());

        }
        private async void seccion4(object sender, EventArgs e)
        {
            DisplayAlert($"Seccin 4", $"Descripción de la sección 4", "Entrar", "Cancelar");
            await Navigation.PushAsync(new seccionCuatro());
        }

        private void cambiarCuenta(object sender, EventArgs e)
        {
            Navigation.PushAsync(new login());
        }
        private void irDashboard(object sender, EventArgs e)
        {
            Navigation.PushAsync(new dashboard2());
        }
    }
}