using GalaSoft.MvvmLight;
using System;
using System.Windows.Media;

namespace ClassGetMS.Models
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class HorairesTypeVM : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the HorairesTypesViewModel class.
        /// </summary>
        public HorairesTypeVM()
        {
        }
        

        private string _Code_Horaire_Type;
        public string Code_Horaire_Type
        {
            get
            {
                return _Code_Horaire_Type;
            }
            set
            {
                if (value != _Code_Horaire_Type)
                {
                    _Code_Horaire_Type = value;
                    RaisePropertyChanged("Code_Horaire_Type");
                }
            }
        }
        
        private string _IDetablissement;
        public string IDetablissement
        {
            get
            {
                return _IDetablissement;
            }
            set
            {
                if (value != _IDetablissement)
                {
                    _IDetablissement = value;
                    RaisePropertyChanged("IDetablissement");
                }
            }
        }

        private string _Type_jour;
        public string Type_jour
        {
            get
            {
                return _Type_jour;
            }
            set
            {
                if (value != _Type_jour)
                {
                    _Type_jour = value;
                    RaisePropertyChanged("Type_jour");
                }
            }
        }

        private Nullable<System.DateTime> _deb_prise_1;
        public Nullable<System.DateTime> deb_prise_1
        {
            get
            {
                return _deb_prise_1;
            }
            set
            {
                if (value != _deb_prise_1)
                {
                    _deb_prise_1 = value;
                    RaisePropertyChanged("deb_prise_1");
                }
            }
        }

        private Nullable<System.DateTime> _fin_prise_1;
        public Nullable<System.DateTime> fin_prise_1
        {
            get
            {
                return _fin_prise_1;
            }
            set
            {
                if (value != _fin_prise_1)
                {
                    _fin_prise_1 = value;
                    RaisePropertyChanged("fin_prise_1");
                }
            }
        }

        private string _pause_1;
        public string pause_1
        {
            get
            {
                return _pause_1;
            }
            set
            {
                if (value != _pause_1)
                {
                    _pause_1 = value;
                    RaisePropertyChanged("pause_1");
                }
            }
        }

        private string _Repas1;
        public string Repas1
        {
            get
            {
                return _Repas1;
            }
            set
            {
                if (value != _Repas1)
                {
                    _Repas1 = value;
                    RaisePropertyChanged("Repas1");
                }
            }
        }

        private string _Type_prise_1;
        public string Type_prise_1
        {
            get
            {
                return _Type_prise_1;
            }
            set
            {
                if (value != _Type_prise_1)
                {
                    _Type_prise_1 = value;
                    RaisePropertyChanged("Type_prise_1");
                }
            }
        }

        private Nullable<System.DateTime> _deb_prise_2;
        public Nullable<System.DateTime> deb_prise_2
        {
            get
            {
                return _deb_prise_2;
            }
            set
            {
                if (value != _deb_prise_2)
                {
                    _deb_prise_2 = value;
                    RaisePropertyChanged("deb_prise_2");
                }
            }
        }

        private Nullable<System.DateTime> _fin_prise_2;
        public Nullable<System.DateTime> fin_prise_2
        {
            get
            {
                return _fin_prise_2;
            }
            set
            {
                if (value != _fin_prise_2)
                {
                    _fin_prise_2 = value;
                    RaisePropertyChanged("fin_prise_2");
                }
            }
        }

        private string _pause_2;
        public string pause_2
        {
            get
            {
                return _pause_2;
            }
            set
            {
                if (value != _pause_2)
                {
                    _pause_2 = value;
                    RaisePropertyChanged("pause_2");
                }
            }
        }

        private string _Repas2;
        public string Repas2
        {
            get
            {
                return _Repas2;
            }
            set
            {
                if (value != _Repas2)
                {
                    _Repas2 = value;
                    RaisePropertyChanged("Repas2");
                }
            }
        }

        private string _type_prise_2;
        public string type_prise_2
        {
            get
            {
                return _type_prise_2;
            }
            set
            {
                if (value != _type_prise_2)
                {
                    _type_prise_2 = value;
                    RaisePropertyChanged("type_prise_2");
                }
            }
        }

        private Nullable<System.DateTime> _deb_prise_3;
        public Nullable<System.DateTime> deb_prise_3
        {
            get
            {
                return _deb_prise_3;
            }
            set
            {
                if (value != _deb_prise_3)
                {
                    _deb_prise_3 = value;
                    RaisePropertyChanged("deb_prise_3");
                }
            }
        }

        private Nullable<System.DateTime> _fin_prise_3;
        public Nullable<System.DateTime> fin_prise_3
        {
            get
            {
                return _fin_prise_3;
            }
            set
            {
                if (value != _fin_prise_3)
                {
                    _fin_prise_3 = value;
                    RaisePropertyChanged("fin_prise_3");
                }
            }
        }

        private string _type_prise_3;
        public string type_prise_3
        {
            get
            {
                return _type_prise_3;
            }
            set
            {
                if (value != _type_prise_3)
                {
                    _type_prise_3 = value;
                    RaisePropertyChanged("type_prise_3");
                }
            }
        }

        private string _Repas3;
        public string Repas3
        {
            get
            {
                return _Repas3;
            }
            set
            {
                if (value != _Repas3)
                {
                    _Repas3 = value;
                    RaisePropertyChanged("Repas3");
                }
            }
        }

        private Nullable<System.DateTime> _Durée;
        public Nullable<System.DateTime> Durée
        {
            get
            {
                return _Durée;
            }
            set
            {
                if (value != _Durée)
                {
                    _Durée = value;
                    RaisePropertyChanged("Durée");
                }
            }
        }

        private string _type_durée;
        public string type_durée
        {
            get
            {
                return _type_durée;
            }
            set
            {
                if (value != _type_durée)
                {
                    _type_durée = value;
                    RaisePropertyChanged("type_durée");
                }
            }
        }

        private bool _nuit;
        public bool nuit
        {
            get
            {
                return _nuit;
            }
            set
            {
                if (value != _nuit)
                {
                    _nuit = value;
                    RaisePropertyChanged("nuit");
                }
            }
        }

        private SolidColorBrush _BGColor;
        public SolidColorBrush BGColor
        {
            get
            {
                return _BGColor;
            }
            set
            {
                if (value != _BGColor)
                {
                    _BGColor = value;
                    RaisePropertyChanged("BGColor");
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
    }
}