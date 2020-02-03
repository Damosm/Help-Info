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
using GET_MS.Views;
using MvvmDialogs;
using System;
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
using System.Windows.Interop;
using System.Windows.Media;

namespace GET_MS.ViewModels
{   
       
    #region class DemandeAbsenceObservable
    public class DemandeAbsenceObservable : ObservableObject
    {
        #region Variables
        private string _Matricule;
        public string Matricule
        {
            get
            {
                return _Matricule;
            }
            set
            {
                if (value != _Matricule)
                {
                    _Matricule = value;
                    RaisePropertyChanged(nameof(Matricule));
                    
                }
            }
        }

        private DateTime _Date_debut;
        public DateTime Date_debut
        {
            get
            {
                return _Date_debut;
            }
            set
            {
                if (value != _Date_debut)
                {
                    _Date_debut = value;
                    RaisePropertyChanged(nameof(Date_debut));

                }
            }
        }

        private DateTime _Date_fin;
        public DateTime Date_fin
        {
            get
            {
                return _Date_fin;
            }
            set
            {
                if (value != _Date_fin)
                {
                    _Date_fin = value;
                    RaisePropertyChanged(nameof(Date_fin));

                }
            }
        }

        private string _Type_Jour;
        public string Type_Jour
        {
            get
            {
                return _Type_Jour;
            }
            set
            {
                if (value != _Type_Jour)
                {
                    _Type_Jour = value;
                    RaisePropertyChanged(nameof(Type_Jour));

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
                if (value != _Commentaire_agent)
                {
                    _Commentaire_agent = value;
                    RaisePropertyChanged(nameof(Commentaire_agent));

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
                if (value != _Commentaire_gestionnaire)
                {
                    _Commentaire_gestionnaire = value;
                    RaisePropertyChanged(nameof(Commentaire_gestionnaire));

                }
            }
        }

        private string _Etat;
        public string Etat
        {
            get
            {
                return _Etat;
            }
            set
            {
                if (value != _Etat)
                {
                    _Etat = value;
                    RaisePropertyChanged(nameof(Etat));

                }
            }
        }
        #endregion

        #region Constructeurs
        ///<summary>
        ///constructeur de la classe
        ///</summary>
        ///<param name ="Matricule"></param>
        ///<param name="Date_debut"></param>
        ///<param name="Date_fin"></param>
        ///<param name="Type_Jour"></param>  
        ///<param name="Commentaire_agent"></param>
        ///<param name="Commentaire_gestionnaire"></param>
        ///<param name="Demande_acceptee"></param>
        ///<param name="En_cours"></param>
        public DemandeAbsenceObservable(string Matricule, DateTime Date_debut, DateTime Date_fin, string Type_Jour, string Commentaire_agent, string Etat)
        {
            this.Matricule = Matricule;
            this.Date_debut = Date_debut;
            this.Date_fin = Date_fin;
            this.Type_Jour = Type_Jour;
            this.Commentaire_agent = Commentaire_agent;         
            this.Etat = Etat;
            
        }


        public DemandeAbsenceObservable() { }
        

        public DemandeAbsenceObservable(Demande_Absence demande_Absence)
        {
            this.Matricule = demande_Absence.Matricule;
            this.Date_debut = demande_Absence.Date_debut;
            this.Date_fin = demande_Absence.Date_fin;
            this.Type_Jour = demande_Absence.Type_Jour;
            this.Commentaire_agent = demande_Absence.Commentaire_agent;
            this.Commentaire_gestionnaire = demande_Absence.Commentaire_gestionnaire;
            this.Etat = demande_Absence.Etat;
            
           
        }
        #endregion

        #region Methodes
        /// <summary>
        /// méthode de conversion d'un ObjetObservable en Demande_Absence
        /// </summary>
        /// <param name="ObjetObservable"></param>
        /// <returns></returns>
        public static Demande_Absence ConversionEnCodeRegime(DemandeAbsenceObservable ObjetObservable)
        {
            Demande_Absence demande_Absence = new Demande_Absence();

            demande_Absence.Matricule = ObjetObservable.Matricule;
            demande_Absence.Date_debut = ObjetObservable.Date_debut;
            demande_Absence.Date_fin = ObjetObservable.Date_fin;
            demande_Absence.Type_Jour = ObjetObservable.Type_Jour;
            demande_Absence.Commentaire_agent = ObjetObservable.Commentaire_agent;
            demande_Absence.Commentaire_gestionnaire = ObjetObservable.Commentaire_gestionnaire;
            demande_Absence.Etat = ObjetObservable.Etat;
            

            return demande_Absence;
        }
        #endregion
    }
    #endregion

    public class DemandeAbsenceViewModel : ViewModelBase
    {

        #region propriétés

        private IDataService _dataService;
        private IDialogService _dialogService;
        private SqlConnection oConnection;
        private bool Initializing;

        DataSet DsTemp = new DataSet();
        private StrucParam ParamGlobaux;

        private string _Etat;

        public string Etat
        {
            get
            {
                return _Etat;
            }
            set
            {
                if (value != _Etat)
                {
                    _Etat = value;
                    RaisePropertyChanged(nameof(Etat));

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
                if (value != _Status)
                {
                    _Status = value;                    
                    
                    RaisePropertyChanged(nameof(Status));

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

                if (value != _Commentaire_agent)
                {
                    _Commentaire_agent = value;
                    RaisePropertyChanged(nameof(Commentaire_agent));
                }
            }
        }

        private string _Commentaire_agentListe;
        public string Commentaire_agentListe
        {
            get
            {
                return _Commentaire_agentListe;
            }
            set
            {

                if( value != _Commentaire_agentListe )
                {
                    _Commentaire_agentListe = value;
                    RaisePropertyChanged( nameof( Commentaire_agentListe ) );
                }
            }
        }

        private DemandeAbsenceObservable _ObsDemandeAbsence;
        public DemandeAbsenceObservable ObsDemandeAbsence
        {
            get
            {
                return _ObsDemandeAbsence;
            }
            set
            {
                if (value != _ObsDemandeAbsence)
                {
                    _ObsDemandeAbsence = value;
                    RaisePropertyChanged(nameof(ObsDemandeAbsence));
                }
            }
        }
        private bool _IsValid;
        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                if (value != _IsValid)
                {
                    _IsValid = value;
                    RaisePropertyChanged("IsValid");
                }
            }
        }
        private bool _IsValid2;
        public bool IsValid2
        {
            get
            {
                return _IsValid2;
            }
            set
            {
                if( value != _IsValid2 )
                {
                    _IsValid2 = value;
                    RaisePropertyChanged( "IsValid2" );
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
                if (value != _Close)
                {
                    _Close = value;
                    RaisePropertyChanged("Close");
                }
            }
        }

        private bool _EnableImprimer;
        public bool EnableImprimer
        {
            get
            {
                //if (DatesDebutFin != null && _Agents != null)
                //    return DatesDebutFin != null && _Agents.Any(ag => ag.IsSelected);
                //return false;

                return _EnableImprimer;
            }
            set
            {
                if (value != _EnableImprimer)
                {
                    _EnableImprimer = value;
                    RaisePropertyChanged("EnableImprimer");
                }
            }
        }
        private List<TypesJours> _TousTypes;

        private ObservableCollection<TypesJours> _ListeTypeJour;
        public ObservableCollection<TypesJours> ListeTypeJour
        {
            get
            {
                return _ListeTypeJour;
            }
            set
            {
                if (value != _ListeTypeJour)
                {
                    _ListeTypeJour = value;
                    RaisePropertyChanged("ListeTypeJour");
                }
            }
        }
        //private List<Demande_Absence> _TtesDemande_Absence;

        private ObservableCollection<Demande_Absence> _ListeDemande_Absence;
        public ObservableCollection<Demande_Absence> ListeDemande_Absence
        {
            get
            {
               

                return _ListeDemande_Absence;
            }
            set
            {
                if (value != _ListeDemande_Absence)
                {
                    _ListeDemande_Absence = value;
                    RaisePropertyChanged(nameof(ListeDemande_Absence));
                }
            }
        }

        private Demande_Absence _SelectedDemande_Absence;
        public Demande_Absence SelectedDemande_Absence
        {
            get
            {
                return _SelectedDemande_Absence;
            }
            set
            {
                if (value != _SelectedDemande_Absence)
                {                    
                    _SelectedDemande_Absence = value;

                    if (SelectedDemande_Absence != null )
                    {
                        SelectedTJourListe = ListeTypeJour.Where( a => a.Libelle == SelectedDemande_Absence.Type_Jour ).SingleOrDefault();
                        DateDebutListe = SelectedDemande_Absence.Date_debut;
                        DateFinListe = SelectedDemande_Absence.Date_fin;
                        Commentaire_agentListe = SelectedDemande_Absence.Commentaire_agent;
                    }
                    



                    RaisePropertyChanged(nameof(SelectedDemande_Absence));


                }
            }
        }

        private Demande_Absence _Demande_AbsenceTemp;
        public Demande_Absence Demande_AbsenceTemp
        {
            get
            {
                return _Demande_AbsenceTemp;
            }
            set
            {
                if( value != _Demande_AbsenceTemp )
                {
                    _Demande_AbsenceTemp = value;

                    RaisePropertyChanged( nameof( Demande_AbsenceTemp ) );


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
                if (value != _Agents)
                {
                    
                    _Agents = value;
                    RaisePropertyChanged("Agents");
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
                if (value != _SelectedAgent)
                {
                    IsValid = false;
                    _SelectedAgent = value;
                    SelectedAgentListe = _SelectedAgent;
                    RaisePropertyChanged(nameof(SelectedAgent));
                    

                }
            }
        }

        private AgentModel _SelectedAgentListe;
        public  AgentModel SelectedAgentListe
        {
            get
            {
                return _SelectedAgentListe;
            }
            set
            {
                if (value != _SelectedAgentListe)
                {
                    _SelectedAgentListe = value;
                    SelectedAgent = SelectedAgentListe;

                    if (SelectedAgentListe != null)
                    {
                        Actualisation();

                        ListeDemande_Absence = new ObservableCollection<Demande_Absence>(ListeDemande_Absence.Where
                                (dm => dm.Matricule == SelectedAgentListe.Matricule));

                        SelectedDemande_Absence = null;
                        SelectedTJourListe = null;
                        DateDebutListe = DateTime.Today;
                        DateFinListe = DateTime.Today;
                        Commentaire_agentListe = null;

                    }
                    else
                    {
                        ListeDemande_Absence = null;
                    }



                    RaisePropertyChanged(nameof(SelectedAgentListe));


                }
            }
        }

        private TypesJours _SelectedTJour;
        public TypesJours SelectedTJour
        {
            get
            {
                return _SelectedTJour;
            }
            set
            {
                if (value != _SelectedTJour)
                {
                    IsValid = false;
                    _SelectedTJour = value;
                    RaisePropertyChanged("SelectedTJour");


                }
            }
        }

        private TypesJours _SelectedTJourListe;
        public TypesJours SelectedTJourListe
        {
            get
            {
                return _SelectedTJourListe;
            }
            set
            {
                if( value != _SelectedTJourListe )
                {
                    IsValid2 = false;
                    _SelectedTJourListe = value;
                    RaisePropertyChanged( "SelectedTJourListe" );


                }
            }
        }


        private DateTime _DisplayDateStart;

        public DateTime DisplayDateStart
        {
            get
            {
                return _DisplayDateStart;
            }
            set
            {
                if (value != _DisplayDateStart)
                {                    
                    _DisplayDateStart = value;

                    if (_DateDebut > _DateFin)
                    {

                        DisplayDateStart = _DateDebut;
                    }
                    else
                    {
                        DisplayDateStart = _DateFin;
                    }

                    RaisePropertyChanged("DisplayDateStart");

                }
            }
        }

        private DateTime _DisplayDateStartListe;
        public DateTime DisplayDateStartListe
        {
            get
            {
                return _DisplayDateStartListe;
            }
            set
            {
                if( value != _DisplayDateStartListe )
                {
                    _DisplayDateStartListe = value;

                    if( _DateDebutListe > _DateFinListe )
                    {

                        DisplayDateStartListe = _DateDebutListe;
                    }
                    else
                    {
                        DisplayDateStartListe = _DateFinListe;
                    }

                    RaisePropertyChanged( "DisplayDateStartListe" );

                }
            }
        }


        private DateTime _DateDebut;
        public DateTime DateDebut
        {
            get
            {
                
                    return _DateDebut;
            }
            set
            {                

                if (value != _DateDebut)
                {
                    IsValid = false;
                    _DateDebut = value;

                    if (_DateDebut != null)
                    {
                        //DateFin.IsEnabled = true;

                        if (_DateDebut > _DateFin)
                        {
                            _DateFin = _DateDebut;
                        }


                        RaisePropertyChanged(nameof(DateDebut));
                        RaisePropertyChanged(nameof(DateFin));
                    }
                }
            }
        }

        private DateTime _DateDebutListe;
        public DateTime DateDebutListe
        {
            get
            {

                return _DateDebutListe;
            }
            set
            {

                if( value != _DateDebutListe )
                {
                    IsValid2 = false;
                    _DateDebutListe = value;

                    if( _DateDebutListe != null )
                    {
                        //DateFin.IsEnabled = true;

                        if( _DateDebutListe > _DateFinListe )
                        {
                            _DateFinListe = _DateDebutListe;
                        }


                        RaisePropertyChanged( nameof( DateDebutListe ) );
                        RaisePropertyChanged( nameof( DateFinListe ) );
                    }
                }
            }
        }

        private DateTime _DateFin;
        public DateTime DateFin
        {
            get
            {                
                return _DateFin;
            }
            set
            {              

                if (value != _DateFin)
                {
                    IsValid = false;
                    _DateFin = value;

                    if (_DateDebut > _DateFin)
                    {
                        _DateFin = _DateDebut;
                    }

                    RaisePropertyChanged("DateFin");
                }
            }
        }

        private DateTime _DateFinListe;
        public DateTime DateFinListe
        {
            get
            {
                return _DateFinListe;
            }
            set
            {

                if( value != _DateFinListe )
                {
                    IsValid2 = false;
                    _DateFinListe = value;

                    if( _DateDebutListe > _DateFinListe )
                    {
                        _DateFinListe = _DateDebutListe;
                    }

                    RaisePropertyChanged( "DateFinListe" );
                }
            }
        }
        #endregion

        #region Constructeur
        public DemandeAbsenceViewModel( IDataService dataService, IDialogService dialogService )
        {
            _dataService = dataService;
            ParamGlobaux = _dataService.ParamGlobaux;
            _dialogService = dialogService;

            AideCommand = new RelayCommand( () => ClassUILibrary.Design.AfficheAide( "AideDemandeAbsence.pdf", oConnection ) );
            LoadedCommand = new RelayCommand( () => Initialize() );
            UnLoadedCommand = new RelayCommand( () => ExecuteUnLoaded() );
            QuitterCommand = new RelayCommand( () => Close = true );
            ImprimerCommand = new RelayCommand( () => ExecuteImprimer() );
            ImprimerCommand2 = new RelayCommand( () => ExecuteImprimer2() );
            ValiderCommand = new RelayCommand( async () => await ExecuteValider() );
            SupprimerCommand = new RelayCommand( async () => await ExecuteSupprimer() );
            ModifierCommand = new RelayCommand( async () => await ExecuteModifier() );
            CPACommand = new RelayCommand( () => ExecuteCPA() );


        }
        #endregion

        #region Relay Command       

        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnLoadedCommand { get; set; }
        public RelayCommand AideCommand { get; set; }
        public RelayCommand<AgentModel> SelectAgentCommand { get; set; }
        public RelayCommand ImprimerCommand { get; private set; }
        public RelayCommand ImprimerCommand2 { get; private set; }
        public RelayCommand ValiderCommand { get; private set; }
        public RelayCommand SupprimerCommand { get; private set; }
        public RelayCommand ModifierCommand { get; private set; }
        public RelayCommand QuitterCommand { get; private set; }
        public RelayCommand CPACommand { get; set; }

        #endregion


        #region Methodes
        private void ExecuteUnLoaded()
        {
            Close = false;
            SelectedAgent = null;
            SelectedAgentListe = null;
            SelectedTJour = null;
            SelectedDemande_Absence = null;
           
        }

        /// <summary>
        /// Mise à jour de la liste de demandes, lors d'un changement d'agent
        /// </summary>
        public async Task Actualisation()
        {
            ListeDemande_Absence = new ObservableCollection<Demande_Absence>( ( await _dataService.GetDemandeAbsenceAsync() ).Where( dm => dm.Matricule == SelectedAgentListe.Matricule ) );
        }
        #endregion

        #region Initialisation
        public async void Initialize()
        {
            string StSQL;
            StSQL = "";
            Commentaire_agent = "";
            Commentaire_agentListe = "";
            Initializing = true;
            Status = DemandeStatus.Toutes;

            IsValid = false;
            IsValid2 = false;
            

            //Initialisation de la date à la date du jour////

            DateDebut = new DateTime();
            DateDebut = DateTime.Today;

            DateFin = new DateTime();
            DateFin = DateTime.Today;

            DateDebutListe = new DateTime();
            DateDebutListe = DateTime.Today;

            DateFinListe = new DateTime();
            DateFinListe = DateTime.Today;

            //Initialisation des listes///////////

            _SelectedAgent = new AgentModel();
            _SelectedDemande_Absence = new Demande_Absence();
            _SelectedAgentListe = new AgentModel();
            _SelectedTJour = new TypesJours();

            EnableImprimer = true;
           
            var taskAgents = _dataService.GetAgentsContratsAsync();
            var taskTypeJour = _dataService.GetTypesJoursAsync();
            var taskDemandeAbsence = _dataService.GetDemandeAbsenceAsync();
            await Task.WhenAll(taskAgents, taskTypeJour, taskDemandeAbsence);


            _TousTypes = taskTypeJour.Result.ToList();
            _TousAgents = taskAgents.Result.ToList();
            _ListeDemande_Absence = new ObservableCollection<Demande_Absence>(taskDemandeAbsence.Result);


            Agents = new ObservableCollection<AgentModel>(_TousAgents.Distinct().OrderBy(a => a.Prenom).OrderBy(a => a.Nom));

            
            ListeTypeJour = new ObservableCollection<TypesJours>(_TousTypes.Where(tp => tp.Famille == "CONG"));

            ListeDemande_Absence = new ObservableCollection<Demande_Absence>(_ListeDemande_Absence.Where(dm => dm.Matricule == SelectedAgentListe.Matricule));
            //ListeDemande_Absence = new ObservableCollection<Demande_Absence>(_TtesDemande_Absence);

            //on initialise _selectedAgent avec l'agent connecté          
            foreach (var item in Agents)
            {
                if(item.Matricule == ParamGlobaux.Matricule)
                {
                    SelectedAgent = item;
                }
               
            }
                       

            //on initialise _selectedAgentListe avec l'agent connecté          
            foreach (var item in Agents)
            {
                if (item.Matricule == ParamGlobaux.Matricule)
                {
                    SelectedAgentListe = item;
                }

            }
            
            RaisePropertyChanged("EnableImprimer");
            Initializing = false;

            oConnection = new SqlConnection( _dataService.ParamGlobaux.ConnectionString );
        }
        #endregion


        #region Bt Imprimer
        private void ExecuteImprimer()
        {
            if (_IsValid == true)
            {

                EnableImprimer = true;

                if (_SelectedAgent.Nom != null)
                {




                    if (_SelectedTJour.Libelle != null)
                    {



                        if (DateDebut <= DateFin)
                        {
                            ApercuDemandeAbsenceViewModel ApercuDemandeAbsenceVM = new ApercuDemandeAbsenceViewModel(_dataService, _dialogService);

                            DemandeAbsenceNP demandeAbsenceNP = new DemandeAbsenceNP(SelectedAgent.Matricule, SelectedAgent.Nom, SelectedAgent.Prenom, DateDebut, DateFin, SelectedTJour.Libelle);

                            ObservableCollection<DemandeAbsenceNP> ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>();
                            ListeDemande_Absence.Add(demandeAbsenceNP);


                            ApercuDemandeAbsenceVM.ListeDemande_Absence = ListeDemande_Absence;

                            _dialogService.ShowDialog<ApercuDemandeAbsence>(this, ApercuDemandeAbsenceVM);
                        }
                        else
                        {
                            MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox(this,
                                "La date de début, doit être antérieur à la date de fin",
                                "Erreur",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox(this,
                           "Veuillez selectionner un Type de congés",
                           "Erreur",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error);
                    }
                }

                else
                {
                    MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox(this,
                        "Veuillez selectionner un agent",
                        "Erreur",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

            }
            else
            {

                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox(this,
                           "Veuillez valider votre demande",
                           "Erreur",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error);

            }
        }
        #endregion

        #region Bt CPA
        private void ExecuteCPA()
        {

            if (SelectedAgent != null)
            {
                _dataService.ParamGlobaux = ParamGlobaux;

                //on initialise le matricule des ParamGlobaux avec celui du selected agent
                _dataService.ParamGlobaux.StMatricule = SelectedAgent.Matricule;

                ListeCongésViewModel LCV = new ListeCongésViewModel(_dataService, _dialogService);

                _dialogService.ShowDialog<ListeCongésView>(this, LCV);
            }
            else
            {
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox(this,
                           "Veuillez selectionner un agent",
                           "Erreur",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error);
            }
           
            
        }
        #endregion

        #region Bt Imprimer2
        private void ExecuteImprimer2()
        {

            

                EnableImprimer = true;

                if (_SelectedAgentListe.Nom != null)
                {




                    if (SelectedDemande_Absence != null)
                    {



                       
                            ApercuDemandeAbsenceViewModel ApercuDemandeAbsenceVM = new ApercuDemandeAbsenceViewModel(_dataService, _dialogService);

                            //on met les données au format DemandeAbsenceNP et on les stocks dans une liste
                            DemandeAbsenceNP demandeAbsenceNP = new DemandeAbsenceNP(SelectedAgentListe.Matricule, SelectedAgentListe.Nom, SelectedAgentListe.Prenom, SelectedDemande_Absence.Date_debut, SelectedDemande_Absence.Date_fin, SelectedDemande_Absence.Type_Jour);
                            ObservableCollection<DemandeAbsenceNP> ListeDemande_Absence = new ObservableCollection<DemandeAbsenceNP>();
                            ListeDemande_Absence.Add(demandeAbsenceNP);


                            ApercuDemandeAbsenceVM.ListeDemande_Absence = ListeDemande_Absence;                            
                            

                            _dialogService.ShowDialog<ApercuDemandeAbsence>(this, ApercuDemandeAbsenceVM);
                        
                       
                    }
                    else
                    {
                        MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox(this,
                           "veuillez selectionner une demande d'absence",
                           "Erreur",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error);
                    }
                }

                else
                {
                    MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox(this,
                        "veuillez selectionner un agent",
                        "Erreur",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

            
            


        }
        #endregion

        #region Bt Valider
        private async Task ExecuteValider()
        {
            

            if (_SelectedAgent.Nom != null)
            {




                if (_SelectedTJour.Libelle != null)
                {



                    if (DateDebut<= DateFin)
                    {
                        IsValid = true;
                        Etat = "ENC";

                        try
                        {

                            using (ProgetEntities pg = new ProgetEntities( Model.GenerateEFConnectionString( ParamGlobaux.ConnectionString ) ) )
                            {
                                // recherche dans la BDD s'il existe déja une Demande identique
                                Demande_Absence existe = pg.Demande_Absence.Where(a => a.Matricule == _SelectedAgent.Matricule && a.Date_debut == _DateDebut && a.Date_fin == _DateFin).SingleOrDefault();

                                //recherche si il n’a pas de conflit avec d’autres demandes
                                Demande_Absence existe2 = pg.Demande_Absence.Where( a => a.Matricule == _SelectedAgent.Matricule && a.Etat != "REF" && (( DateDebut >= a.Date_debut  && DateDebut <= a.Date_fin ) 
                                || ( DateFin <= a.Date_fin && DateFin >= a.Date_debut ) || ( DateDebut < a.Date_debut && DateFin > a.Date_fin) ) ).SingleOrDefault();

                                //s'il n'existe pas d'objet similaire en base 
                                if (existe == null && existe2 == null)
                                {
                                    ObsDemandeAbsence = new DemandeAbsenceObservable(_SelectedAgent.Matricule, _DateDebut, _DateFin, _SelectedTJour.Libelle, _Commentaire_agent, Etat);
                                    pg.Demande_Absence.Add(DemandeAbsenceObservable.ConversionEnCodeRegime(ObsDemandeAbsence));

                                    // on sauvegarde les changements dans la BDD
                                    pg.SaveChanges();

                                    MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox(this,
                                    "Demande Validée",
                                    "Validation",
                                    MessageBoxButton.OK);

                                    
                                    //on actualise la liste des demandes
                                    await Actualisation();

                                    ListeDemande_Absence = new ObservableCollection<Demande_Absence>(ListeDemande_Absence.Where
                                   (dm => dm.Matricule == SelectedAgentListe.Matricule));



                                    //Initialisation des differents parametres////

                                    Commentaire_agent = "";
                                    DateDebut = new DateTime();
                                    DateDebut = DateTime.Today;

                                    DateFin = new DateTime();
                                    DateFin = DateTime.Today;

                                    SelectedTJour = null;
                                    SelectedTJour = new TypesJours();

                                }
                                else if(existe == null)
                                {
                                    MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox(this,
                                    "Cette demande contient des dates déjà utilisées dans une autre demande",                               
                                    "Erreur",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                                }
                                else
                                {
                                    MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox( this,
                                    "Cette demande est déjà existante",
                                    "Erreur",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation );
                                }

                            }
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Debug.WriteLine("DEBUG :Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Debug.WriteLine(" DEBUG:- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox(this,
                            "La date de début, doit être antérieur à la date de fin",
                            "Erreur",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation );
                    }
                }
                else
                {
                    MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox(this,
                       "veuillez selectionner un Type de congés",
                       "Erreur",
                       MessageBoxButton.OK,
                       MessageBoxImage.Exclamation );
                }
            }

            else
            {
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox(this,
                    "veuillez selectionner un agent",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
            }


        }

        #endregion

        #region Bt Modifier
        private async Task ExecuteModifier()
        {
            if( SelectedAgentListe.Nom != null )
            {

                if( SelectedDemande_Absence != null )
                {




                    if( SelectedTJourListe.Libelle != null )
                    {



                        if( DateDebutListe <= DateFinListe )
                        {

                            if( SelectedDemande_Absence.Etat == "ENC" )
                            {


                                IsValid = true;
                                Etat = "ENC";

                                try
                                {

                                    using( ProgetEntities pg = new ProgetEntities( Model.GenerateEFConnectionString( ParamGlobaux.ConnectionString ) ) )
                                    {
                                        // recherche dans la BDD s'il existe déja une Demande identique
                                        Demande_Absence demande = pg.Demande_Absence.SingleOrDefault( a => a.Matricule == SelectedDemande_Absence.Matricule && a.Date_debut == SelectedDemande_Absence.Date_debut && a.Date_fin == SelectedDemande_Absence.Date_fin );


                                        //si la demande existe bien en base, on la supprime
                                        if( demande != null)
                                        {

                                            pg.Demande_Absence.Remove( demande );

                                            // on sauvegarde les changements dans la BDD
                                            pg.SaveChanges();

                                            //on stock SelectedDemande_Absence dans une variable temp pour qu elle ne soit^pas effacée pdt l'actualisation
                                            Demande_Absence Demande_AbsenceTemp = new Demande_Absence();
                                            Demande_AbsenceTemp = SelectedDemande_Absence;

                                            //on actualise la liste des demandes
                                            await Actualisation();

                                            bool existe2 = pg.Demande_Absence.Any( a => a.Matricule == SelectedAgentListe.Matricule && a.Etat != "REF" && ( ( DateDebutListe >= a.Date_debut && DateDebutListe <= a.Date_fin ) || ( DateFinListe <= a.Date_fin && DateFinListe >= a.Date_debut ) || ( DateDebutListe < a.Date_debut && DateFinListe > a.Date_fin ) ) );

                                            

                                            if( existe2 == false )
                                            {
                                                ObsDemandeAbsence = new DemandeAbsenceObservable( SelectedAgent.Matricule, DateDebutListe, DateFinListe, SelectedTJourListe.Libelle, Commentaire_agentListe, Etat );
                                                pg.Demande_Absence.Add( DemandeAbsenceObservable.ConversionEnCodeRegime( ObsDemandeAbsence ) );

                                                // on sauvegarde les changements dans la BDD
                                                pg.SaveChanges();

                                                MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox( this,
                                               "Demande Modifiée",
                                               "Validation",
                                               MessageBoxButton.OK );
                                            }
                                            else
                                            {
                                                MessageBoxResult _dialogResult3 = _dialogService.ShowMessageBox( this,
                                                "Cette demande contient des dates déjà utilisées dans une autre demande",
                                                "Erreur",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Exclamation );

                                                

                                                //on enregistre en base la demande initiale
                                                ObsDemandeAbsence = new DemandeAbsenceObservable( SelectedAgent.Matricule, Demande_AbsenceTemp.Date_debut, Demande_AbsenceTemp.Date_fin, Demande_AbsenceTemp.Type_Jour, Demande_AbsenceTemp.Commentaire_agent, Etat );
                                                pg.Demande_Absence.Add( DemandeAbsenceObservable.ConversionEnCodeRegime( ObsDemandeAbsence ) );

                                                // on sauvegarde les changements dans la BDD
                                                pg.SaveChanges();

                                                //on actualise la liste des demandes
                                                await Actualisation();
                                            }

                                            


                                            //on actualise la liste des demandes
                                            await Actualisation();

                                            ListeDemande_Absence = new ObservableCollection<Demande_Absence>( ListeDemande_Absence.Where
                                           ( dm => dm.Matricule == SelectedAgentListe.Matricule ) );



                                            //Initialisation des differents parametres////

                                            Commentaire_agentListe = "";
                                            DateDebutListe = new DateTime();
                                            DateDebutListe = DateTime.Today;

                                            DateFinListe = new DateTime();
                                            DateFinListe = DateTime.Today;

                                            SelectedTJourListe = null;
                                            SelectedTJourListe = new TypesJours();

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
                                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                                 "Seule les demandes en cours peuvent être modifiées",
                                 "Erreur",
                                 MessageBoxButton.OK,
                                MessageBoxImage.Exclamation );
                            }
                        }
                        else
                        {
                            MessageBoxResult _dialogResult2 = _dialogService.ShowMessageBox( this,
                                "La date de début, doit être antérieur à la date de fin",
                                "Erreur",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation );
                        }
                    }
                    else
                    {
                        MessageBoxResult _dialogResult1 = _dialogService.ShowMessageBox( this,
                           "veuillez selectionner un Type de congés",
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

            else
            {
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                    "veuillez selectionner un agent",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
            }
        }
        #endregion

        #region Bt Supprimer
        private async Task ExecuteSupprimer()
        {
            if( SelectedAgentListe.Nom != null )
            {

                if( SelectedDemande_Absence != null )
                {
                    if( SelectedDemande_Absence.Etat == "ENC" )
                    {


                        IsValid = true;

                        try
                        {

                            using( ProgetEntities pg = new ProgetEntities( Model.GenerateEFConnectionString( ParamGlobaux.ConnectionString ) ) )
                            {
                                // recherche dans la BDD s'il existe déja une Demande identique
                                Demande_Absence existe = pg.Demande_Absence.Where( a => a.Matricule == SelectedDemande_Absence.Matricule && a.Date_debut == SelectedDemande_Absence.Date_debut && a.Date_fin == SelectedDemande_Absence.Date_fin ).SingleOrDefault();


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
                                    await Actualisation();

                                    ListeDemande_Absence = new ObservableCollection<Demande_Absence>( ListeDemande_Absence.Where
                                   ( dm => dm.Matricule == SelectedAgentListe.Matricule ) );



                                    //Initialisation des differents parametres////

                                    Commentaire_agent = "";
                                    DateDebutListe = new DateTime();
                                    DateDebutListe = DateTime.Today;

                                    DateFinListe = new DateTime();
                                    DateFinListe = DateTime.Today;

                                    SelectedTJourListe = null;
                                    SelectedTJourListe = new TypesJours();

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
                        MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                    "Seule les demandes en cours peuvent être supprimées",
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

            else
            {
                MessageBoxResult _dialogResult = _dialogService.ShowMessageBox( this,
                    "veuillez selectionner un agent",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation );
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
                 "Du " + _DateDebut.ToShortDateString() + " au " + _DateFin.ToShortDateString() +
                 "\n" + "\n" +
                "Cordialement,\n" +
                "\n" +
                "La Direction\n";
           
            _dialogService.ShowDialog<MailConfigView>(this, MailConfigVM);
        }
        #endregion
    }
}
