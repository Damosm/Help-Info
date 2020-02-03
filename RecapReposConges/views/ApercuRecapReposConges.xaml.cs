using System.Windows;


namespace ClassCrystalReportProduction.Dialogs
{
    /// <summary>
    /// Logique d'interaction pour ApercuRecapReposConges.xaml
    /// </summary>
    public partial class ApercuRecapReposConges : Window
    {
        System.Windows.Forms.Form OwnerForm;

        public ApercuRecapReposConges()
        {
            InitializeComponent();
        }

        public ApercuRecapReposConges( System.Windows.Forms.Form Owner )
           : this()
        {
            this.OwnerForm = Owner;
        }
    }
}
