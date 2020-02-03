using ClassCrystalReportProduction.Dialogs;
using ClassGetMS;
using ClassGetMS.Models;
using ClassGetMS.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClassCrystalReportProduction.ViewModels

{
    #region Enum Previsionnel
    public enum PrevisionnelEnum
    {
        Oui, Non
    }
    #endregion

    /// <summary>
    /// This class contains properties that a View can data bind to.    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class BordereauDeclaratifViewModel : ViewModelBase
    {

        #region Variables constructeur

        private IDataService _dataService;
        private IDialogService _dialogService;
        private SqlConnection oConnection;              // Parametre de connexion à la database qui sera utilisé dans tout le programme

        private int DureeMaxCDDCourt;

        private bool Initializing;

        /// <summary>
        /// Initializes a new instance of the PresenceHebdomadaireViewModel class.
        /// </summary>
        public BordereauDeclaratifViewModel( IDataService dataService, IDialogService dialogService )
        {
            _dataService = dataService;
            _dialogService = dialogService;

            SelectServicesCommand = new RelayCommand<bool>( ( check ) => ExecuteSelectServices( check ) );
            SelectSectionsCommand = new RelayCommand<bool>( ( check ) => ExecuteSelectSections( check ) );
            AideCommand = new RelayCommand( () => ClassUILibrary.Design.AfficheAide( "AideBordereauDeclaratifHebdomadaire.pdf", oConnection ) );
            LoadedCommand = new RelayCommand( () => Initialize() );
            ImprimerCommand = new RelayCommand( () => ExecuteImprimer() );
            QuitterCommand = new RelayCommand( () => Close = true );
            UnLoadedCommand = new RelayCommand( () => ExecuteUnLoaded() );
            SelectAgentCommand = new RelayCommand<AgentModel>( ( ag ) => ExecuteSelectAgent( ag ) );
        }
        #endregion

        #region Properties
        private DebutFin _SelectedPeriode;
        public DebutFin SelectedPeriode
        {
            get
            {
                return _SelectedPeriode;
            }
            set
            {
                if( value != _SelectedPeriode )
                {
                    _SelectedPeriode = value;
                    RaisePropertyChanged( "SelectedPeriode" );
                    RaisePropertyChanged( "CanValidate" );
                    Task task = UpdateSemaines();

                }
            }
        }

        private ObservableCollection<DebutFin> _Periodes;
        public ObservableCollection<DebutFin> Periodes
        {
            get
            {
                return _Periodes;
            }
            set
            {
                if( value != _Periodes )
                {
                    _Periodes = value;
                    RaisePropertyChanged( "Periodes" );
                }
            }
        }

        private DebutFin _SelectedSemaine;
        public DebutFin SelectedSemaine
        {
            get
            {
                if( _SelectedSemaine != null )
                {
                    SelectedMois = _SelectedSemaine.Debut;
                }

                return _SelectedSemaine;
            }
            set
            {
                if( value != _SelectedSemaine )
                {
                    _SelectedSemaine = value;
                    
                    RaisePropertyChanged( "SelectedSemaine" );
                    RaisePropertyChanged( "CanValidate" );

                    if( _SelectedSemaine != null )
                    {
                        SelectedMois = _SelectedSemaine.Debut;
                        Debug.WriteLine( "Semaine du " + _SelectedSemaine.Debut + " au " + _SelectedSemaine.Fin + " selectionnée" );
                    }
                }
            }
        }

        private ObservableCollection<DebutFin> _Semaines;
        public ObservableCollection<DebutFin> Semaines
        {
            get
            {
                return _Semaines;
            }
            set
            {
                if( value != _Semaines )
                {
                    _Semaines = value;
                    RaisePropertyChanged( "Semaines" );
                }
            }
        }

     
        private string _LibelléSection;
        public string LibelléSection
        {
            get
            {
                return _LibelléSection;
            }
            set
            {
                if( value != _LibelléSection )
                {
                    _LibelléSection = value;
                    RaisePropertyChanged( nameof( LibelléSection ) );
                }
            }
        }

        public bool CanValidate
        {
            get
            {
                if( _SelectedPeriode == null || _SelectedSemaine == null )
                    return false;
                if( !Initializing )
                {
                    foreach( ServiceSectionModel ser in _Services )
                        if( ser.Selected )
                            return true;
                    foreach( ServiceSectionModel sec in _Sections )
                        if( sec.Selected )
                            return true;
                    return false;
                }
                else
                    return false;
            }
        }

       
       

        private PrevisionnelEnum _OuiNon;
        public PrevisionnelEnum OuiNon
        {
            get
            {
                return _OuiNon;
            }
            set
            {
                if( value != _OuiNon )
                {
                    _OuiNon = value;
                    RaisePropertyChanged( "OuiNon" );                   
                    
                }
            }
        }


        private bool _EnableImprimer;
        public bool EnableImprimer
        {
            get
            {
                if( DatesDebutFin != null && _Agents != null )
                    return DatesDebutFin != null && _Agents.Any( ag => ag.IsSelected );
                return false;
            }
            set
            {
                if( value != _EnableImprimer )
                {
                    _EnableImprimer = value;
                    RaisePropertyChanged( "EnableImprimer" );
                }
            }
        }

        private string _Titre;
        public string Titre
        {
            get
            {
                return _Titre;
            }
            set
            {
                if( value != _Titre )
                {
                    _Titre = value;
                    RaisePropertyChanged( "Titre" );
                }
            }
        }

        private MinutesCentiemes _FormatHoraire;
        public MinutesCentiemes FormatHoraire
        {
            get
            {
                return _FormatHoraire;
            }
            set
            {
                if( value != _FormatHoraire )
                {
                    _FormatHoraire = value;
                    RaisePropertyChanged( "FormatHoraire" );
                }
            }
        }

        private string _Entete;
        public string Entete
        {
            get
            {
                return _Entete;
            }
            set
            {
                if( value != _Entete )
                {
                    _Entete = value;
                    RaisePropertyChanged( "Entete" );
                }
            }
        }

        private bool _EnableMoisPeriode;
        public bool EnableMoisPeriode
        {
            get
            {
                return _EnableMoisPeriode;
            }
            set
            {
                if( value != _EnableMoisPeriode )
                {
                    _EnableMoisPeriode = value;
                    RaisePropertyChanged( "EnableMoisPeriode" );
                }
            }
        }

        private string _LibelleSections;
        public string LibelleSections
        {
            get
            {
                return _LibelleSections;
            }
            set
            {
                if( value != _LibelleSections )
                {
                    _LibelleSections = value;
                    RaisePropertyChanged( "LibelleSections" );
                }
            }
        }

        private StatutModel _SelectedStatut;
        public StatutModel SelectedStatut
        {
            get
            {
                return _SelectedStatut;
            }
            set
            {
                if( value != null && value != _SelectedStatut )
                {
                    _SelectedStatut = value;

                    if( _SelectedStatut.Durée_limitée )
                        EnableMoisPeriode = true;
                    else
                    {
                        MoisPeriode = MoisPeriodeEnum.Mois;
                        EnableMoisPeriode = false;
                    }

                    RaisePropertyChanged( "SelectedStatut" );
                    UpdateListeAgents();
                }
            }
        }

        private ObservableCollection<StatutModel> _Statuts;
        public ObservableCollection<StatutModel> Statuts
        {
            get
            {
                return _Statuts;
            }
            set
            {
                if( value != _Statuts )
                {
                    _Statuts = value;
                    RaisePropertyChanged( "Statuts" );
                }
            }
        }


        private ServiceSectionModel _SelectedService;
        public ServiceSectionModel SelectedService
        {
            get
            {
                return _SelectedService;
            }
            set
            {
                if( value != _SelectedService )
                {
                    _SelectedService = value;
                    RaisePropertyChanged( "SelectedService" );
                    UpdateListeAgents();
                }
            }
        }

        private ObservableCollection<ServiceSectionModel> _Services;
        public ObservableCollection<ServiceSectionModel> Services
        {
            get
            {
                return _Services;
            }
            set
            {
                if( value != _Services )
                {
                    _Services = value;
                    RaisePropertyChanged( "Services" );
                }
            }
        }

        private ServiceSectionModel _SelectedSection;
        public ServiceSectionModel SelectedSection
        {
            get
            {
                return _SelectedSection;
            }
            set
            {
                if( value != _SelectedSection )
                {
                    _SelectedSection = value;
                    RaisePropertyChanged( "SelectedSection" );
                    UpdateListeAgents();
                }
            }
        }

        private ObservableCollection<ServiceSectionModel> _Sections;
        public ObservableCollection<ServiceSectionModel> Sections
        {
            get
            {
                return _Sections;
            }
            set
            {
                if( value != _Sections )
                {
                    _Sections = value;
                    RaisePropertyChanged( "Sections" );
                }
            }
        }

        private Emploi _SelectedEmploi;
        public Emploi SelectedEmploi
        {
            get
            {
                return _SelectedEmploi;
            }
            set
            {
                if( value != _SelectedEmploi )
                {
                    _SelectedEmploi = value;
                    RaisePropertyChanged( "SelectedEmploi" );
                    UpdateListeAgents();
                }
            }
        }

        private ObservableCollection<Emploi> _Emplois;
        public ObservableCollection<Emploi> Emplois
        {
            get
            {
                return _Emplois;
            }
            set
            {
                if( value != _Emplois )
                {
                    _Emplois = value;
                    RaisePropertyChanged( "Emplois" );
                }
            }
        }


        private ServiceSectionModel _SelectedFiliere;
        public ServiceSectionModel SelectedFiliere
        {
            get
            {
                return _SelectedFiliere;
            }
            set
            {
                if( value != _SelectedFiliere )
                {
                    _SelectedFiliere = value;
                    RaisePropertyChanged( "SelectedFiliere" );
                    UpdateListeAgents();
                }
            }
        }

        private ObservableCollection<ServiceSectionModel> _Filieres;
        public ObservableCollection<ServiceSectionModel> Filieres
        {
            get
            {
                return _Filieres;
            }
            set
            {
                if( value != _Filieres )
                {
                    _Filieres = value;
                    RaisePropertyChanged( "Filieres" );
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

        private MoisPeriodeEnum _MoisPeriode;
        public MoisPeriodeEnum MoisPeriode
        {
            get
            {
                return _MoisPeriode;
            }
            set
            {
                if( value != _MoisPeriode )
                {
                    _MoisPeriode = value;
                    RaisePropertyChanged( "MoisPeriode" );

                    UpdateListeAgents();
                    //if( value == MoisPeriodeEnum.Periode )
                    //{
                    //    if( Agents.Where( ag => ag.IsSelected ).Count() > 1 )
                    //        SelectAllAgents = false;
                       
                    //}
                    RaisePropertyChanged( "TextDateContrat" );
                    RaisePropertyChanged( "EnableCalendrier" );
                    RaisePropertyChanged( "EnableContrat" );
                    //RaisePropertyChanged( "EnableSelectAllAgents" );
                    RaisePropertyChanged( "EnableImprimer" );
                }
            }
        }

        private DateTime _SelectedMois;
        public DateTime SelectedMois
        {
            get
            {
                return _SelectedMois;
            }
            set
            {

                if( value != _SelectedMois )
                {
                    _SelectedMois = value;
                    RaisePropertyChanged( "SelectedMois" );
                    UpdateListeAgents();
                }
            }
        }


        public DebutFin DatesDebutFin
        {
            get
            {

                return new DebutFin( new DateTime( _SelectedMois.Year, _SelectedMois.Month, 1 ), new DateTime( _SelectedMois.Year, _SelectedMois.Month, DateTime.DaysInMonth( _SelectedMois.Year, _SelectedMois.Month ) ) );

            }
        }

        #endregion

        #region Relay Command
        public RelayCommand<bool> SelectServicesCommand { get; private set; }
        public RelayCommand<bool> SelectSectionsCommand { get; private set; }
        public RelayCommand ImprimerCommand { get; private set; }
        public RelayCommand QuitterCommand { get; private set; }
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnLoadedCommand { get; set; }
        public RelayCommand AideCommand { get; set; }
        public RelayCommand<AgentModel> SelectAgentCommand { get; set; }
        #endregion

        #region Initialisation
        public async void Initialize()
        {
            Initializing = true;
            OuiNon = PrevisionnelEnum.Non;

            EnableImprimer = false;

            Services = await _dataService.GetServicesAsync();
            Sections = await _dataService.GetSectionsAsync( _dataService.ParamGlobaux.IDEtablissement );
            LibelléSection = await _dataService.GetParametreAsync( "Libellé section" );
            DebutFin dates = await _dataService.GetLimitesPlanningAsync();
            Periodes = await _dataService.GetPeriodesEtablissementAsync();
            

            var query = from DebutFin periode in Periodes where DateTime.Now >= periode.Debut && DateTime.Now <= periode.Fin select periode;
            SelectedPeriode = (DebutFin)query.First();
            ///////////////////////////////////////////////////////////////////////////////////
            Titre = "Bordereau Déclaratif: " + _dataService.ParamGlobaux.Etablissement + ", Utilisateur: " + _dataService.ParamGlobaux.Nom + " " + _dataService.ParamGlobaux.Prénom;
            MoisPeriode = MoisPeriodeEnum.Mois;

            // ************************************************************************************************************************************************
            // Rechercher le parametre Format Horaire pour connaitre l'affichage de la combobox
            // ************************************************************************************************************************************************
            
                FormatHoraire = MinutesCentiemes.Minutes;
            

            DureeMaxCDDCourt = Convert.ToInt32( await _dataService.GetParametreAsync( "Durée maxi CDD court" ) );


            var taskServices = _dataService.GetServicesAsync( true );
            var taskSections = _dataService.GetSectionsAsync( _dataService.ParamGlobaux.IDEtablissement, true );
            var taskEmplois = _dataService.GetEmploisAsync( true );
            var taskFilieres = _dataService.GetFilieresAsync( true );
            var taskStatuts = _dataService.GetStatutsAsync();
            var taskAgents = _dataService.GetAgentsContratsAsync();

            await Task.WhenAll( taskServices, taskSections, taskEmplois, taskFilieres, taskStatuts, taskAgents );

            _Services = taskServices.Result;
            _Sections = taskSections.Result;
            _Emplois = taskEmplois.Result;
            _Filieres = taskFilieres.Result;
            _Statuts = taskStatuts.Result;
            _TousAgents = taskAgents.Result.ToList();

            oConnection = new SqlConnection( _dataService.ParamGlobaux.ConnectionString );

            

            ///////////////////////Suppression des agents n'apparaissant pas dans la table planning////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<string> listeAgentsPlanning = new List<string>();
            DataSet DsTemp = new DataSet();
            string StSQL;
            StSQL = "SELECT DISTINCT Planning.Matricule from Planning";

            DsTemp = ClassLibraryProget.DataBase.SELECTSqlServer( oConnection, "Planning", StSQL );

            for( int i = 0; i < DsTemp.Tables[0].Rows.Count; i++ )
            {

                listeAgentsPlanning.Add( DsTemp.Tables[0].Rows[i]["Matricule"].ToString() );

            }
   
            for( int i = _TousAgents.Count-1 ; i >=0 ; i-- )
            {
                if( !listeAgentsPlanning.Contains( _TousAgents[i].Matricule ) )
                {
                    _TousAgents.Remove( _TousAgents[i] );
                }
            }
            
         

            // Pour les non gestionnaire, n'afficher que l'agent en cours
            if( !_dataService.ParamGlobaux.Gestionnaire )
            {
                _TousAgents.RemoveAll( ag => ag.Matricule != _dataService.ParamGlobaux.StMatricule );
                
            }

           
            LibelleSections = ClassGetMS.Parametres.RechercherParametres( oConnection, _dataService.ParamGlobaux.IDEtablissement, _dataService.ParamGlobaux.StMatricule, "Libellé section" );

            RaisePropertyChanged( "Services" );
            RaisePropertyChanged( "Sections" );
            RaisePropertyChanged( "Emplois" );
            RaisePropertyChanged( "Filieres" );
            RaisePropertyChanged( "Statuts" );

            SelectedMois = _SelectedSemaine.Debut;

            if( !String.IsNullOrEmpty( _SingleAgentMatricule ) )
            {
                SelectedStatut = ( from StatutModel statut in _Statuts
                                   where statut.IDStatut == _TousAgents.Where( ag => ag.Matricule == SingleAgentMatricule
                                                                                          && _SelectedMois >= ag.DebutContrat
                                                                                          && ( ag.FinContrat == null || _SelectedMois <= ag.FinContrat ) ).First().Typecontrat
                                   select statut ).First();
                RaisePropertyChanged( "SelectedMois" );
            }
            else
            {
                SelectedStatut = _Statuts.ToList().GetRange( 1, _Statuts.Count - 1 ).First( s => s.Durée_limitée == false );
                //Par défaut, le mois sélectionné est le mois en cours
               // SelectedMois = new DateTime( DateTime.Today.Year, DateTime.Today.Month, 1 );
                //_SelectAllAgents = true;
            }

            _Agents = new ObservableCollection<AgentModel>();
        

            RaisePropertyChanged( "SelectAllAgents" );

            UpdateListeAgents();

            RaisePropertyChanged( "SingleAgentMatricule" );
            RaisePropertyChanged( "EnableSelectionAgents" );
            RaisePropertyChanged( "EnableImprimer" );
            ///////////////////////////////////////////////////////////////////////////////////////

            Initializing = false;
        }
        #endregion

        #region Methodes

        private void ExecuteSelectAgent( AgentModel agent )
        {
            if( MoisPeriode == MoisPeriodeEnum.Periode )
                foreach( AgentModel ag in _Agents )
                    if( ag != agent )
                        ag.IsSelected = false;
          
        }

        private void ExecuteUnLoaded()
        {

            SingleAgentMatricule = null;
            Close = false;
            _Agents = null;

        }

        public async Task UpdateSemaines()
        {
            Semaines = await _dataService.GetSemainesAsync( _SelectedPeriode.Debut, _SelectedPeriode.Fin.Value );
            if( ( DateTime.Now >= _SelectedPeriode.Debut ) && ( DateTime.Now <= _SelectedPeriode.Fin ) )
            {
                var query = from DebutFin semaine in _Semaines where DateTime.Now >= semaine.Debut && DateTime.Now <= semaine.Fin select semaine;
                SelectedSemaine = (DebutFin)query.First();
            }
        }

        private void ExecuteSelectServices( bool check )
        {
            foreach( ServiceSectionModel service in Services )
                service.Selected = check;
        }

        private void ExecuteSelectSections( bool check )
        {
            foreach( ServiceSectionModel section in Sections )
                section.Selected = check;
        }
        #endregion

        #region Agents
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
                    RaisePropertyChanged( "EnableSelectAllAgents" );
                    RaisePropertyChanged( "EnableImprimer" );
                }
            }
        }

        public bool EnableSelectAllAgents
        {
            get
            {
              
                return MoisPeriode == MoisPeriodeEnum.Mois;
            }
        }

        private bool _SelectAllAgents;
        public bool SelectAllAgents
        {
            get
            {
                return _SelectAllAgents;
            }
            set
            {
                _SelectAllAgents = value;
                foreach( AgentModel ag in _Agents )
                    ag.IsSelected = value;
                RaisePropertyChanged( "SelectAllAgents" );
                RaisePropertyChanged( "Agents" );
                RaisePropertyChanged( "EnableImprimer" );
            }
        }


        private string _SingleAgentMatricule;
        public string SingleAgentMatricule
        {
            get
            {
                return _SingleAgentMatricule;
            }
            set
            {
                if( value != _SingleAgentMatricule )
                {
                    _SingleAgentMatricule = value;
                    RaisePropertyChanged( "SingleAgentMatricule" );
                    RaisePropertyChanged( "EnableSelectionAgents" );
                    RaisePropertyChanged( "EnableImprimer" );
                }
            }
        }
        public bool EnableSelectionAgents
        {
            get
            {
                return String.IsNullOrEmpty( SingleAgentMatricule );
            }
        }
        #endregion
       
        #region update liste agents
        /// <summary>
        /// Mettre à jour la liste des agent de l'établissement
        /// </summary>
        private void UpdateListeAgents()
        {
            if( _TousAgents != null && _Agents != null )
            {
                IEnumerable<AgentModel> _agents;
                if( _SelectedStatut != null )
                {
                    _agents = from agent in _TousAgents
                              where agent.Typecontrat == _SelectedStatut.IDStatut
                              select agent;
                    if( MoisPeriode == MoisPeriodeEnum.Mois )
                        _agents = from agent in _agents
                                  where DatesDebutFin.Fin >= agent.DebutContrat
                                    && ( agent.FinContrat == null
                                    || DatesDebutFin.Debut <= agent.FinContrat )
                                  select agent;
                    if( _SelectedService != null && !String.IsNullOrEmpty( _SelectedService.ID ) )
                        _agents = from agent in _agents
                                  where agent.IDService == _SelectedService.ID
                                  select agent;
                    if( _SelectedSection != null && !String.IsNullOrEmpty( _SelectedSection.ID ) )
                        _agents = from agent in _agents
                                  where agent.IDSection == _SelectedSection.ID
                                  select agent;
                    if( _SelectedEmploi != null && !String.IsNullOrEmpty( _SelectedEmploi.IDEmploi ) )
                        _agents = from agent in _agents
                                  where agent.IDEmploi == _SelectedEmploi.IDEmploi
                                  select agent;
                    if( _SelectedFiliere != null && !String.IsNullOrEmpty( _SelectedFiliere.ID ) )
                        _agents = from agent in _agents
                                  where agent.IDFiliere == _SelectedFiliere.ID
                                  select agent;

                    _Agents.Clear();
                    foreach( AgentModel ag in _agents.Distinct().OrderBy( a => a.Prenom ).OrderBy( a => a.Nom ) )
                    {
                        _Agents.Add( ag );
                    }

                    if( _Agents != null && _Agents.Count > 0 )
                    {
                        if( !String.IsNullOrEmpty( SingleAgentMatricule ) && _Agents.SingleOrDefault( ag => ag.Matricule == _SingleAgentMatricule ) != null )
                        {
                            _Agents.SingleOrDefault( ag => ag.Matricule == _SingleAgentMatricule ).IsSelected = true;
                        }
                        else
                            SelectAllAgents = true;
      
                    }

                    RaisePropertyChanged( "Agents" );
                }
            }
        }
        #endregion

        #region Bouton Imprimer
        private void ExecuteImprimer()
        {
            if( Agents.Any( ag => ag.IsSelected ) && _SelectedSemaine != null)
            {

                _dataService.Jour = SelectedSemaine.Debut;

                //ApercuBordereauDeclaratifViewModel bordereauDeclaratifVM = SimpleIoc.Default.GetInstance<ApercuBordereauDeclaratifViewModel>();

                ApercuBordereauDeclaratifViewModel bordereauDeclaratifVM = new ApercuBordereauDeclaratifViewModel( _dataService, _dialogService );

                List <string> listeMatricules = new List<string>();

                foreach( var item in Agents.Where( ag => ag.IsSelected ) )
                {
                    listeMatricules.Add(  item.Matricule );
                }
           
                bordereauDeclaratifVM.entete = _Entete;                
                bordereauDeclaratifVM.Agents = listeMatricules;
                bordereauDeclaratifVM.previsionnel = OuiNon;
                bordereauDeclaratifVM.Mois = DatesDebutFin.Debut;

                _dialogService.ShowDialog<ApercuBordereauDeclaratif>( this, bordereauDeclaratifVM );

            }else
            {
                EnableImprimer = false;
            }

        }
        #endregion
    }


}