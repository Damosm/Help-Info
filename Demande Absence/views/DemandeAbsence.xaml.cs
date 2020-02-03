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
    /// Logique d'interaction pour DemandeAbsence.xaml
    /// </summary>
    public partial class DemandeAbsence : Window
    {
        public ClassGetMS.StrucParam ParamGlobaux = new ClassGetMS.StrucParam();

        System.Windows.Forms.Form OwnerForm;

        public DemandeAbsence(System.Windows.Forms.Form Owner) : this()        
        {
            this.OwnerForm = Owner;
        }

        public DemandeAbsence()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {

        }
    }
}
