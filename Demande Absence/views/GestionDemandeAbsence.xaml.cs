using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GET_MS.Views
{
    /// <summary>
    /// Logique d'interaction pour GestionDemandeAbsence.xaml
    /// </summary>
    public partial class GestionDemandeAbsence : Window
    {

        public ClassGetMS.StrucParam ParamGlobaux = new ClassGetMS.StrucParam();

        System.Windows.Forms.Form OwnerForm;

        public GestionDemandeAbsence(System.Windows.Forms.Form Owner) : this()
        {
            this.OwnerForm = Owner;
        }
        public GestionDemandeAbsence()
        {
            InitializeComponent();
        }

        private void LVAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
