using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas.secciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class seccionCinco : ContentPage
    {
        public seccionCinco()
        {
            InitializeComponent();
        }

        private void LeccionesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}