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
using static ClassGetMS.TempsTravail;

namespace ClassCrystalReportProduction.ViewModels
{
    public class ApercuRecapReposCongesViewModel : ViewModelBase, IModalDialogViewModel
    {
        #region Variables // Constructeur

        private IDataService _dataService;
        private IDialogService _dialogService;
        private StrucParam ParamGlobaux;
        private SqlConnection oConnection;
        private int _progressMax;                        
        private IProgress<ProgressMessage> _progress;
        private IEnumerable<PeriodeModulation> listPeriodseModulation;
        public PeriodeModulation PeriodeModulation;
        public IEnumerable<AgentModel> ListAgents;
        private int DureeMaxCDDCourt;
        



        //public ApercuBordereauDeclaratifViewModel() { }
        /// <summary>
        /// Initializes a new instance of the ApercuBordereauDeclaratifViewModel class.
        /// </summary>
        public ApercuRecapReposCongesViewModel( IDataService dataService, IDialogService dialogService )
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

        #region RelayCommands

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

        #region proprietes

        public bool? DialogResult
        {
            get
            {
                return true;
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

        public string TypeJour { get; private set; }
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
             PeriodeModulation = null;
             ListAgents = null;
            _ReportSource = null;
        }
        #endregion

        #region Creation Apercu
        private async Task CreationApercu( IProgress<ProgressMessage> progress )
        {
            _ReportSource = new CRRecapReposConges();
            DataSet DsTemp = new DataSet();
            oConnection = ClassLibraryProget.DataBase.OpenSqlServer( _dataService.ParamGlobaux.ConnectionString );
            DsRecapReposConges DsRecapReposConges = new DsRecapReposConges();
            

            ////////////////////////Initialisation barre de progression//////////////////////////
            _progressMax = ( ListAgents.Count() * 5 );
            int _progressValue = 0;

            #region Remplissage du DataSet Agents
            //Remplissage du DataSet Agents

            foreach( AgentModel ag in ListAgents )
            {
                if(ag.Typecontrat != null )
                {
                    DsRecapReposConges.Tables["Agents"].Rows.Add( ag.Matricule, ag.Mail, ag.Nom, ag.Prenom, ag.IDService, ag.IDSection, ag.IDEmploi,
                                                              ag.IDFiliere, ag.Typecontrat, ag.DebutContrat, ag.FinContrat );

                }
                else
                {
                    DsRecapReposConges.Tables["Agents"].Rows.Add( ag.Matricule, ag.Mail, ag.Nom, ag.Prenom, ag.IDService, ag.IDSection, ag.IDEmploi,
                                                              ag.IDFiliere, "", ag.DebutContrat, ag.FinContrat );
                }
                
                               
                _progressValue++;
                _progress.Report( new ProgressMessage( ( _progressValue * 100 ) / _progressMax, "Création de l'aperçu...", false ) );                
            }
            #endregion

            #region Remplissage du DataSet PeriodeModulation
            //remplissage du DataSet PeriodeModulation

            PeriodeModulation pTemp = new PeriodeModulation();
            
            DureeMaxCDDCourt = Convert.ToInt32( Parametres.RechercherParametres( oConnection, ParamGlobaux.IDEtablissement, "", "Durée maxi CDD court" ) );

            foreach( AgentModel ag in ListAgents )
            {
                listPeriodseModulation = await gestion.PeriodesModulation( ParamGlobaux.ConnectionString, ag.Matricule, PeriodeModulation.Debut, DureeMaxCDDCourt );
                pTemp = listPeriodseModulation.Where( pm => pm.EnCours == true ).SingleOrDefault();

                DsRecapReposConges.Tables["PeriodeModulation"].Rows.Add( ag.Matricule, pTemp.Debut, pTemp.Fin, pTemp.TypeContrat, pTemp.IdContrat, 
                                                                         pTemp.TempsBase, pTemp.Regime, pTemp.IDEtablissement, pTemp.EnCours );

                _progressValue++;
                _progress.Report( new ProgressMessage( ( _progressValue * 100 ) / _progressMax, "Création de l'aperçu...", false ) );
            }
            #endregion

            #region Remplissage DataSet CongesDroits

            List<Parametre> Params = new List<Parametre>();
            
            List<TypesJours> typesJours = new List<TypesJours>();
            var taskTypeJour = _dataService.GetTypesJoursAsync();           
            await  taskTypeJour;
            typesJours = taskTypeJour.Result.ToList();
            int DroitCPA;
            double DroitRCNuit;
            double DroitRCC;
            int DroitCESS;
            
            StrucRTT DroitRTT = new StrucRTT();

            foreach( AgentModel ag in ListAgents )
            {
                ParamGlobaux.StMatricule = ag.Matricule;
                Params = await Parametres.GetParametresAsync( _dataService.ParamGlobaux.ConnectionString, _dataService.ParamGlobaux.IDEtablissement, ag.Matricule,
                                                                new List<string> { "Congés en jours ouvrés",
                                                                                "Début periode de prise congés",
                                                                                "Durée maxi Contrat court CPA",
                                                                                "Durée carence CPA",
                                                                                "Jours Congés Ouvrables",
                                                                                "Jours Congés Ouvrés",
                                                                                "Durée pause avec Repas",
                                                                                "Durée pause sans Repas",
                                                                                "Valorisation CPA",
                                                                                "Valorisation CA",                                                                                
                                                                                "Base hebdomadaire",
                                                                                "Durée maxi CDD court",
                                                                                "Année Calcul N-1 RCN",
                                                                                "Convention collective",
                                                                                "Forfait RC Nuit",
                                                                                "Taux RC Nuit",
                                                                                "Heure début nuit",
                                                                                "Heure fin nuit",
                                                                                "Heure début nuit AT",
                                                                                "Heure fin nuit AT",
                                                                                "Forfait RCC",
                                                                                "Durée Repos Quotidien",
                                                                                "Ouverture droit coupure",
                                                                                "Année Calcul N-1 RCC"
                                                                } );

                DroitCPA = await TempsTravail.CalculDroitCPA( ParamGlobaux, PeriodeModulation.Debut, Params, true );

                DroitRCNuit = (await TempsTravail.CalculDroitRCNuitH( ParamGlobaux, Params, typesJours, PeriodeModulation.Debut, PeriodeModulation.Fin ))/60;             
                                
                DroitRCC = await TempsTravail.CalculDroitRCC( ParamGlobaux, typesJours, PeriodeModulation.Debut, PeriodeModulation.Fin );

                DroitCESS = await TempsTravail.CalculDroitCESS( ParamGlobaux, typesJours, PeriodeModulation.Debut );

                DroitRTT = await TempsTravail.CalculDroitRTT( ParamGlobaux, PeriodeModulation.Debut, PeriodeModulation.Fin );

                DsRecapReposConges.Tables["CongesDroits"].Rows.Add( ag.Matricule, DroitCPA, DroitRCNuit, DroitRCC, DroitCESS, DroitRTT.DbDroitRTT );

                _progressValue++;
                _progress.Report( new ProgressMessage( ( _progressValue * 100 ) / _progressMax, "Création de l'aperçu...", false ) );

            }





            #endregion

            #region Remplissage DataSet CongesPris
            
            await taskTypeJour;
            typesJours = taskTypeJour.Result.ToList();
            int PrisCPA;
            int PrisCA;
            int TotalCPACA;
            double PrisRCNuit;
            double PrisRCC;
            int PrisCESS;

            double PrisRTT;

            foreach( AgentModel ag in ListAgents )
            {
                ParamGlobaux.StMatricule = ag.Matricule;
                Params = await Parametres.GetParametresAsync( _dataService.ParamGlobaux.ConnectionString, _dataService.ParamGlobaux.IDEtablissement, ag.Matricule,
                                                                new List<string> { "Congés en jours ouvrés",
                                                                                "Début periode de prise congés",
                                                                                "Durée maxi Contrat court CPA",
                                                                                "Durée carence CPA",
                                                                                "Jours Congés Ouvrables",
                                                                                "Jours Congés Ouvrés",
                                                                                "Durée pause avec Repas",
                                                                                "Durée pause sans Repas",
                                                                                "Valorisation CPA",
                                                                                "Valorisation CA",                                                                                
                                                                                "Base hebdomadaire",
                                                                                "Durée maxi CDD court",
                                                                                "Année Calcul N-1 RCN",
                                                                                "Convention collective",
                                                                                "Forfait RC Nuit",
                                                                                "Taux RC Nuit",
                                                                                "Heure début nuit",
                                                                                "Heure fin nuit",
                                                                                "Heure début nuit AT",
                                                                                "Heure fin nuit AT",
                                                                                "Forfait RCC",
                                                                                "Durée Repos Quotidien",
                                                                                "Ouverture droit coupure",
                                                                                "Année Calcul N-1 RCC"
                                                                } );

                (PrisCPA, PrisCA) = await TempsTravail.CalculPrisCPA( _dataService.ParamGlobaux.ConnectionString, _dataService.ParamGlobaux.IDEtablissement, ag.Matricule, PeriodeModulation.Debut );
                TotalCPACA = PrisCPA + PrisCA;

                PrisRCNuit = await TempsTravail.CalculPrisRCNuit( ParamGlobaux, typesJours, Params,  PeriodeModulation.Debut, PeriodeModulation.Fin );

                PrisRCC = await TempsTravail.CalculGenreH( ParamGlobaux, typesJours, Params, "13", PeriodeModulation.Debut, PeriodeModulation.Fin );

                PrisCESS =  TempsTravail.CalculPrisCESS( ParamGlobaux, PeriodeModulation.Debut, PeriodeModulation.Fin );

                PrisRTT = TempsTravail.CalculGenreJ( _dataService.ParamGlobaux.ConnectionString, ag.Matricule, "30", PeriodeModulation.Debut, PeriodeModulation.Fin );

                DsRecapReposConges.Tables["CongesPris"].Rows.Add( ag.Matricule, TotalCPACA, PrisRCNuit, PrisRCC, PrisCESS, PrisRTT );

                _progressValue++;
                _progress.Report( new ProgressMessage( ( _progressValue * 100 ) / _progressMax, "Création de l'aperçu...", false ) );
            }

            #endregion



            #region Remplissage DataSet RcFeries

            ObservableCollection<RCFerie> RCFeries;
            IEnumerable<JourFerieW> ListRCFerie;
            DataSet DsRCFeriePris;
            DateTime d = new DateTime(1900, 01, 01);

            foreach( AgentModel ag in ListAgents )
            {
                ParamGlobaux.StMatricule = ag.Matricule;
                Params = await Parametres.GetParametresAsync( _dataService.ParamGlobaux.ConnectionString, _dataService.ParamGlobaux.IDEtablissement, ag.Matricule,
                                                                new List<string> { "Congés en jours ouvrés",
                                                                                "Début periode de prise congés",
                                                                                "Durée maxi Contrat court CPA",
                                                                                "Durée carence CPA",
                                                                                "Jours Congés Ouvrables",
                                                                                "Jours Congés Ouvrés",
                                                                                "Durée pause avec Repas",
                                                                                "Durée pause sans Repas",
                                                                                "Valorisation CPA",
                                                                                "Valorisation CA",                                                                                
                                                                                "Base hebdomadaire",
                                                                                "Durée maxi CDD court",
                                                                                "Année Calcul N-1 RCN",
                                                                                "Convention collective",
                                                                                "Forfait RC Nuit",
                                                                                "Taux RC Nuit",
                                                                                "Heure début nuit",
                                                                                "Heure fin nuit",
                                                                                "Heure début nuit AT",
                                                                                "Heure fin nuit AT",
                                                                                "Forfait RCC",
                                                                                "Durée Repos Quotidien",
                                                                                "Ouverture droit coupure",
                                                                                "Année Calcul N-1 RCC"

                                                                } );
                
                ListRCFerie = await TempsTravail.CalculFeriesAgent( ParamGlobaux, typesJours, PeriodeModulation.Debut, PeriodeModulation.Fin );
                

                RCFeries = await _dataService.GetHistoriqueRCFAsync( PeriodeModulation.Debut, PeriodeModulation.Fin);

                foreach( RCFerie rc in RCFeries )
                {
                    ListRCFerie = ListRCFerie.Where( a => a.Date != rc.JourFerie);

                }
                if ( ListRCFerie != null )
                {
                    foreach( var item in ListRCFerie )
                    {
                        RCFerie temp = new RCFerie( d, item.Date, item.NbHeuresW );
                        RCFeries.Add( temp );
                    }
                }

                ObservableCollection<RCFerie> RCFeries2 = new ObservableCollection<RCFerie> (RCFeries.OrderBy( a => a.JourFerie ));


                if( RCFeries2.Count() > 0 )
                {
                    foreach( RCFerie RCF in RCFeries2 )
                    {
                        if(RCF.JourFerie != null && RCF.RCF != d)
                        {
                            DsRecapReposConges.Tables["RcFeries"].Rows.Add( ag.Matricule, Convert.ToDateTime( RCF.JourFerie ).ToShortDateString(), RCF.NbHeures.ToString( @"hh\:mm" ),  RCF.RCF.ToShortDateString(), RCF.NbHeuresRCF.ToString( @"hh\:mm" ) );

                        }
                        else if( RCF.JourFerie == null && RCF.JourFerie != d )
                        {
                            DsRecapReposConges.Tables["RcFeries"].Rows.Add( ag.Matricule, "", RCF.NbHeures.ToString( @"hh\:mm" ), RCF.RCF.ToShortDateString(), RCF.NbHeuresRCF.ToString( @"hh\:mm" ) );

                        }
                        else
                        {
                            DsRecapReposConges.Tables["RcFeries"].Rows.Add( ag.Matricule, Convert.ToDateTime( RCF.JourFerie ).ToShortDateString(), RCF.NbHeures.ToString( @"hh\:mm" ), "", "" );

                        }
                    }
                }
                else
                {

                    DsRecapReposConges.Tables["RcFeries"].Rows.Add( ag.Matricule, "", "", "", "" );

                }
               
                _progressValue++;
                _progress.Report( new ProgressMessage( ( _progressValue * 100 ) / _progressMax, "Création de l'aperçu...", false ) );



            }

            #endregion
            
    
            _ReportSource.SetDataSource( DsRecapReposConges );

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

           // MailConfigVM.Subject = "Planning du " + Mois.ToShortDateString() + " au " + Mois.AddDays( DateTime.DaysInMonth( Mois.Year, Mois.Month ) - 1 ).ToShortDateString();

            MailConfigVM.MessageText = "Bonjour,\n" +
                "\n" +
                "Veuillez trouver votre planning en pièce jointe." +
                "\n" +
                "Cordialement,\n" +
                "\n" +
                "La Direction\n";
            MailConfigVM.AttachmentStream = _ReportSource.ExportToStream( ExportFormatType.PortableDocFormat );
            MailConfigVM.AttachmentName = "PlanningIndividuel.pdf";
            _dialogService.ShowDialog<MailConfigView>( this, MailConfigVM );
        }
        #endregion
    }
}
