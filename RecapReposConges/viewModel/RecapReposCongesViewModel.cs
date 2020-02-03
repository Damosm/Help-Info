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
using System.Windows;

namespace ClassCrystalReportProduction.ViewModels
{   
    #region class PeriodeModulationIsSelected
    /*
    public class PeriodeModulationIsSelected : PeriodeModulation
    {
        private bool _IsSelectedPeriode;

        public PeriodeModulationIsSelected( PeriodeModulation perMod ) : base( perMod )
        {
        }

        public bool IsSelectedPeriode
        {
            get
            {
                return _IsSelectedPeriode;
            }
            set
            {
                if(value != _IsSelectedPeriode )
                {
                    _IsSelectedPeriode = value;
                }

                RaisePropertyChanged( nameof( IsSelectedPeriode ) );
            }
        }


    }
    */
    #endregion
    
    public class RecapReposCongesViewModel : ViewModelBase
    {
        #region Variables constructeur
        private IDataService _dataService;
        private IDialogService _dialogService;
        private SqlConnection oConnection;
        private StrucParam ParamGlobaux;
        private int DureeMaxCDDCourt;


        public RecapReposCongesViewModel( IDataService dataService, IDialogService dialogService )
        {
            _dataService = dataService;
            _dialogService = dialogService;

            AideCommand = new RelayCommand( () => ClassUILibrary.Design.AfficheAide( "AideRecapReposConges.pdf", oConnection ) );
            LoadedCommand = new RelayCommand( () => Initialize() );
            UnLoadedCommand = new RelayCommand( () => ExecuteUnLoaded() );
            QuitterCommand = new RelayCommand( () => Close = true );
            ImprimerCommand = new RelayCommand( () => ExecuteImprimer() );
            UpdateListPeriode = new RelayCommand( () => UpdateListePeriodes() );
        }
        #endregion

        #region Proprietes
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
                    RaisePropertyChanged( nameof( Close ) );
                }
            }
        }
        private bool _EnableImprimer;
        public bool EnableImprimer
        {
            get
            {               
                return _EnableImprimer;
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
                   
                    RaisePropertyChanged( nameof( SelectedAgent ) );


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

                   // UpdateListePeriodes();

                    RaisePropertyChanged( nameof(Agents) );
                    RaisePropertyChanged( nameof( PeriodeCommun ) );
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
                    //SelectedService = Services[0];
                    //SelectedEmploi = Emplois[0];

                    UpdateListeAgents();
                    RaisePropertyChanged( "SelectedStatut" );
                    
                }
            }
        }
        
        private PeriodeModulation _PerModIsSelected;
        public PeriodeModulation PerModIsSelected
        {
            get
            {
                return _PerModIsSelected;
            }
            set
            {
                if( value != _PerModIsSelected )
                {
                    _PerModIsSelected = value;
                }
                RaisePropertyChanged( nameof( PerModIsSelected ) );
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

                    //SelectedEmploi = Emplois[0];

                    RaisePropertyChanged( "SelectedService" );
                    UpdateListeAgents();
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

        private bool _IsEnable;
        public bool IsEnable
        {
            get
            {
                return _IsEnable;
            }
            set
            {
                if(value != _IsEnable )
                {
                    _IsEnable = value;
                }                
                
                RaisePropertyChanged( nameof( IsEnable ) );
            }
        }
        private ObservableCollection<PeriodeModulation> _PeriodeCommun;
        public ObservableCollection<PeriodeModulation> PeriodeCommun
        {
            get {
                return _PeriodeCommun;
            }
            set
            {
                _PeriodeCommun = value;

                RaisePropertyChanged( "PeriodeCommun" );
            }
        }
        
        #endregion

        #region Relay Command       

        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnLoadedCommand { get; set; }
        public RelayCommand AideCommand { get; set; }
        public RelayCommand<AgentModel> SelectAgentCommand { get; set; }
        public RelayCommand ImprimerCommand { get; private set; }        
        public RelayCommand QuitterCommand { get; private set; }
        public RelayCommand UpdateListPeriode { get; set; }


        #endregion

        #region Methodes
        private void ExecuteUnLoaded()
        {
            Close = false;
            SelectedAgent = null;
            _SelectedStatut = null;
            _SelectedService = null;
            _SelectedEmploi = null;


        }


        #endregion

        #region Initialisation
        public async void Initialize()
        {
            ParamGlobaux = _dataService.ParamGlobaux;
            EnableImprimer = false;

            oConnection = ClassLibraryProget.DataBase.OpenSqlServer( _dataService.ParamGlobaux.ConnectionString );
            DureeMaxCDDCourt = Convert.ToInt32( Parametres.RechercherParametres( oConnection, ParamGlobaux.IDEtablissement, "", "Durée maxi CDD court" ) );
            oConnection.Close();

            IsEnable = true;

            //Initialisation des listes///////////

            SelectedAgent = new AgentModel();
            PeriodeCommun = new ObservableCollection<PeriodeModulation>();

            var taskAgents = _dataService.GetAgentsContratsAsync();
            var taskStatuts = _dataService.GetStatutsAsync(true);
            var taskServices = _dataService.GetServicesAsync( true );            
            var taskEmplois = _dataService.GetEmploisAsync( true );
            

            await Task.WhenAll( taskAgents, taskStatuts, taskServices, taskEmplois );

            _TousAgents = taskAgents.Result.ToList();

            Agents = new ObservableCollection<AgentModel>( _TousAgents.Distinct().OrderBy( a => a.Prenom ).OrderBy( a => a.Nom ) );

            Statuts = taskStatuts.Result;         
            Services = taskServices.Result;
            Emplois = taskEmplois.Result;

            

            //On initialise les selections sur la ligne vide
            _SelectedStatut = Statuts[0];
            _SelectedService = Services[0];
            _SelectedEmploi = Emplois[0];

            SelectAllAgents = false;
            UpdateListePeriodes();
            
        }
        #endregion

        #region update liste agents
        /// <summary>
        /// Mettre à jour la liste des agent de l'établissement
        /// </summary>
        private void UpdateListeAgents()
        {
            if( _TousAgents != null && Agents != null )
            {
                IEnumerable<AgentModel> _agents;
                if( !String.IsNullOrEmpty( SelectedStatut.IDStatut ) )
                {
                    _agents = from agent in _TousAgents
                              where agent.Typecontrat == SelectedStatut.IDStatut
                              select agent;
                    
                    if( SelectedService != null && !String.IsNullOrEmpty( SelectedService.ID ) )
                        _agents = from agent in _agents
                                  where agent.IDService == SelectedService.ID
                                  select agent;
                    
                    if( SelectedEmploi != null && !String.IsNullOrEmpty( SelectedEmploi.IDEmploi ) )
                        _agents = from agent in _agents
                                  where agent.IDEmploi == SelectedEmploi.IDEmploi
                                  select agent;
                   

                    Agents.Clear();
                    foreach( AgentModel ag in _agents.Distinct().OrderBy( a => a.Prenom ).OrderBy( a => a.Nom ) )
                    {
                        Agents.Add( ag );
                    }

                   
                           SelectAllAgents = false;

                   

                    RaisePropertyChanged( "Agents" );
                    UpdateListePeriodes();

                }
                else if ( String.IsNullOrEmpty( SelectedStatut.IDStatut )  && (  !String.IsNullOrEmpty( SelectedService.ID ) ))
                {

                    _agents = from agent in _TousAgents
                              where agent.IDService == SelectedService.ID
                              select agent;

                    if( SelectedEmploi != null && !String.IsNullOrEmpty( SelectedEmploi.IDEmploi ) )
                        _agents = from agent in _agents
                                  where agent.IDEmploi == SelectedEmploi.IDEmploi
                                  select agent;

                    Agents.Clear();

                    foreach( AgentModel ag in _agents.Distinct().OrderBy( a => a.Prenom ).OrderBy( a => a.Nom ) )
                    {
                        Agents.Add( ag );
                    }

                    SelectAllAgents = false;

                    RaisePropertyChanged( "Agents" );
                    UpdateListePeriodes();
                }
                else if( String.IsNullOrEmpty( SelectedStatut.IDStatut ) && ( String.IsNullOrEmpty( SelectedService.ID ) ) && !String.IsNullOrEmpty( SelectedEmploi.IDEmploi ) )
                {
                    
                        _agents = from agent in _TousAgents
                                  where agent.IDEmploi == SelectedEmploi.IDEmploi
                                  select agent;

                    Agents.Clear();

                    foreach( AgentModel ag in _agents.Distinct().OrderBy( a => a.Prenom ).OrderBy( a => a.Nom ) )
                    {
                        Agents.Add( ag );
                    }

                    SelectAllAgents = false;

                    RaisePropertyChanged( "Agents" );
                    UpdateListePeriodes();
                }
                else
                {
                    Agents = new ObservableCollection<AgentModel>( _TousAgents.Distinct().OrderBy( a => a.Prenom ).OrderBy( a => a.Nom ) );

                    RaisePropertyChanged( "Agents" );
                    UpdateListePeriodes();
                }
            }
            
        }
        #endregion

        #region update liste périodes
        /// <summary>
        /// Mettre à jour la liste des périodes en fonction des l'agents selectionnés
        /// </summary>
        private async void UpdateListePeriodes()
        {
            IsEnable = false;
            // on remplit une liste de listes de périodes de modulation de tous les agents selectionnés
            List<List<PeriodeModulation>> listeDeListPeriode = new List<List<PeriodeModulation>>();

            List<PeriodeModulation> ListPeriode = new List<PeriodeModulation>();
            PeriodeCommun.Clear();                        

            foreach( AgentModel agent in Agents.Where( ag => ag.IsSelected ) )
            {               
                List<PeriodeModulation> periodesMod = await gestion.PeriodesModulation( ParamGlobaux.ConnectionString, agent.Matricule, DateTime.Now, DureeMaxCDDCourt );
                
                listeDeListPeriode.Add( periodesMod );
            }

            foreach( List<PeriodeModulation> list in listeDeListPeriode )
            {
                List<List<PeriodeModulation>> listPerTruncated = listeDeListPeriode.Where( lp => lp != list ).ToList();

                foreach ( PeriodeModulation perMod in list)
                {
                    bool found = true;
                    foreach( List<PeriodeModulation> list2 in listPerTruncated )
                    {
                        //Porte logique
                        found &= list2.Any( p => p.Debut == perMod.Debut && p.Fin == perMod.Fin );
                        if( !found )
                            break;
                    }
                    if( found && !PeriodeCommun.Any( p => p.Debut == perMod.Debut && p.Fin == perMod.Fin ) )
                        PeriodeCommun.Add( perMod );
                }
            }

            PeriodeCommun = new ObservableCollection<PeriodeModulation>( PeriodeCommun.OrderBy( p => p.Debut ).Reverse());
            IsEnable = true;
        }
            #endregion

            #region Bt Imprimer
            private void ExecuteImprimer()
            {
                if( PerModIsSelected != null )
                {
                Debug.WriteLine( PerModIsSelected.Debut );

                ApercuRecapReposCongesViewModel RecapReposCongesVM = new ApercuRecapReposCongesViewModel( _dataService, _dialogService );

                RecapReposCongesVM.PeriodeModulation = PerModIsSelected;
                RecapReposCongesVM.ListAgents = Agents.Where( ag => ag.IsSelected );
                                

                _dialogService.ShowDialog<ApercuRecapReposConges>( this, RecapReposCongesVM );
            }
                else
                {
                       MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox( this,
                       "Veuillez selectionner une période de modulation",
                       "Erreur",
                       MessageBoxButton.OK,
                       MessageBoxImage.Exclamation );
                }
            
                

            }
        #endregion
    }

    internal class asyn
    {
    }
}
