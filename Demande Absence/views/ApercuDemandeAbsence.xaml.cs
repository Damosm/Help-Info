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

namespace ClassCrystalReportProduction.Dialogs
{    
    /// <summary>
    /// Logique d'interaction pour ApercuDemandeAbsence.xaml
    /// </summary>
    public partial class ApercuDemandeAbsence : Window
    {
        System.Windows.Forms.Form OwnerForm;

        public ApercuDemandeAbsence()
        {
            InitializeComponent();
        }

        public ApercuDemandeAbsence(System.Windows.Forms.Form Owner)
            : this()
        {
            this.OwnerForm = Owner;
        }
    }
}
