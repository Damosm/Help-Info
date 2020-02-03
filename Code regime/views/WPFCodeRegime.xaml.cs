using ClassGetMS;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassGetMSReferences.Views
{
    /// <summary>
    /// Logique d'interaction pour WPFCodeRegime.xaml
    /// </summary>
    public partial class WPFCodeRegime : Window
    {
        public WPFCodeRegime()
        {
            InitializeComponent();
        }

        public StrucParam ParamGlobaux
        {
            get; set;
        }

        /// <summary>
        /// Pour garder une page au premier plan
        /// </summary>
        /// 
        System.Windows.Forms.Form OwnerForm;

        public WPFCodeRegime( System.Windows.Forms.Form Owner )
             : this()
        {
            this.OwnerForm = Owner;
        }

        private void BtnAjouter_Click( object sender, RoutedEventArgs e )
        {

        }
    }
}
