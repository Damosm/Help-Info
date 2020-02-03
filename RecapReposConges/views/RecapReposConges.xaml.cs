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
    /// Logique d'interaction pour RecapReposConges.xaml
    /// </summary>
    public partial class RecapReposConges : Window
    {
        public ClassGetMS.StrucParam ParamGlobaux = new ClassGetMS.StrucParam();

        System.Windows.Forms.Form OwnerForm;

        public RecapReposConges()
        {
            InitializeComponent();
        }
        public RecapReposConges( System.Windows.Forms.Form Owner ) : this()
        {
            this.OwnerForm = Owner;
        }

    }
}
