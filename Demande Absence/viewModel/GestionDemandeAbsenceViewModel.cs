using ClassCrystalReportProduction.Dialogs;
using ClassCrystalReportProduction.ViewModels;
using ClassGetMS;
using ClassGetMS.Models;
using ClassGetMS.Services;
using ClassGetMSReferences.ViewModel;
using ClassGetMSUI.ViewModels;
using ClassGetMSUI.Views;
using CrystalDecisions.Shared;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmDialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;

namespace GET_MS.ViewModels
{

    #region Enum
    public enum DemandeStatus
    {
        Toutes, En_cours
    }
    #endregion

  

    public class GestionDemandeAbsenceViewModel : ViewModelBase
    {


        #region Propriétés

        private IDataService _dataService;
        private IDialogService _dialogService;
        private StrucParam ParamGlobaux;
        private SqlConnection oConnection;
        private bool _SelectAllAgents;
        private DataSet DsDroits;


        public bool SelectAllAgents
        {
            get
            {
                return _SelectAllAgents;
            }
            set
            {
                _SelectAllAgents = value;
                foreach( DemandeAbsenceNP ag in ListeDemande_Absence )
                    ag.IsSelected = value;

                RaisePropertyChanged( "SelectAllAgents" );


            }
        }


        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if( value != _IsSelected )
                {
                    _IsSelected = value;



                    RaisePropertyChanged( "IsSelected" );
                }
            }
        }

        private bool _Commentaire;
        public bool Commentaire
        {
            get
            {
                return _Commentaire;
            }
            set
            {
                if( value != _Commentaire )
                {
                    _Commentaire = value;
                    RaisePropertyChanged( nameof( Commentaire ) );
                }
            }
        }

        private bool _Commentaire2;
        public bool Commentaire2
        {
            get
            {
                return _Commentaire2;
            }
            set
            {
                if( value != _Commentaire2 )
                {
                    _Commentaire2 = value;
                    RaisePropertyChanged( nameof( Commentaire2 ) );
                }
            }
        }

        private string _Commentaire_agent;
        public string Commentaire_agent
        {
            get
            {
                return _Commentaire_agent;
            }
            set
            {

                if( value != _Commentaire_agent )
                {
                    _Commentaire_agent = value;
                    RaisePropertyChanged( nameof( Commentaire_agent ) );
                }
            }
        }

        private string _Commentaire_gestionnaire;
        public string Commentaire_gestionnaire
        {
            get
            {
                return _Commentaire_gestionnaire;
            }
            set
            {

                if( value != _Commentaire_gestionnaire )
                {
                    _Commentaire_gestionnaire = value;
                    RaisePropertyChanged( nameof( Commentaire_gestionnaire ) );
                }
            }
        }

        private DemandeStatus _Status;
        public DemandeStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if( value != _Status )
                {
                    _Status = value;

                    if( _Status == DemandeStatus.Toutes )
                    {
                        if( _SelectedAgent != null )
                        {
                            if( SelectedAgent.Nom == "Tous les Agents" )

                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP );
                            }
                            else
                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP.Where( dm => dm.Matricule == SelectedAgent.Matricule ) );

                            }

                        }
                        else
                        {
                            ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP );

                        }


                    }
                    else
                    {
                        if( _SelectedAgent != null )
                        {
                            if( SelectedAgent.Nom == "Tous les Agents" )

                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP.Where( dm => dm.Etat == "ENC" ) );
                            }
                            else
                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP.Where( dm => dm.Etat == "ENC" && dm.Matricule == SelectedAgent.Matricule ) );

                            }
                        }
                        else
                        {
                            ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP.Where( dm => dm.Etat == "ENC" ) );

                        }

                    }

                    RaisePropertyChanged( nameof( Status ) );

                }
            }
        }

        private List<AgentModel> _TousAgents;

        private ObservableCollection<AgentModel> _Agents;
        public ObservableCollection<AgentModel> Agents
        {
            get
            {
                return _Agents;
            }
            set
            {
                if( value != _Agents )
                {

                    _Agents = value;
                    RaisePropertyChanged( "Agents" );
                }
            }
        }

        private AgentModel _SelectedAgent;
        public AgentModel SelectedAgent
        {
            get
            {
                return _SelectedAgent;
            }
            set
            {
                if( value != _SelectedAgent )
                {
                    _SelectedAgent = value;
                    //_Status = DemandeStatus.Toutes;

                    if( SelectedAgent != null )
                    {
                        if( SelectedAgent.Nom == "Tous les Agents" )

                        {
                            if( Status == DemandeStatus.Toutes )
                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP );
                            }
                            else
                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP.Where( dm => dm.Etat == "ENC" ) );
                            }

                        }
                        else
                        {
                            Actualisation();

                            if( Status == DemandeStatus.Toutes )
                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP.Where
                                       ( dm => dm.Matricule == SelectedAgent.Matricule ) );
                            }
                            else
                            {
                                ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>( _TtesDemande_AbsenceNP.Where
                                       ( dm => dm.Matricule == SelectedAgent.Matricule && dm.Etat == "ENC" ) );
                            }

                        }
                    }
                    else
                    {
                        ListeDemande_Absence = null;
                    }



                    RaisePropertyChanged( nameof( SelectedAgent ) );


                }
            }
        }
        private List<DemandeAbsenceNP> _TtesDemande_AbsenceNP;

        private List<Demande_Absence> _TtesDemande_Absence;

        private List<DemandeAbsenceNP> _Demande_Absence_Commentaire;

        private ObservableCollection<DemandeAbsenceNP> _ListeDemande_Absence;
        public ObservableCollection<DemandeAbsenceNP> ListeDemande_Absence
        {
            get
            {


                return _ListeDemande_Absence;
            }
            set
            {
                if( value != _ListeDemande_Absence )
                {
                    _ListeDemande_Absence = value;

                    ExecuteShowCommentaire();

                    RaisePropertyChanged( nameof( ListeDemande_Absence ) );
                }
            }
        }
        private string _SelectedDemande_Absence;
        public string SelectedDemande_Absence
        {
            get
            {
                return _SelectedDemande_Absence;
            }
            set
            {
                if( value != _SelectedDemande_Absence )
                {
                    _SelectedDemande_Absence = value;
                    RaisePropertyChanged( nameof( SelectedDemande_Absence ) );


                }
            }
        }



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
        #endregion

        #region Constructeur
        public GestionDemandeAbsenceViewModel(IDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            ParamGlobaux = _dataService.ParamGlobaux;
            _dialogService = dialogService;

            AideCommand = new RelayCommand(() => ClassUILibrary.Design.AfficheAide( "AideGestionDemandeAbsence.pdf", oConnection));
            LoadedCommand = new RelayCommand(() => Initialize());
            UnLoadedCommand = new RelayCommand(() => ExecuteUnLoaded());
            QuitterCommand = new RelayCommand(() => Close = true);
            ImprimerCommand = new RelayCommand(() => ExecuteImprimer());
            AccepterCommand = new RelayCommand(async () => await ExecuteAccepter());
            RefuserCommand = new RelayCommand(async () => await ExecuteRefuser());
            SupprimerCommand = new RelayCommand( () => ExecuteSupprimer() );
            CongesCommand = new RelayCommand( () => ExecuteConges() );
            ShowCommentaireCommand = new RelayCommand(() => ExecuteShowCommentaire());


        }


        #endregion

        #region Relay Command       

        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnLoadedCommand { get; set; }
        public RelayCommand AideCommand { get; set; }
        public RelayCommand ImprimerCommand { get; private set; }
        public RelayCommand AccepterCommand { get; private set; }
        public RelayCommand RefuserCommand { get; private set; }
        public RelayCommand QuitterCommand { get; private set; }
        public RelayCommand SupprimerCommand { get; private set; }
        public RelayCommand CongesCommand { get; private set; }
        public RelayCommand ShowCommentaireCommand { get; private set; }

        #endregion


        #region Initialisation
        public async void Initialize()
        {
            //initialisation du commentaire
            Commentaire_agent = "";            
            Commentaire = false;
            Commentaire2 = false;
            Commentaire_gestionnaire = "";

            
            //Initialisation des listes///////////

            _SelectedAgent = new AgentModel();
            SelectedAgent = null;

            var taskAgents = _dataService.GetAgentsContratsAsync();            
            var taskDemandeAbsence = _dataService.GetDemandeAbsenceAsync();
            await Task.WhenAll(taskAgents, taskDemandeAbsence);
                       
            _TousAgents = taskAgents.Result.ToList();
            Agents = new ObservableCollection<AgentModel>(_TousAgents.Distinct().OrderBy(a => a.Prenom).OrderBy(a => a.Nom));

            ////////on ajoute au debut de la liste, une ligne "tous les agents"
            Agents.Insert(0, new AgentModel("-------","", "Tous les Agents"));

            _TtesDemande_Absence = taskDemandeAbsence.Result.ToList();
            _TtesDemande_AbsenceNP = new List<DemandeAbsenceNP>();

            //on remplit la liste avec les donnees de "_TtesDemande_Absence" et les donnees (nom, prenom) de "Agents"
            foreach (var d in _TtesDemande_Absence)
            {
                foreach (var a in Agents)
                {
                    if(d.Matricule == a.Matricule)
                    {
                        _TtesDemande_AbsenceNP.Add(new DemandeAbsenceNP(d.Matricule, a.Nom, a.Prenom, d.Date_debut, d.Date_fin, d.Type_Jour,
                        d.Commentaire_agent, d.Commentaire_gestionnaire, d.Etat));
                    }
                    
                }
                
            }

            ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>(_TtesDemande_AbsenceNP);

            //Selection des agents
            SelectAllAgents = false;

            
            //initialisation du statut            
            Status = DemandeStatus.En_cours;

            oConnection = new SqlConnection( _dataService.ParamGlobaux.ConnectionString );
        }
        #endregion

        #region Methodes
        private void ExecuteUnLoaded()
        {
            Status = DemandeStatus.Toutes;
            Close = false;
            SelectedAgent = null;
            ListeDemande_Absence = null;
            Commentaire_gestionnaire = null;

            _TtesDemande_AbsenceNP = null;
            SelectedDemande_Absence = null;


        }
        /// <summary>
        /// Mise à jour de la liste de demandes, lors d'un changement d'agent
        /// </summary>
        public async void Actualisation()
        {
            var taskDemandeAbsence = _dataService.GetDemandeAbsenceAsync();
            await taskDemandeAbsence;

            _TtesDemande_Absence = taskDemandeAbsence.Result.ToList();

            _TtesDemande_AbsenceNP = new List<DemandeAbsenceNP>();

            //on remplit la liste avec les donnees de "_TtesDemande_Absence" et les donnees (nom, prenom) de "Agents"
            foreach (var d in _TtesDemande_Absence)
            {
                foreach (var a in Agents)
                {
                    if (d.Matricule == a.Matricule)
                    {
                        _TtesDemande_AbsenceNP.Add(new DemandeAbsenceNP(d.Matricule, a.Nom, a.Prenom, d.Date_debut, d.Date_fin, d.Type_Jour,
                        d.Commentaire_agent, d.Commentaire_gestionnaire, d.Etat));
                    }

                }

            }
            if (_Status == DemandeStatus.Toutes)
            {
                if (_SelectedAgent != null)
                {
                    if (SelectedAgent.Nom == "Tous les Agents")

                    {
                        ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>(_TtesDemande_AbsenceNP);
                    }
                    else
                    {
                        ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>(_TtesDemande_AbsenceNP.Where(dm => dm.Matricule == SelectedAgent.Matricule));

                    }

                }
                else
                {
                    ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>(_TtesDemande_AbsenceNP);

                }


            }
            else
            {
                if (_SelectedAgent != null)
                {
                    if (SelectedAgent.Nom == "Tous les Agents")

                    {
                        ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>(_TtesDemande_AbsenceNP.Where(dm => dm.Etat == "ENC"));
                    }
                    else
                    {
                        ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>(_TtesDemande_AbsenceNP.Where(dm => dm.Etat == "ENC" && dm.Matricule == SelectedAgent.Matricule));

                    }
                }
                else
                {
                    ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>(_TtesDemande_AbsenceNP.Where(dm => dm.Etat == "ENC"));

                }

            }
        }

        private bool RecoupePeriode()
        {
            if( ListeDemande_Absence.Any( d => d.IsSelected && d.Etat == "REF" ) )
            {



                using( ProgetEntities pg = new ProgetEntities( Model.GenerateEFConnectionString( ParamGlobaux.ConnectionString ) ) )
                {
                    bool existe2;

                    foreach( DemandeAbsenceNP demandeAbsence in ListeDemande_Absence.Where( d => d.IsSelected ) )
                    {
                        existe2 = pg.Demande_Absence.Any( a => a.Matricule == demandeAbsence.Matricule && a.Etat != "REF" &&
                        ( ( demandeAbsence.Date_debut >= a.Date_debut && demandeAbsence.Date_debut <= a.Date_fin ) ||
                        ( demandeAbsence.Date_fin <= a.Date_fin && demandeAbsence.Date_fin >= a.Date_debut ) ||
                        ( demandeAbsence.Date_debut < a.Date_debut && demandeAbsence.Date_fin > a.Date_fin ) ) );

                        Debug.WriteLine( "existe2 : " + existe2 );

                        if( existe2 == false )
                        {
                            return false;
                        }
                        else
                        {
                            break;
                        }

                    }

                    return true;

                }
            }
            else
            {
                return false;
            }

        }

        private void ExecuteShowCommentaire()
        {
            // on initialise une liste, afin de compter le nombre d'agents selectionnés
            _Demande_Absence_Commentaire = new List<DemandeAbsenceNP>();



            if( ListeDemande_Absence != null )
            {
                foreach( var item in ListeDemande_Absence )
                {
                    if( item.IsSelected == true )
                    {
                        _Demande_Absence_Commentaire.Add( item );
                    }

                }


                //si il n'y a qu'une demande selectionnée, on affiche le textbox commentaire
                if( _Demande_Absence_Commentaire.Count == 1 )
                {
                    Commentaire = true;


                    foreach( var item in _Demande_Absence_Commentaire )
                    {
                        SelectedDemande_Absence = item.Commentaire_agent;
                        Commentaire_gestionnaire = item.Commentaire_gestionnaire;
                    }

                }
                else
                {
                    Commentaire = false;
                }


                //si il n'y a qu'une demande selectionnée et que son etat est ACC, on affiche le bouton Congés
                if( _Demande_Absence_Commentaire.Count == 1 && ListeDemande_Absence.Any( d => d.IsSelected && d.Etat == "ACC" ) )
                {
                    Commentaire2 = true;

                }
                else
                {
                    Commentaire2 = false;
                }
                
            }

        }
        #endregion

        
        #region Bt Imprimer
        private void ExecuteImprimer()
        {
            ApercuDemandeAbsenceViewModel ApercuDemandeAbsenceVM = new ApercuDemandeAbsenceViewModel(_dataService, _dialogService);

            //on renseigne une liste avec les agents selectionnés
            ObservableCollection<DemandeAbsenceNP> ListeDemande_Absence2 = new ObservableCollection<DemandeAbsenceNP>(ListeDemande_Absence.Where(a => a.IsSelected == true));
            

            if (ListeDemande_Absence2.Count != 0)
            {                
                ApercuDemandeAbsenceVM.ListeDemande_Absence = ListeDemande_Absence2;

                _dialogService.ShowDialog<ApercuDemandeAbsence>(this, ApercuDemandeAbsenceVM);
            }
            else
            {
                MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox(this,
                           "veuillez selectionner une demande d'absence",
                           "Erreur",
                           MessageBoxButton.OK,
                           MessageBoxImage.Exclamation );
            }
            
        }
        #endregion

        #region Bt Accepter
        private async Task ExecuteAccepter()
        {

            if( ListeDemande_Absence.Any( d => d.IsSelected ) )
            {
                if( ListeDemande_Absence.Where( d => d.IsSelected && d.Etat == "REF" ).Count() < 2 )
                {



                    if( RecoupePeriode() == false )
                    {

                        int NbLines = 0;

                        string StSQL = @"Update Demande_Absence Set [Etat]= 'ACC', [Commentaire gestionnaire]=@Commentaire_gestionnaire  Where Matricule=@Matricule AND [Date debut]=@Date_debut AND [Date fin]=@Date_fin";

                        if( Commentaire_gestionnaire == null )
                        {
                            Commentaire_gestionnaire = "";
                        }
                        Debug.WriteLine( StSQL );

                        string StSQLHisto = @"INSERT INTO JournalTransaction (DateEvt, Matricule, NumLigne, Ecran, description, Audit) VALUES (@Date, @Matricule, @Lignes, @Ecran, @Description, 1)";

                        using( SqlConnection _oConnection = new SqlConnection( ParamGlobaux.ConnectionString ) )
                        {


                            _oConnection.Open();
                            using( SqlTransaction _transaction = _oConnection.BeginTransaction() )
                            {


                                foreach( DemandeAbsenceNP demandeAbsence in ListeDemande_Absence.Where( d => d.IsSelected ) )
                                {
                                    try
                                    {

                                        SqlCommand command = new SqlCommand( StSQL, _oConnection, _transaction );
                                        command.Parameters.AddWithValue( "@Matricule", demandeAbsence.Matricule );
                                        command.Parameters.AddWithValue( "@Date_debut", demandeAbsence.Date_debut );
                                        command.Parameters.AddWithValue( "@Date_fin", demandeAbsence.Date_fin );
                                        command.Parameters.AddWithValue( "@Commentaire_gestionnaire", Commentaire_gestionnaire );

                                        Debug.WriteLine( demandeAbsence.Matricule );
                                        Debug.WriteLine( demandeAbsence.Date_debut );
                                        Debug.WriteLine( demandeAbsence.Date_fin );


                                        NbLines = await command.ExecuteNonQueryAsync();


                                        SqlCommand HistoCommand = new SqlCommand( StSQLHisto, _oConnection, _transaction );
                                        HistoCommand.Parameters.AddWithValue( "@Date", DateTime.Now );
                                        HistoCommand.Parameters.AddWithValue( "@Matricule", ParamGlobaux.Matricule );
                                        HistoCommand.Parameters.AddWithValue( "@Lignes", NbLines );
                                        HistoCommand.Parameters.AddWithValue( "@Ecran", "ExecuteAccepter()" );
                                        HistoCommand.Parameters.AddWithValue( "@Description", "Modification de l'etat en Accepter" );
                                        await HistoCommand.ExecuteNonQueryAsync();



                                    }
                                    catch( Exception e )
                                    {
                                        Debug.WriteLine( "ExecuteAccepter() : " + e.Message );
                                        break;
                                    }

                                }
                                try
                                {
                                    _transaction.Commit();
                                    MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox( this,
                                    "Demande(s) acceptée(s)",
                                    "valider",
                                    MessageBoxButton.OK );
                                }
                                catch( Exception ex )
                                {
                                    // This catch block will handle any errors that may have occurred 
                                    // on the server that would cause the rollback to fail, such as 
                                    // a closed connection.
                                    Debug.WriteLine( "Rollback Exception Type: {0} Message {1}", ex.GetType(), ex.Message );
                                    _transaction.Rollback();
                                }

                                Commentaire_gestionnaire = null;


                            }

                        }

                        Actualisation();
                    }
                    else
                    {
                        MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox( this,
                        "Cette demande contient des dates déjà utilisées dans une autre demande",
                        "Erreur",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation );
                    }

                }
                else
                {
                    MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox( this,
                    "Une seule demande refusée ne peut être modifiée à la fois",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
                }
            }
            else
            {
                MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox( this,
                    "Veuillez selectionner une demande",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
            }
            
        }
        #endregion

        #region Bt Supprimer
        private void ExecuteSupprimer()
        {
            if( ListeDemande_Absence.Any( d => d.IsSelected ) )
            {
                if( ListeDemande_Absence.Any( d => d.IsSelected && d.Etat == "REF" ) )
                {




                    if( ListeDemande_Absence.Where( d => d.IsSelected ).Count() < 2 )
                    {
                        //IsValid = true;

                        try
                        {

                            using( ProgetEntities pg = new ProgetEntities( Model.GenerateEFConnectionString( ParamGlobaux.ConnectionString ) ) )
                            {

                                foreach( DemandeAbsenceNP demandeAbsence in ListeDemande_Absence.Where( d => d.IsSelected ) )
                                {
                                    // recherche dans la BDD s'il existe déja une Demande identique
                                    Demande_Absence existe = pg.Demande_Absence.Where( a => a.Matricule == demandeAbsence.Matricule && a.Date_debut == demandeAbsence.Date_debut && a.Date_fin == demandeAbsence.Date_fin ).SingleOrDefault();


                                    //si la demande existe bien en base 
                                    if( existe != null )
                                    {


                                        pg.Demande_Absence.Remove( existe );

                                        // on sauvegarde les changements dans la BDD
                                        pg.SaveChanges();





                                        MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox( this,
                                        "Demande supprimée",
                                        "Validation",
                                        MessageBoxButton.OK );


                                        //on actualise la liste des demandes
                                        Actualisation();



                                    }
                                    else
                                    {
                                        MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox( this,
                                        "Cette demande est inexistante",
                                        "Erreur",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Exclamation );
                                    }
                                }




                            }
                        }
                        catch( DbEntityValidationException e )
                        {
                            foreach( var eve in e.EntityValidationErrors )
                            {
                                Debug.WriteLine( "DEBUG :Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State );
                                foreach( var ve in eve.ValidationErrors )
                                {
                                    Debug.WriteLine( " DEBUG:- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage );
                                }
                            }
                        }


                    }
                    else
                    {
                        MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox( this,
                        "Une seule demande refusée ne peut être supprimée à la fois",
                        "Erreur",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation );
                    }
                }
                else
                {
                    MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox( this,
                    "Seul les demandes refusées peuvent être supprimées",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
                }
            }

            else
            {
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                    "veuillez selectionner une demande d'absence",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
            }
        }
        #endregion       

        #region Bt Refuser
        private async Task ExecuteRefuser()
        {
            if( ListeDemande_Absence.Any( d => d.IsSelected ) )
            {


                int NbLines = 0;

                string StSQL = @"Update Demande_Absence Set [Etat]= 'REF', [Commentaire gestionnaire]=@Commentaire_gestionnaire  Where Matricule=@Matricule AND [Date debut]=@Date_debut AND [Date fin]=@Date_fin";

                if( Commentaire_gestionnaire == null )
                {
                    Commentaire_gestionnaire = "";
                }
                Debug.WriteLine( StSQL );

                string StSQLHisto = @"INSERT INTO JournalTransaction (DateEvt, Matricule, NumLigne, Ecran, description, Audit) VALUES (@Date, @Matricule, @Lignes, @Ecran, @Description, 1)";

                using( SqlConnection _oConnection = new SqlConnection( ParamGlobaux.ConnectionString ) )
                {
                    _oConnection.Open();
                    using( SqlTransaction _transaction = _oConnection.BeginTransaction() )
                    {
                        foreach( var item in ListeDemande_Absence )
                        {
                            if( item.IsSelected == true )
                            {
                                try
                                {

                                    SqlCommand command = new SqlCommand( StSQL, _oConnection, _transaction );
                                    command.Parameters.AddWithValue( "@Matricule", item.Matricule );
                                    command.Parameters.AddWithValue( "@Date_debut", item.Date_debut );
                                    command.Parameters.AddWithValue( "@Date_fin", item.Date_fin );
                                    command.Parameters.AddWithValue( "@Commentaire_gestionnaire", Commentaire_gestionnaire );

                                    Debug.WriteLine( item.Matricule );
                                    Debug.WriteLine( item.Date_debut );
                                    Debug.WriteLine( item.Date_fin );


                                    NbLines = await command.ExecuteNonQueryAsync();


                                    SqlCommand HistoCommand = new SqlCommand( StSQLHisto, _oConnection, _transaction );
                                    HistoCommand.Parameters.AddWithValue( "@Date", DateTime.Now );
                                    HistoCommand.Parameters.AddWithValue( "@Matricule", ParamGlobaux.Matricule );
                                    HistoCommand.Parameters.AddWithValue( "@Lignes", NbLines );
                                    HistoCommand.Parameters.AddWithValue( "@Ecran", "ExecuteRefuser()" );
                                    HistoCommand.Parameters.AddWithValue( "@Description", "Modification de l'etat en Refuser" );
                                    await HistoCommand.ExecuteNonQueryAsync();



                                }
                                catch( Exception e )
                                {
                                    Debug.WriteLine( "ExecuteRefuser() : " + e.Message );
                                    break;
                                }


                            }

                        }
                        try
                        {
                            _transaction.Commit();
                            MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox( this,
                            "Demande(s) refusée(s)",
                            "refuser",
                            MessageBoxButton.OK );
                        }
                        catch( Exception ex )
                        {
                            // This catch block will handle any errors that may have occurred 
                            // on the server that would cause the rollback to fail, such as 
                            // a closed connection.
                            Debug.WriteLine( "Rollback Exception Type: {0} Message {1}", ex.GetType(), ex.Message );
                            _transaction.Rollback();
                        }

                        Commentaire_gestionnaire = null;


                    }

                }

                Actualisation();

            }
            else
            {
                MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox( this,
                    "Veuillez selectionner une demande",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
            }
        }
        #endregion

        #region Bt Conges
        private void ExecuteConges()
        {
            if( ListeDemande_Absence.Any( d => d.IsSelected && d.Etat == "ACC" ) )
            {
                DemandeAbsenceNP demandeAbsenceNP = new DemandeAbsenceNP();
                demandeAbsenceNP = ListeDemande_Absence.Where( d => d.IsSelected ).SingleOrDefault();
                ParamGlobaux.StMatricule = demandeAbsenceNP.Matricule;
                Form Window = new Form();
                WPFCongés WPFMAJ = new WPFCongés( Window );
                WindowInteropHelper helper = new WindowInteropHelper( WPFMAJ );
                helper.Owner = Window.Handle;
                WPFMAJ.ParamGlobaux = this.ParamGlobaux;
                
                WPFMAJ.ShowDialog();
                AfficheTuile();
            }
            else
            {
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                           "Uniquement pour les demandes acceptées",
                           "Erreur",
                           MessageBoxButton.OK,
                           MessageBoxImage.Exclamation );
            }
        }

        /// <summary>
        /// Affiche les tuiles (boutons) en fonction des droits
        /// </summary>
        private void AfficheTuile()
        {
            // Retrouver les droits de l'utilisateur dans la table des droits 
            // L'administrateur proget accéde à tout          
            if( ParamGlobaux.Matricule != ParamGlobaux.StNomProget )
            {
                // Retouver tous les droits de cet agent
                DsDroits = ClassGetMS.Droits.LireDroits( ParamGlobaux.Matricule, ParamGlobaux.IDEtablissement, ParamGlobaux.ConnectionString );

            }
        }

        #endregion

        #region Mail
        /// <summary>
        /// Bouton Envoyer
        /// </summary>        
        private void SendMail()
        {


            MailConfigViewModel MailConfigVM = new MailConfigViewModel(_dataService);

            MailConfigVM.Subject = "Nouvelle demande d'absense ";

            MailConfigVM.MessageText = "Bonjour,\n" +
                "\n" +
                "Nous vous informons, de la nouvelle demande d'absense de :" +
                "\n" + "\n" +
                "M ou Mme " + _SelectedAgent.Nom + " " + _SelectedAgent.Prenom +
                 "\n" + "\n" +
              //   "Du " + _DateDebut.ToShortDateString() + " au " + _DateFin.ToShortDateString() +
                 "\n" + "\n" +
                "Cordialement,\n" +
                "\n" +
                "La Direction\n";

            _dialogService.ShowDialog<MailConfigView>(this, MailConfigVM);
        }
        #endregion
    }
}
