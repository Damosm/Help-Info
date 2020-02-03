using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace ClassGetMS.Models
{
    public struct HeuresJournee
    {
        public DateTime Jour;
        public TimeSpan W;
        public TimeSpan Abs;
    }

    public struct Parametre
    {
        public string Name;
        public string Value;

        public Parametre(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }

    public class DebutFin : ObservableObject
    {
        private DateTime _Debut;
        public DateTime Debut
        {
            get
            {
                return _Debut;
            }
            set
            {
                if (value != _Debut)
                {
                    _Debut = value;
                    RaisePropertyChanged("Debut");
                }
            }
        }

        private DateTime? _Fin;
        public DateTime? Fin
        {
            get
            {
                return _Fin;
            }
            set
            {
                if (value != _Fin)
                {
                    _Fin = value;
                    RaisePropertyChanged("Fin");
                }
            }
        }

        public DebutFin()
        {

        }

        public DebutFin(DateTime debut, DateTime? fin)
        {
            Debut = debut;
            Fin = fin;
        }
    }

    public class SelectionServicesSections : ObservableObject
    {
        private ObservableCollection<ServiceSectionModel> _Services;
        public ObservableCollection<ServiceSectionModel> Services
        {
            get
            {
                return _Services;
            }
            set
            {
                if (value != _Services)
                {
                    _Services = value;
                    RaisePropertyChanged("Services");
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
                if (value != _Sections)
                {
                    _Sections = value;
                    RaisePropertyChanged("Sections");
                }
            }
        }

        public SelectionServicesSections(ObservableCollection<ServiceSectionModel> Services, ObservableCollection<ServiceSectionModel> Sections)
        {
            this._Services = Services;
            this.Sections = Sections;
        }
    }

    public class PeriodeModulation : Periode
    {
        private string _TypeContrat;
        public string TypeContrat
        {
            get
            {
                return _TypeContrat;
            }
            set
            {
                if (value != _TypeContrat)
                {
                    _TypeContrat = value;
                    RaisePropertyChanged(nameof(TypeContrat));
                }
            }
        }
        
        private string _IdContrat;
        public string IdContrat
        {
            get
            {
                return _IdContrat;
            }
            set
            {
                if (value != _IdContrat)
                {
                    _IdContrat = value;
                    RaisePropertyChanged(nameof(IdContrat));
                }
            }
        }

        private double _TempsBase;
        public double TempsBase
        {
            get
            {
                return _TempsBase;
            }
            set
            {
                if (value != _TempsBase)
                {
                    _TempsBase = value;
                    RaisePropertyChanged(nameof(TempsBase));
                }
            }
        }

        private string _Regime;
        public string Regime
        {
            get
            {
                return _Regime;
            }
            set
            {
                if (value != _Regime)
                {
                    _Regime = value;
                    RaisePropertyChanged(nameof(Regime));
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
                    RaisePropertyChanged(nameof(IDEtablissement));
                }
            }
        }

        private bool _EnCours;
        public bool EnCours
        {
            get
            {
                return _EnCours;
            }
            set
            {
                if (value != _EnCours)
                {
                    _EnCours = value;
                    RaisePropertyChanged(nameof(EnCours));
                }
            }
        }

        public PeriodeModulation()
        {

        }

        public PeriodeModulation(PeriodeModulation perMod)
        {
            this._Debut = perMod.Debut;
            this._Fin = perMod.Fin;
            this._TypeContrat = String.Copy(perMod.TypeContrat);
            this._IdContrat = String.Copy(perMod.IdContrat);
            this._TempsBase = perMod.TempsBase;
            this._Regime = String.Copy(perMod.Regime);
            this._IDEtablissement = String.Copy(perMod.IDEtablissement);
            this._EnCours = perMod.EnCours;
        }
    }

    public class Periode : ObservableObject
    {
        protected DateTime _Debut;
        public DateTime Debut
        {
            get
            {
                return _Debut;
            }
            set
            {
                if (value != _Debut)
                {
                    _Debut = value;
                    RaisePropertyChanged(nameof(Debut));
                }
            }
        }


        protected DateTime _Fin;
        public DateTime Fin
        {
            get
            {
                return _Fin;
            }
            set
            {
                if (value != _Fin)
                {
                    _Fin = value;
                    RaisePropertyChanged(nameof(Fin));
                }
            }
        }


        protected TypePeriode _TypePeriode;
        public TypePeriode TypePeriode
        {
            get
            {
                return _TypePeriode;
            }
            set
            {
                if (value != _TypePeriode)
                {
                    _TypePeriode = value;
                    RaisePropertyChanged(nameof(TypePeriode));
                }
            }
        }

        public Periode() : base()
        { }

        public Periode(DateTime debut, DateTime fin, TypePeriode typePeriode) : base()
        {
            _Debut = debut;
            _Fin = fin;
            _TypePeriode = typePeriode;
        }

        public Periode(Periode periode) : base()
        {
            this.Debut = periode.Debut;
            this.Fin = periode.Fin;
            this.TypePeriode = periode.TypePeriode;
        }
    }

    public class Congé : Periode
    {
        private decimal _DroitCPA;
        public decimal DroitCPA
        {
            get
            {
                return _DroitCPA;
            }
            set
            {
                if (value != _DroitCPA)
                {
                    _DroitCPA = value;
                    RaisePropertyChanged(nameof(DroitCPA));
                    RaisePropertyChanged(nameof(DroitText));
                }
            }
        }

        private int _CPAPris;
        public int CPAPris
        {
            get
            {
                return _CPAPris;
            }
            set
            {
                if (value != _CPAPris)
                {
                    _CPAPris = value;
                    RaisePropertyChanged(nameof(CPAPris));
                    RaisePropertyChanged(nameof(PrisText));
                }
            }
        }

        private decimal _DroitCA;
        public decimal DroitCA
        {
            get
            {
                return _DroitCA;
            }
            set
            {
                if (value != _DroitCA)
                {
                    _DroitCA = value;
                    RaisePropertyChanged(nameof(DroitCA));
                    RaisePropertyChanged(nameof(DroitText));
                }
            }
        }
        
        private int _CAPris;
        public int CAPris
        {
            get
            {
                return _CAPris;
            }
            set
            {
                if (value != _CAPris)
                {
                    _CAPris = value;
                    RaisePropertyChanged(nameof(CAPris));
                    RaisePropertyChanged(nameof(PrisText));
                }
            }
        }

        public string DroitText
        {
            get
            {
                bool CPA = _DroitCPA != 0;
                bool CA = _DroitCA != 0;
                return (_DroitCPA + _DroitCA).ToString() + (CPA || CA ? " ("  : "") + (CPA ? + _DroitCPA + " CPA" : "") + (CPA && CA ? " + " : "") + (CA ?_DroitCA + " CA" : "") + (CPA || CA ? ")" : "");
            }
        }

        public string PrisText
        {
            get
            {
                bool CPA = _CPAPris != 0;
                bool CA = _CAPris != 0;
                return (_CPAPris + _CAPris).ToString() + (CPA || CA ? " (" : "") + (CPA ?  _CPAPris + " CPA" : "") + (CPA && CA ? " + " : "") + (CA ? _CAPris + " CA" : "") + (CPA || CA ? ")" : "");
            }
        }

        private decimal _ReportConge;
        public decimal ReportConge
        {
            get
            {
                return _ReportConge;
            }
            set
            {
                if (value != _ReportConge)
                {
                    _ReportConge = value;
                    Modifié = true;
                    RaisePropertyChanged(nameof(ReportConge));
                    RaisePropertyChanged(nameof(Reste));
                    RaisePropertyChanged(nameof(ResteText));
                }
            }
        }
        
        private string _CommentaireReport;
        public string CommentaireReport
        {
            get
            {
                return _CommentaireReport;
            }
            set
            {
                if (value != _CommentaireReport)
                {
                    _CommentaireReport = value;
                    RaisePropertyChanged(nameof(CommentaireReport));
                }
            }
        }
        
        private int _ResteCPA;
        public int ResteCPA
        {
            get
            {
                return _ResteCPA;
            }
            set
            {
                if (value != _ResteCPA)
                {
                    _ResteCPA = value;
                    //RaisePropertyChanged(nameof(ResteCPA));
                    RaisePropertyChanged(nameof(Reste));
                    RaisePropertyChanged(nameof(ResteText));
                }
            }
        }
        
        private int _ResteCA;
        public int ResteCA
        {
            get
            {
                return _ResteCA;
            }
            set
            {
                if (value != _ResteCA)
                {
                    _ResteCA = value;
                    //RaisePropertyChanged(nameof(ResteCA));
                    RaisePropertyChanged(nameof(Reste));
                    RaisePropertyChanged(nameof(ResteText));
                }
            }
        }

        public decimal Reste
        {
            get
            {
                return _ResteCPA + _ResteCA + _ReportConge;
            }
        }

        public string ResteText
        {
            get
            {
                bool CPA = _ResteCPA != 0;
                bool CA = _ResteCA != 0;
                bool Detail = CPA || CA;
                bool Tout = CPA && CA;
                return (_ResteCPA + _ResteCA + ReportConge).ToString() + (Detail ? " (" + (CPA ? _ResteCPA.ToString () + " CPA" : "") + (Tout ? " + " : "") + (CA ? _ResteCA.ToString() + " CA" : "") + ")": "");
            }
        }

        private bool _ShowDiagSign;
        public bool ShowDiagSign
        {
            get
            {
                return _ShowDiagSign;
            }
            set
            {
                if (value != _ShowDiagSign)
                {
                    _ShowDiagSign = value;
                    RaisePropertyChanged(nameof(ShowDiagSign));
                }
            }
        }


        private bool _ShowHorSign;
        public bool ShowHorSign
        {
            get
            {
                return _ShowHorSign;
            }
            set
            {
                if (value != _ShowHorSign)
                {
                    _ShowHorSign = value;
                    RaisePropertyChanged(nameof(ShowHorSign));
                }
            }
        }

        public bool Modifié;

        public Congé(Periode periode) : base(periode)
        {
        }
    }

    public class CESS : Periode
    {
        private decimal _DroitCESS;
        public decimal DroitCESS
        {
            get
            {
                return _DroitCESS;
            }
            set
            {
                if (value != _DroitCESS)
                {
                    _DroitCESS = value;
                    RaisePropertyChanged(nameof(DroitCESS));
                }
            }
        }

        private int _CESSPris;
        public int CESSPris
        {
            get
            {
                return _CESSPris;
            }
            set
            {
                if (value != _CESSPris)
                {
                    _CESSPris = value;
                    RaisePropertyChanged(nameof(CESSPris));
                }
            }
        }

        private int _ReportCESS;
        public int ReportCESS
        {
            get
            {
                return _ReportCESS;
            }
            set
            {
                if (value != _ReportCESS)
                {
                    _ReportCESS = value;
                    Modifié = true;
                    RaisePropertyChanged(nameof(ReportCESS));
                    RaisePropertyChanged(nameof(ResteCESS));
                }
            }
        }

        private string _CommentaireReport;
        public string CommentaireReport
        {
            get
            {
                return _CommentaireReport;
            }
            set
            {
                if (value != _CommentaireReport)
                {
                    _CommentaireReport = value;
                    RaisePropertyChanged(nameof(CommentaireReport));
                }
            }
        }

        private int _ResteCESS;
        public int ResteCESS
        {
            get
            {
                return _ResteCESS;
            }
            set
            {
                if (value != _ResteCESS)
                {
                    _ResteCESS = value;
                    RaisePropertyChanged(nameof(ResteCESS));
                }
            }
        }

        private bool _ShowDiagSign;
        public bool ShowDiagSign
        {
            get
            {
                return _ShowDiagSign;
            }
            set
            {
                if (value != _ShowDiagSign)
                {
                    _ShowDiagSign = value;
                    RaisePropertyChanged(nameof(ShowDiagSign));
                }
            }
        }


        private bool _ShowHorSign;
        public bool ShowHorSign
        {
            get
            {
                return _ShowHorSign;
            }
            set
            {
                if (value != _ShowHorSign)
                {
                    _ShowHorSign = value;
                    RaisePropertyChanged(nameof(ShowHorSign));
                }
            }
        }

        public bool Modifié;

        public CESS(Periode periode) : base(periode)
        {
        }
    }
    /// <summary>
    /// Type de période de Modulation
    /// </summary>
    public enum TypePeriode
    {
        Contrat, Avenant, PeriodeEtb
    }

    public enum TypeDate
    {
        Debut, Fin
    }

    public enum TypeRCF
    {
        Module, Cycle
    }

    public enum TypeRepas
    {
        P, G, A
    }

    public enum MinutesCentiemes
    {
        Minutes, Centiemes
    }

    [Flags]
    public enum SelectionPrise
    {
        Aucune = 0x0,
        Prise1 = 0x1,
        Prise2 = 0x2,
        Prise3 = 0x4,
        PriseDuree = 0x8
    }



    public enum MessageType
    {
        Info,
        Warning,
        Error
    }

    public class DatePeriode : IEquatable<DatePeriode>, IComparable<DatePeriode>
    {
        public DateTime? Date;
        public TypeDate TypeDate;
        public TypePeriode TypePeriode;

        #region IEquatable<DatePeriode> Member

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
            return base.GetHashCode();
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
            return Equals(obj as DatePeriode);
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
        public bool Equals(DatePeriode other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if ((this.Date == other.Date) && (this.TypeDate == other.TypeDate))
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
        public static bool operator ==(DatePeriode left, DatePeriode right)
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
        public static bool operator !=(DatePeriode left, DatePeriode right)
        {
            if (ReferenceEquals(left, right))
                return false;

            if (ReferenceEquals(left, null))
                return true;

            return !left.Equals(right);
        }

        #endregion

        #region IComparable<DatePeriode>

        public int CompareTo(DatePeriode datePeriode)
        {
            if ((this.Date > datePeriode.Date) || ((this.Date == null) && (datePeriode != null)))
                return 1;
            else if ((this.Date < datePeriode.Date) || ((this.Date != null) && (datePeriode == null)))
                return -1;
            else if (this.Date == datePeriode.Date)
                if ((this.TypeDate == Models.TypeDate.Fin) && (datePeriode.TypeDate == Models.TypeDate.Debut))
                    return 1;
                else if ((this.TypeDate == Models.TypeDate.Debut) && (datePeriode.TypeDate == Models.TypeDate.Fin))
                    return -1;
            return 0;
        }

        // Define the is greater than operator.
        public static bool operator >(DatePeriode operand1, DatePeriode operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        // Define the is less than operator.
        public static bool operator <(DatePeriode operand1, DatePeriode operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(DatePeriode operand1, DatePeriode operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(DatePeriode operand1, DatePeriode operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        #endregion

        public DatePeriode()
        {
        }

        public DatePeriode(DateTime? date, TypeDate typeDate, TypePeriode typePeriode)
        {
            this.Date = date;
            this.TypeDate = typeDate;
            this.TypePeriode = typePeriode;
        }
    }

    public struct PasswordData
    {
        public string PasswordHash;
        public string PasswordSalt;
        public DateTime? PasswordResetDate;
    }

    public enum EnumGenre: int
    {
        ReposSansIncidence = 0,
        Chomage = 1,
        RCRemp = 11,
        RCO = 12,
        RCCoupure = 13,
        RCCEmploye = 131,
        RCCSalarie = 132,
        RCFerie = 14,
        RCNuit = 15,
        CPA = 20,
        CESS = 21,
        CANC = 22,
        CSupp = 23,
        CEXP = 24,
        CongeSansSolde = 25,
        CongeFormationES = 26,
        RTT = 30,
        JourFerie = 31,
        ReposHebdo = 32,
        CongeFormation = 40,
        CEP = 41,
        Travail = 50,
        Delegation = 51,
        DelegUCEDP = 52,
        DelegAutreOuMandat = 53,
        DelegCE = 54,
        DelegCHSCT = 55,
        DelegPersonnel = 56,
        DelegSyndic = 57,
        MandatSyndic = 58,
        ComiteHS = 59,
        CongeNonPaye = 60,
        AbsPaye = 61,
        Maladie = 62,
        Maternite = 63,
        Paternite = 64,
        AT = 65,
        EvtFamilial = 66,
        CongeEMC = 67,
        CongeEML = 68,
        Formation = 70,
        Greve = 75,
        Astreinte = 80,
        JNS = 81,
        ReducMat = 82,
        Repos = 90,
        Equivalence = 99
    }
}
