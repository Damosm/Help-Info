using System.Windows;

namespace ClassCrystalReportProduction.Dialogs
{
    /// <summary>
    /// LC
    /// Logique d'interaction pour WPFLignePrises.xaml
    /// </summary>
    public partial class BordereauDeclaratif : Window
    {
        public ClassGetMS.StrucParam ParamGlobaux = new ClassGetMS.StrucParam();

        System.Windows.Forms.Form OwnerForm;

        public BordereauDeclaratif()
        {
            InitializeComponent();
        }
        
        public BordereauDeclaratif( System.Windows.Forms.Form Owner )
            : this()
        {
            this.OwnerForm = Owner;
        }

        private void ComboBox_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {

        }

        private void ComboBox_SelectionChanged_1( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {

        }

        private void ComboBox_SelectionChanged_2( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {

        }

        private void LVAgent_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {

        }
    }
}