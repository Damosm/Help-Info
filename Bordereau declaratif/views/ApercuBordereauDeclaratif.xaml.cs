using System.Windows;

namespace ClassCrystalReportProduction.Dialogs
{
    /// <summary>
    /// Description for ApercuBordereauDeclaratif.
    /// </summary>
    public partial class ApercuBordereauDeclaratif : Window
    {

        System.Windows.Forms.Form OwnerForm;
        /// <summary>
        /// Initializes a new instance of the ApercuBordereauDeclaratif class.
        /// </summary>
        public ApercuBordereauDeclaratif()
        {           
            InitializeComponent();
        }

        public ApercuBordereauDeclaratif( System.Windows.Forms.Form Owner )
            : this()
        {
            this.OwnerForm = Owner;
        }
    }
}