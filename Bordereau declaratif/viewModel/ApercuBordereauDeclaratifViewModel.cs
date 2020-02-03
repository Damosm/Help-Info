using ClassCrystalReportProduction;
using ClassCrystalReportProduction.Dialogs;
using ClassGetMS;
using ClassGetMS.Models;
using ClassGetMS.Services;
using ClassGetMSUI.ViewModels;
using ClassGetMSUI.Views;
using ClassLibraryProget;
using ClassLibraryProget.Model;
using ClassUILibrary.ViewModels;
using ClassUILibrary.Views;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClassCrystalReportProduction.ViewModels
{

    #region Class data
    public class Data
    {
        public string Matricule;
        public double TempBase;
        //public DateTime TempTravail;
        //public DateTime Abs;
        public double TempTravail;
        public double Abs;
        public DateTime Jour;
        public string Regime;

        public Data( string Matricule, double TempBase )
        {
            this.Matricule = Matricule;
            this.TempBase = TempBase;
        }

        public Data( string Matricule, double TempBase, string Regime )
        {
            this.Matricule = Matricule;
            this.TempBase = TempBase;
            this.Regime = Regime;
        }

        public Data( string Matricule, double TempTravail, double Abs, DateTime Jour )
        {
            this.Matricule = Matricule;
            this.TempTravail = TempTravail;
            this.Abs = Abs;
            this.Jour = Jour;
        }
    }
    #endregion



    /// <summary>C:\DEV\Get-MS\ClassCrystalReportProduction\ViewModels\ApercuBordereauDeclaratifViewModel.cs
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ApercuBordereauDeclaratifViewModel : ViewModelBase, IModalDialogViewModel
    {

        #region Variables // Constructeur

        public List<string> Agents;                      // Liste d'Agents
        public string entete;                            // Commentaires
        public PrevisionnelEnum previsionnel;            // Prévisionnel
        private DateTime dateSemaine;                    // Date de la semaine selectionnée
        public string stringPrevisionnel;                // 
        public int weekOfYear;                           // Formule calcul semaine de l'année             
        private int _progressMax;                        // Barre de progression : valeur pour 100 %
        private IProgress<ProgressMessage> _progress;
        public DateTime Mois;    
        private IDataService _dataService;
        private IDialogService _dialogService;
        private StrucParam ParamGlobaux;
        private SqlConnection oConnection;

        public bool? DialogResult
        {
            get
            {
                return true;
            }
        }


        //public ApercuBordereauDeclaratifViewModel() { }
        /// <summary>
        /// Initializes a new instance of the ApercuBordereauDeclaratifViewModel class.
        /// </summary>
        public ApercuBordereauDeclaratifViewModel( IDataService dataService, IDialogService dialogService )
        {

            _dataService = dataService;
            ParamGlobaux = _dataService.ParamGlobaux;

            this._dataService = dataService;
            this._dialogService = dialogService;
            LoadedCommand = new RelayCommand( async () => await Initialize() );
            UnLoadedCommand = new RelayCommand( () => Reset() );
            MailCommand = new RelayCommand( () => SendMail() );


        }
        #endregion

        #region debug Afficher
        /*
        /// <summary>
        /// Degug//////////////
        /// </summary>
        public void Afficher()
        {

            Debug.WriteLine( entete );
            Debug.WriteLine( dateSemaine );

            foreach( var item in Agents )
            {
                Debug.WriteLine( item );
            }
            Debug.WriteLine( "Prévisionnel : " + previsionnel.ToString() );

        }*/
        #endregion

        #region Properties

        private bool _Close;
        public bool Close
        {
            get
            {
                return _Close;
            }
            set
            {
                if( value != _Close )
                {
                    _Close = value;
                    RaisePropertyChanged( "Close" );
                }
            }
        }
        private ReportClass _ReportSource;
        public ReportClass ReportSource
        {
            get
            {
                return _ReportSource;
            }
            set
            {
                if( value != _ReportSource )
                {
                    _ReportSource = value;
                    RaisePropertyChanged( "ReportSource" );
                }
            }
        }

        private ParameterFields _ParameterFieldInfo;
        public ParameterFields ParameterFieldInfo
        {
            get
            {
                return _ParameterFieldInfo;
            }
            set
            {
                if( value != _ParameterFieldInfo )
                {
                    _ParameterFieldInfo = value;
                    RaisePropertyChanged( "ParameterFieldInfo" );
                }
            }
        }
        #endregion

        #region Commands

        private RelayCommand _LoadedCommand;
        public RelayCommand LoadedCommand
        {
            get
            {
                return _LoadedCommand;
            }
            set
            {
                if( value != _LoadedCommand )
                {
                    _LoadedCommand = value;
                    RaisePropertyChanged( "LoadedCommand" );
                }
            }
        }

        private RelayCommand _UnLoadedCommand;
        public RelayCommand UnLoadedCommand
        {
            get
            {
                return _UnLoadedCommand;
            }
            set
            {
                if( value != _UnLoadedCommand )
                {
                    _UnLoadedCommand = value;
                    RaisePropertyChanged( "UnLoadedCommand" );
                }
            }
        }

        private RelayCommand _MailCommand;
        public RelayCommand MailCommand
        {
            get
            {
                return _MailCommand;
            }
            set
            {
                if( value != _MailCommand )
                {
                    _MailCommand = value;
                    RaisePropertyChanged( "MailCommand" );
                }
            }
        }

        #endregion

        #region Initialisation
        private async Task Initialize()
        {
            LoadingViewModel lvm = new LoadingViewModel();

            _progressMax = 0;
            _progress = new Progress<ProgressMessage>( ( msg ) => { Messenger.Default.Send<ProgressMessage>( msg ); } );
            
            Task apercuTask = CreationApercu( _progress );

            _dialogService.ShowDialog<LoadingDialog>( this, lvm );

            await apercuTask;
        }

        private void Reset()
        {
             Agents = null;
            _ReportSource = null;
        }
        #endregion

        #region Creation Apercu
        private async Task CreationApercu( IProgress<ProgressMessage> progress )
        {
            dateSemaine = (DateTime)_dataService.Jour;
            DataRow DtRow;
            string StSQL;
            StSQL = "";
            DataSet DsTemp = new DataSet();
            bool ajoutVirgule = false;

            _ReportSource = new CRBordereauDeclaratif();

            DsBordereauDeclaratif DsBordereauDeclaratif = new DsBordereauDeclaratif();

            

            ///////////////////////////////////////////////////////////////////////////////////
            /////////////Table Agents///////////////////////////////////////////

            //////Création de la requete/////////////////
            StSQL = "SELECT * FROM Agents WHERE Agents.Matricule IN (";
            ajoutVirgule = false;


            foreach( string s in Agents )
            {
                if( ajoutVirgule == true ) StSQL += ",";
                StSQL += "'" + s + "'";
                ajoutVirgule = true;
            }
            StSQL += ")";

          
            oConnection = ClassLibraryProget.DataBase.OpenSqlServer( ParamGlobaux.ConnectionString );


            ////////remplissage du DsTemp///////////////
            try
            {
                DsTemp = ClassLibraryProget.DataBase.SELECTSqlServer( oConnection, "Agents", StSQL );
            }
            catch( SqlException exc )
            {
                System.Windows.MessageBox.Show( "Lecture impossible des données de la table " + "Agents" + " " + exc.Message, "Erreur" );

            }
                      

            ////////remplissage du DsBordereauDeclaratif///////////////

            for( int i = 0; i < DsTemp.Tables[0].Rows.Count; i++ )
            {
                DtRow = DsBordereauDeclaratif.Tables["Agents"].NewRow();
                for( int j = 0; j < DsTemp.Tables[0].Columns.Count; j++ )
                    DtRow[j] = DsTemp.Tables[0].Rows[i][j];
                DsBordereauDeclaratif.Tables["Agents"].Rows.Add( DtRow );

                
            }


            ///////////////////////////////////////////////////////////////////////////////////
            /////////////Table Planning///////////////////////////////////////////

           

            /////Calcul du numéro de la semaine///////////
            CultureInfo ci = CultureInfo.CurrentCulture;
            weekOfYear = ci.Calendar.GetWeekOfYear( dateSemaine, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday );

            //////Création de la requete/////////////////
            StSQL = "SELECT * FROM Planning WHERE Planning.Matricule IN (";
            ajoutVirgule = false;


            foreach( string s in Agents )
            {
                if( ajoutVirgule == true ) StSQL += ",";
                StSQL += "'" + s + "'";
                ajoutVirgule = true;
            }
            StSQL += ") AND Planning.[Num semaine] = " + weekOfYear + " AND YEAR(Jour) = " + dateSemaine.Year;



            ////////remplissage du DsTemp///////////////
            try
            {
                DsTemp = ClassLibraryProget.DataBase.SELECTSqlServer( oConnection, "Planning", StSQL );
            }
            catch( SqlException exc )
            {                
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                    "Lecture impossible des données de la table " + "Planning" + " " + exc.Message,
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error );

            }
            ///////////////////////si pas resultat///////////////////////////////////////////
            if ( DsTemp.Tables[0].Rows.Count == 0 )
            {                 
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                    "Aucun planning correspondant n'a été trouvé",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error );

                _progress.Report( new ProgressMessage( 100, "Finished", true ));

                Reset();
                await Task.Delay( 500 );
                Close = true;
                return;

                
            }


            ////////////////////////Initialisation barre de progression//////////////////////////
            _progressMax = ( Agents.Count * 1 );           
            int _progressValue = 0;


            ////////remplissage du DsBordereauDeclaratif///////////////

            for( int i = 0; i < DsTemp.Tables[0].Rows.Count; i++ )
            {
                DtRow = DsBordereauDeclaratif.Tables["Planning"].NewRow();
                for( int j = 0; j < DsTemp.Tables[0].Columns.Count; j++ )
                    DtRow[j] = DsTemp.Tables[0].Rows[i][j];
                DsBordereauDeclaratif.Tables["Planning"].Rows.Add( DtRow );

                

            }

            //////////////////Remplissage Annualisation/////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            List<Parametre> Params = new List<Parametre>();
            List<PeriodeModulation> Periodes = new List<PeriodeModulation>();
            List<Data> ListeData = new List<Data>();

            ///compteur de tour de boucle//
            int k = 0;

            foreach( var item in Agents )
            {
                
                //////recherche parametre pour la methode PeriodesModulation//////////////////////////////////
                Params = await Parametres.GetParametresAsync( ParamGlobaux.ConnectionString, ParamGlobaux.IDEtablissement, item,
                                                                         new List<string> { "Durée maxi CDD court" } );
                int DureeMaxCDDCourt = Convert.ToInt32( Params.Single( p => p.Name == "Durée maxi CDD court" ).Value );

                /////////////////////////////recherche des périodes de modulation/////////////////////////////////////////////             


                Periodes = await gestion.PeriodesModulation( ParamGlobaux.ConnectionString, item, dateSemaine, DureeMaxCDDCourt );



                ////////////////////////////Création liste Data Matricule + tempbase///////////////////////////////////////////////////

                foreach( var z in Periodes )
                {
                    if( z.EnCours == true )
                    {
                        ListeData.Add( new Data( item, z.TempsBase , z.Regime) );
                    }
                }

                _progressValue++;
                _progress.Report( new ProgressMessage( ( _progressValue * 100 ) / _progressMax, "Création de l'aperçu...", false ) );
            }
            
            ////////////////////////////////////Remplissage DsBordereauDeclaratif//////////////////////////////////////////////

            foreach( var x in ListeData )
            {
                k = 0;
               
                foreach( DataRow DtRowBD in DsBordereauDeclaratif.Tables["Planning"].Rows )                    
                {
                    
                    if( DsBordereauDeclaratif.Tables["Planning"].Rows[k]["Matricule"].ToString() == x.Matricule )
                    {
                        DsBordereauDeclaratif.Tables["Planning"].Rows[k]["Annualisation"] = x.TempBase;
                        DsBordereauDeclaratif.Tables["Planning"].Rows[k]["Regime"] = x.Regime;
                    }
                    k++;
                }

            }


            // =======================================================================================================================================
            // Définir une structure de données temps de travail pour les calculs
            // =======================================================================================================================================
            ClassGetMS.TpsDeTravail TempsTravail;
            List<Data> listeCalcul = new List<Data>();
            List<DateTime> Jours = new List<DateTime>();

            for( int i = 0; i < 7; i++ )
            {
                Jours.Add( dateSemaine );

                dateSemaine = dateSemaine.AddDays( 1 );
            }
           

           

            DateTime dateValue = new DateTime( 1900, 1, 1, 0, 0, 0 );
            DateTime dateValue1 = new DateTime( 1900, 1, 1, 0, 0, 0 );
                        
            foreach( var item in Agents )
            {
                foreach( var d in Jours )
                {
                    TempsTravail = await ClassGetMS.TempsTravail.CalculTpsTravail( ParamGlobaux, item, d, d, ParamGlobaux.IDEtablissement, "", "" );
                    listeCalcul.Add( new Data( item,  (( TempsTravail.TpsTravailEffectif + TempsTravail.TpsTravailAssimilé + TempsTravail.TpsRéparti ) / (double)60.00m ), TempsTravail.HeuresAbsencesRémunérées , d  ));
                  
                }
               
            }

           

            foreach( DataRow DtRowBD in DsBordereauDeclaratif.Tables["Planning"].Rows )
            {
                Data jour = listeCalcul.SingleOrDefault( l => l.Matricule == DtRowBD["Matricule"].ToString() && l.Jour == Convert.ToDateTime( DtRowBD["Jour"] ) );
                if( jour != null )
                {
                    DtRowBD["Temp de travail"] = jour.TempTravail;
                    DtRowBD["Abs"] = jour.Abs;
                }

            }

            //////////Prévisionnel en string////////////


            if( previsionnel.ToString() == "Oui" )
            {
                stringPrevisionnel = "Prévisionnel";
            }
            else
            {
                stringPrevisionnel = "";
            }

            if( entete == null )
            {
                entete = "";
            }


            _ReportSource.SetDataSource( DsBordereauDeclaratif );

            ///////////Nom Etablissement/////////////////////////////
            string NomEtablissement;

            NomEtablissement = ParamGlobaux.Etablissement.ToString();

            TextObject Commentaire;
            TextObject Previsionnel;
            TextObject Etablissement;
            if( ( _ReportSource.ReportDefinition.ReportObjects["Text3"] != null ) &&
                ( _ReportSource.ReportDefinition.ReportObjects["Text4"] != null ) &&
                ( _ReportSource.ReportDefinition.ReportObjects["Text6"] != null ) )
            {
                Commentaire = (TextObject)_ReportSource.ReportDefinition.ReportObjects["Text3"];
                Commentaire.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase( entete.ToString() );
                Previsionnel = (TextObject)_ReportSource.ReportDefinition.ReportObjects["Text4"];
                Previsionnel.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase( stringPrevisionnel.ToString() );
                Etablissement = (TextObject)_ReportSource.ReportDefinition.ReportObjects["Text6"];
                Etablissement.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase( NomEtablissement.ToString() );
            }

            RaisePropertyChanged( "ReportSource" );

        }
        #endregion


        #region Mail
        /// <summary>
        /// Bouton pour l'envoie du doc par email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMail()
        {
            MailConfigViewModel MailConfigVM = new MailConfigViewModel( _dataService );

            MailConfigVM.Subject = "Bordereau déclaratif du " + Mois.ToShortDateString() + " au " + Mois.AddDays( DateTime.DaysInMonth( Mois.Year, Mois.Month ) - 1 ).ToShortDateString();

            MailConfigVM.MessageText = "Bonjour,\n" +
                "\n" +
                "Veuillez trouver votre Bordereau déclaratif en pièce jointe." +
                "\n" +
                "Cordialement,\n" +
                "\n" +
                "La Direction\n";
            MailConfigVM.AttachmentStream = _ReportSource.ExportToStream( ExportFormatType.PortableDocFormat );
            MailConfigVM.AttachmentName = "BordereauDeclaratif.pdf";
            _dialogService.ShowDialog<MailConfigView>( this, MailConfigVM );
        }
        #endregion
    }
}
