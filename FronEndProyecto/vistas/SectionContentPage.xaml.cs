using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FronEndProyecto.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SectionContentPage : ContentPage
    {
        public SectionContentPage(int sectionNumber)
        {
            InitializeComponent();

            SectionTitleLabel.Text = $"Título de la Sección {sectionNumber}";
            SectionDescriptionLabel.Text = $"Contenido detallado de la sección {sectionNumber}. Aquí puedes agregar texto o elementos adicionales según sea necesario.";

        }
    }
}