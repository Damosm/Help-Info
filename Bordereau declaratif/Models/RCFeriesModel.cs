using GalaSoft.MvvmLight;
using System;

namespace ClassGetMS.Models
{

    public class JourFerieW: ObservableObject, IComparable<JourFerieW>
    {
        protected DateTime _Date;
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if (value != _Date)
                {
                    _Date = value;
                    RaisePropertyChanged("Date");
                }
            }
        }


        /// <summary>
        /// Heures effectivement travaillées
        /// </summary>
        protected TimeSpan _NbHeuresW;
        public TimeSpan NbHeuresW
        {
            get
            {
                return _NbHeuresW;
            }
            set
            {
                if (value != _NbHeuresW)
                {
                    _NbHeuresW = value;
                    RaisePropertyChanged("NbHeures");
                }
            }
        }

        protected string _TypeJour;
        public string TypeJour
        {
            get
            {
                return _TypeJour;
            }
            set
            {
                if (value != _TypeJour)
                {
                    _TypeJour = value;
                    RaisePropertyChanged("TypeJour");
                }
            }
        }

        protected string _Description;
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

        public JourFerieW()
        { }

        public JourFerieW(JourFerieW jfw)
        {
            Date = jfw.Date;
            NbHeuresW = jfw.NbHeuresW;
            TypeJour = jfw.TypeJour;
            Description = jfw.Description;
        }

        public JourFerieW(DateTime Date, string Description)
        {
            this._Date = Date;
            this._Description = Description;
        }

        public JourFerieW(DateTime Date, TimeSpan NbHeures, string TypeJour)
        {
            this._Date = Date;
            this._NbHeuresW = NbHeures;
            this._TypeJour = TypeJour;
        }

        #region IComparable<JourFerie>

        public int CompareTo(JourFerieW jourFerie)
        {
            if ((this.Date > jourFerie.Date) || ((this.Date == null) && (jourFerie != null)))
                return 1;
            else if ((this.Date < jourFerie.Date) || ((this.Date != null) && (jourFerie == null)))
                return -1;
            return 0;
        }

        // Define the is greater than operator.
        public static bool operator >(JourFerieW operand1, JourFerieW operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        // Define the is less than operator.
        public static bool operator <(JourFerieW operand1, JourFerieW operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(JourFerieW operand1, JourFerieW operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(JourFerieW operand1, JourFerieW operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        #endregion
    }

    public class JourFerie : JourFerieW
    {
        public JourFerie() : base()
        {
        }

        public JourFerie(JourFerieW jfw): base(jfw)
        { }

        public JourFerie(DateTime Date, string Description) : base()
        { }

        public JourFerie(DateTime Date, TimeSpan NbHeures, string TypeJour, string Description, bool Inclus) : base(Date, NbHeures, TypeJour)
        {
            this._Description = Description;
            this._Inclus = Inclus;
        }

        /// <summary>
        /// Heures effectivement récupérées
        /// </summary>
        private TimeSpan _NbHeuresRCF;
        public TimeSpan NbHeuresRCF
        {
            get
            {
                return _NbHeuresRCF;
            }
            set
            {
                if (value != _NbHeuresRCF)
                {
                    _NbHeuresRCF = value;
                    RaisePropertyChanged("NbHeuresRCF");
                }
            }
        }

        private bool _Inclus;
        public bool Inclus
        {
            get
            {
                return _Inclus;
            }
            set
            {
                if (value != _Inclus)
                {
                    _Inclus = value;
                    RaisePropertyChanged("Inclus");
                }
            }
        }
    }

    public class RCFerie : ObservableObject
    {
        private DateTime _RCF;
        public DateTime RCF
        {
            get
            {
                return _RCF;
            }
            set
            {
                if (value != _RCF)
                {
                    _RCF = value;
                    RaisePropertyChanged("RCF");
                }
            }
        }

        private TimeSpan _NbHeuresRCF;
        public TimeSpan NbHeuresRCF
        {
            get
            {
                return _NbHeuresRCF;
            }
            set
            {
                if (value != _NbHeuresRCF)
                {
                    _NbHeuresRCF = value;
                    RaisePropertyChanged("NbHeuresRCF");
                }
            }
        }

        private DateTime? _JourFerie;
        public DateTime? JourFerie
        {
            get
            {
                return _JourFerie;
            }
            set
            {
                if (value != _JourFerie)
                {
                    _JourFerie = value;
                    RaisePropertyChanged("JourFerie");
                }
            }
        }

        private TimeSpan _NbHeures;
        public TimeSpan NbHeures
        {
            get
            {
                return _NbHeures;
            }
            set
            {
                if (value != _NbHeures)
                {
                    _NbHeures = value;
                    RaisePropertyChanged("NbHeures");
                }
            }
        }

        public RCFerie()
        { }

        public RCFerie(DateTime RCF, DateTime? JourFerie, TimeSpan NbHeures)
        {
            this.RCF = RCF;
            this.JourFerie = JourFerie;
            this.NbHeures = NbHeures;
            this.NbHeuresRCF = NbHeures;
        }
    }

    //public class ParamsRCFeriesMessage
    //{
    //    public ClassGetMS.StrucParam ParamGlobaux;
    //    public DateTime RCFerie;
    //    public DateTime DebPeriode;
    //    public DateTime FinPeriode;
    //    public string TypeJourRCF;
    //}

    public struct ParametresRCF
    {
        public int ModHRecup;
        public bool CyHRecup;
        //public bool CyRecupRH;
        //public bool Cy1Mai;
        public bool CyAnciens;
        public DateTime CyDateSeuil;
        public bool ModEqCy;
    }
}