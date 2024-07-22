using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.config
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class inicioConfig : ContentPage
    {
        public inicioConfig()
        {
            InitializeComponent();
        }

        private void Iniciar(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new registrar());
        }
    }
}