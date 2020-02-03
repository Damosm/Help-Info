using GalaSoft.MvvmLight;
using System;

namespace ClassGetMS.Models
{
    public class ServiceSectionModel : ObservableObject
    {
        private string _ID;
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }

        private string _Libelle;
        public string Libelle
        {
            get
            {
                return _Libelle;
            }
            set
            {
                if (value != _Libelle)
                {
                    _Libelle = value;
                    RaisePropertyChanged("Libelle");
                }
            }
        }

        private bool _Selected;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (value != _Selected)
                {
                    _Selected = value;
                    RaisePropertyChanged("Selected");
                    RaisePropertyChanged("CanValidate");
                }
            }
        }
    }

    public class Emploi : ObservableObject
    {

        private string _IDEmploi;
        public string IDEmploi
        {
            get
            {
                return _IDEmploi;
            }
            set
            {
                if (value != _IDEmploi)
                {
                    _IDEmploi = value;
                    RaisePropertyChanged("IDEmploi");
                }
            }
        }


        private string _Libelle;
        public string Libelle
        {
            get
            {
                return _Libelle;
            }
            set
            {
                if (value != _Libelle)
                {
                    _Libelle = value;
                    RaisePropertyChanged("Libelle");
                }
            }
        }


        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }


        private bool _Selected;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (value != _Selected)
                {
                    _Selected = value;
                    RaisePropertyChanged("Selected");
                }
            }
        }
    }
    
    public class StatutModel : ObservableObject
    {
        private string _IDStatut;
        public string IDStatut
        {
            get
            {
                return _IDStatut;
            }
            set
            {
                if (value != _IDStatut)
                {
                    _IDStatut = value;
                    RaisePropertyChanged("IDStatut");
                }
            }
        }

        private string _Nom;
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                if (value != _Nom)
                {
                    _Nom = value;
                    RaisePropertyChanged("Nom");
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
                    _DateDebut = value;
                    RaisePropertyChanged("DateDebut");
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
                    _DateFin = value;
                    RaisePropertyChanged("DateFin");
                }
            }
        }

        private bool _Durée_limitée;
        public bool Durée_limitée
        {
            get
            {
                return _Durée_limitée;
            }
            set
            {
                if (value != _Durée_limitée)
                {
                    _Durée_limitée = value;
                    RaisePropertyChanged("Durée_limitée");
                }
            }
        }

        private Nullable<int> _Couleur;
        public Nullable<int> Couleur
        {
            get
            {
                return _Couleur;
            }
            set
            {
                if (value != _Couleur)
                {
                    _Couleur = value;
                    RaisePropertyChanged("Couleur");
                }
            }
        }

        private bool _Selected;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (value != _Selected)
                {
                    _Selected = value;
                    RaisePropertyChanged("Selected");
                }
            }
        }
    }




    public class AgentModel : ObservableObject, IEquatable<AgentModel>
    {
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
                    RaisePropertyChanged("Matricule");
                }
            }
        }
        
        private string _Mail;
        public string Mail
        {
            get
            {
                return _Mail;
            }
            set
            {
                if (value != _Mail)
                {
                    _Mail = value;
                    RaisePropertyChanged("Mail");
                }
            }
        }

        private string _Prenom;
        public string Prenom
        {
            get
            {
                return _Prenom;
            }
            set
            {
                if (value != _Prenom)
                {
                    _Prenom = value;
                    RaisePropertyChanged("Prenom");
                }
            }
        }


        private string _Nom;
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                if (value != _Nom)
                {
                    _Nom = value;
                    RaisePropertyChanged("Nom");
                }
            }
        }

        public string NomMatricule
        {
            get
            {
                return _Nom + " " + _Prenom + " (" + _Matricule + ")";
            }
        }

        private string _PasswordSalt;
        public string PasswordSalt
        {
            get
            {
                return _PasswordSalt;
            }
            set
            {
                if (value != _PasswordSalt)
                {
                    _PasswordSalt = value;
                    RaisePropertyChanged("PasswordSalt");
                }
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }


        private DateTime? _PasswordResetDate;
        public DateTime? PasswordResetDate
        {
            get
            {
                return _PasswordResetDate;
            }
            set
            {
                if (value != _PasswordResetDate)
                {
                    _PasswordResetDate = value;
                    RaisePropertyChanged("PasswordResetDate");
                }
            }
        }

        private bool _Gestionnaire;
        public bool Gestionnaire
        {
            get
            {
                return _Gestionnaire;
            }
            set
            {
                if( value != _Gestionnaire )
                {
                    _Gestionnaire = value;
                    RaisePropertyChanged( "Gestionnaire" );
                }
            }
        }


        private string _IDEtablissement;
        public string IDEtablissement
        {
            get
            {
                return _IDEtablissement;
            }
            set
            {
                if (value != _IDEtablissement)
                {
                    _IDEtablissement = value;
                    RaisePropertyChanged("IDEtablissement");
                }
            }
        }

        private string _IDService;
        public string IDService
        {
            get
            {
                return _IDService;
            }
            set
            {
                if (value != _IDService)
                {
                    _IDService = value;
                    RaisePropertyChanged("IDService");
                }
            }
        }


        private string _IDSection;
        public string IDSection
        {
            get
            {
                return _IDSection;
            }
            set
            {
                if (value != _IDSection)
                {
                    _IDSection = value;
                    RaisePropertyChanged("IDSection");
                }
            }
        }


        private string _IDEmploi;
        public string IDEmploi
        {
            get
            {
                return _IDEmploi;
            }
            set
            {
                if (value != _IDEmploi)
                {
                    _IDEmploi = value;
                    RaisePropertyChanged("IDEmploi");
                }
            }
        }


        private string _IDFiliere;
        public string IDFiliere
        {
            get
            {
                return _IDFiliere;
            }
            set
            {
                if (value != _IDFiliere)
                {
                    _IDFiliere = value;
                    RaisePropertyChanged("IDFiliere");
                }
            }
        }


        private string _Typecontrat;
        public string Typecontrat
        {
            get
            {
                return _Typecontrat;
            }
            set
            {
                if (value != _Typecontrat)
                {
                    _Typecontrat = value;
                    RaisePropertyChanged("Typecontrat");
                }
            }
        }


        private DateTime _DebutContrat;
        public DateTime DebutContrat
        {
            get
            {
                return _DebutContrat;
            }
            set
            {
                if (value != _DebutContrat)
                {
                    _DebutContrat = value;
                    RaisePropertyChanged("DebutContrat");
                }
            }
        }


        private DateTime? _FinContrat;
        public DateTime? FinContrat
        {
            get
            {
                return _FinContrat;
            }
            set
            {
                if (value != _FinContrat)
                {
                    _FinContrat = value;
                    RaisePropertyChanged("FinContrat");
                }
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
                if (value != _IsSelected)
                {
                    _IsSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        public AgentModel()
        { }

        public AgentModel(string Matricule, string Prenom, string Nom)
        {
            _Matricule = Matricule;
            _Prenom = Prenom;
            _Nom = Nom;
        }

        #region IEquatable<AgentModel> Member

        /// <summary>
        /// Generates a hash code for this object.
        /// </summary>
        /// 
        /// <returns>The hash code of this object.</returns>
        /// 
        /// <remarks>
        /// </remarks>
        /// 
        public override int GetHashCode()
        {
            // TODO: Implement your code here:
            return (_Matricule + _Prenom + _Nom).GetHashCode();
        }

        /// <summary>
        /// Checks whether this object is equal to the specified one.
        /// </summary>
        /// 
        /// <param name="obj">The object to compare with.</param>
        /// 
        /// <returns><c>true</c> if this object is equal to <paramref name="obj"/>.</returns>
        /// 
        /// <remarks>
        /// This object is equal to <paramref name="obj"/> if <paramref name="obj"/> is not
        /// <c>null</c> and is of the same type as this object and ...
        /// </remarks>
        /// 
        public override bool Equals(object obj)
        {
            return Equals(obj as AgentModel);
        }

        /// <summary>
        /// Checks whether this object is equal to the specified one.
        /// </summary>
        /// 
        /// <param name="other">The object to compare with.</param>
        /// 
        /// <returns><c>true</c> if this object is equal to <paramref name="other"/>.</returns>
        /// 
        /// <remarks>
        /// This object is equal to <paramref name="other"/> if <paramref name="other"/>
        /// is not <c>null</c> and ...
        /// </remarks>
        /// 
        public bool Equals(AgentModel other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if ((this._Matricule == other._Matricule) && (this._Prenom == other._Prenom) && (this._Nom == other._Nom))
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether the specified objects are equal.
        /// </summary>
        /// 
        /// <param name="left">The object to compare with.</param>
        /// <param name="right">The object to compare.</param>
        /// 
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>.</returns>
        /// 
        /// <remarks>
        /// The two objects are equal if both are <c>null</c> or ...
        /// </remarks>
        /// 
        public static bool operator ==(AgentModel left, AgentModel right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (ReferenceEquals(left, null))
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Checks whether the specified objects are not equal.
        /// </summary>
        /// 
        /// <param name="left">The object to compare with.</param>
        /// <param name="right">The object to compare.</param>
        /// 
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>.</returns>
        /// 
        /// <remarks>
        /// The two objects are not equal if only one of them is <c>null</c> or ...
        /// </remarks>
        /// 
        public static bool operator !=(AgentModel left, AgentModel right)
        {
            if (ReferenceEquals(left, right))
                return false;

            if (ReferenceEquals(left, null))
                return true;

            return !left.Equals(right);
        }

        #endregion
    }

    public class ContratModel : Contrat
    {
        public ContratModel(Contrat contrat)
        {
            this.Num_Contrat = contrat.Num_Contrat; 
            this.Avenant = contrat.Avenant; 
            this.Type = contrat.Type; 
            this.Date_début = contrat.Date_début; 
            this.Date_Fin = contrat.Date_Fin; 
            this.Temps_Base = contrat.Temps_Base; 
            this.IDRégime = contrat.IDRégime; 
            this.Régime_Mod = contrat.Régime_Mod; 
            this.Base_contractuelle = contrat.Base_contractuelle; 
            this.RTT = contrat.RTT; 
            this.travail_de_nuit = contrat.travail_de_nuit; 
            this.Règle_équivalence = contrat.Règle_équivalence; 
            this.MultiEtablissement = contrat.MultiEtablissement; 
            this.Matricule_agent_remplace = contrat.Matricule_agent_remplace; 
            this.Motif_remplacement = contrat.Motif_remplacement; 
            this.Convention_collective = contrat.Convention_collective; 
            this.Num_contrat_base = contrat.Num_contrat_base; 
            this.Matricule = contrat.Matricule; 
            this.IDetablissement = contrat.IDetablissement; 
            this.Report_CPA = contrat.Report_CPA; 
            this.Report_Ancienneté = contrat.Report_Ancienneté; 
        }
    }
}
