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
    public class ApercuDemandeAbsenceViewModel : ViewModelBase, IModalDialogViewModel
    {
        #region Variables
        private IDataService _dataService;
        private IDialogService _dialogService;
        private StrucParam ParamGlobaux;
        private SqlConnection oConnection;
        private int _progressMax;
        private IProgress<ProgressMessage> _progress;
        //public AgentModel _SelectedAgent;
        //public DateTime _DateDebut;
        //public DateTime _DateFin;
        //public TypesJours _SelectedTJour;
        //public Demande_Absence _SelectedDemande_Absence;
        public DemandeAbsenceNP demandeAbsenceNP;
        public ObservableCollection<DemandeAbsenceNP> ListeDemande_Absence;
        public bool? DialogResult
        {
            get
            {
                return true;
            }
        }
        private bool _MailEnabled;
        public bool MailEnabled
        {
            get
            {
                return _MailEnabled;
            }
            set
            {
                if (value != _MailEnabled)
                {
                    _MailEnabled = value;
                    RaisePropertyChanged("MailEnabled");
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
        private ReportClass _ReportSource;
        public ReportClass ReportSource
        {
            get
            {
                return _ReportSource;
            }
            set
            {
                if (value != _ReportSource)
                {
                    _ReportSource = value;
                    RaisePropertyChanged("ReportSource");
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
                if (value != _ParameterFieldInfo)
                {
                    _ParameterFieldInfo = value;
                    RaisePropertyChanged("ParameterFieldInfo");
                }
            }
        }
        #endregion

        public ApercuDemandeAbsenceViewModel(IDataService dataService, IDialogService dialogService)
        {

            _dataService = dataService;
            ParamGlobaux = _dataService.ParamGlobaux;

            this._dataService = dataService;
            this._dialogService = dialogService;
            LoadedCommand = new RelayCommand(async () => await Initialize());
            UnLoadedCommand = new RelayCommand(() => Reset());
            MailCommand = new RelayCommand(() => SendMail());
            //MailEnabled = new RelayCommand(() => EnabledMail());

        }

        
        #region Initialisation
        private async Task Initialize()
        {
            LoadingViewModel lvm = new LoadingViewModel();

            _progressMax = 0;
            _progress = new Progress<ProgressMessage>((msg) => { Messenger.Default.Send<ProgressMessage>(msg); });

            Task apercuTask = CreationApercu(_progress);

            if(ListeDemande_Absence.Count != 1)
            {
                MailEnabled = false;
            }
            else
            {
                MailEnabled = true;
            }

            _dialogService.ShowDialog<LoadingDialog>(this, lvm);

            await apercuTask;
        }

        private void Reset()
        {
            
            _ReportSource = null;
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
                if (value != _LoadedCommand)
                {
                    _LoadedCommand = value;
                    RaisePropertyChanged("LoadedCommand");
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
                if (value != _UnLoadedCommand)
                {
                    _UnLoadedCommand = value;
                    RaisePropertyChanged("UnLoadedCommand");
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
                if (value != _MailCommand)
                {
                    _MailCommand = value;
                    RaisePropertyChanged("MailCommand");
                }
            }
        }

       

        #endregion

        #region Creation Apercu
        private async Task CreationApercu(IProgress<ProgressMessage> progress)
        {
            
            DataRow DtRow;
            //string StSQL;
            //StSQL = "";
            //DataSet DsTemp = new DataSet();

            
            _ReportSource = new CRDemandeAbsence();

            DsDemandeAbsence DsDemandeAbsence = new DsDemandeAbsence();

            ////////////////////////Initialisation barre de progression//////////////////////////
            _progressMax = (ListeDemande_Absence.Count);
            int _progressValue = 0;

            ////////remplissage du DsBordereauDeclaratif///////////////

            foreach (var item in ListeDemande_Absence)
            {
                DtRow = DsDemandeAbsence.Tables["Demande_Absence"].NewRow();

                DtRow[0] = item.Matricule;
                DtRow[1] = item.Nom;
                DtRow[2] = item.Prenom;
                DtRow[3] = item.Date_debut;
                DtRow[4] = item.Date_fin;
                DtRow[5] = item.Type_Jour;

                DsDemandeAbsence.Tables["Demande_Absence"].Rows.Add(DtRow);

                _progressValue++;
                _progress.Report(new ProgressMessage((_progressValue * 100) / _progressMax, "Création de l'aperçu...", false));
            }

            

            _ReportSource.SetDataSource(DsDemandeAbsence);



            //oConnection = ClassLibraryProget.DataBase.OpenSqlServer(ParamGlobaux.ConnectionString);

            ///////////Nom Etablissement/////////////////////////////
            string NomEtablissement;

            NomEtablissement = ParamGlobaux.Etablissement.ToString();

            
            TextObject Etablissement;
            if (_ReportSource.ReportDefinition.ReportObjects["Text1"] != null)
            {                
                Etablissement = (TextObject)_ReportSource.ReportDefinition.ReportObjects["Text1"];
                Etablissement.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(NomEtablissement.ToString());
            }

            RaisePropertyChanged("ReportSource");

            _progress.Report(new ProgressMessage(100, "Finished", true));
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
            demandeAbsenceNP = new DemandeAbsenceNP();

            foreach (var item in ListeDemande_Absence)
            {
                demandeAbsenceNP.Nom = item.Nom;
                demandeAbsenceNP.Prenom = item.Prenom;
                demandeAbsenceNP.Date_debut = item.Date_debut;
                demandeAbsenceNP.Date_fin = item.Date_fin;
                demandeAbsenceNP.Type_Jour = item.Type_Jour;

            }

            MailConfigViewModel MailConfigVM = new MailConfigViewModel(_dataService);

            MailConfigVM.Subject = "Nouvelle demande d'absense ";

            MailConfigVM.MessageText = "Bonjour,\n" +
                "\n" +
                "Nous vous informons, de la nouvelle demande d'absense de :" +
                "\n" + "\n" +
                demandeAbsenceNP.Nom + " " + demandeAbsenceNP.Prenom + ", de type : " + demandeAbsenceNP.Type_Jour +
                 "\n" + "\n" +
                "Du " + demandeAbsenceNP.Date_debut.ToShortDateString() + " au " + demandeAbsenceNP.Date_fin.ToShortDateString() +
                 "\n" + "\n" +
                "Cordialement,\n" +
                "\n" +
                "La Direction\n";

            _dialogService.ShowDialog<MailConfigView>(this, MailConfigVM);
        }
        #endregion
    }

}
