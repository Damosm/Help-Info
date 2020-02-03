using ClassLibraryProget;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ClassGetMS.Models
{

    public class LigneTransfert : ObservableObject, IEquatable<LigneTransfert>
    {
        private SolidColorBrush JBrush = Brushes.LightGreen;
        private SolidColorBrush GBrush = Brushes.LightSalmon;
        private SolidColorBrush FBrush = Brushes.Gold;
        private SolidColorBrush CBrush = Brushes.LightSkyBlue;
        private SolidColorBrush FnBrush = Brushes.Orchid;

        //public Modele Modele;

        private int _Index;
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                if (value != _Index)
                {
                    _Index = value;
                    RaisePropertyChanged("Index");
                    RaisePropertyChanged("BackColor");
                }
            }
        }

        private string _StLibelle;
        public string StLibelle
        {
            get
            {
                return _StLibelle;
            }
            set
            {
                if (value != _StLibelle)
                {
                    _StLibelle = value;
                    RaisePropertyChanged("StLibelle");
                }
            }
        }

        private string _StDescription;
        public string StDescription
        {
            get
            {
                return _StDescription;
            }
            set
            {
                if (value != _StDescription)
                {
                    _StDescription = value;
                    RaisePropertyChanged("StDescription");
                }
            }
        }

        private bool _EnableEnJours;
        public bool EnableEnJours
        {
            get
            {
                return _EnableEnJours;
            }
            set
            {
                if (value != _EnableEnJours)
                {
                    _EnableEnJours = value;
                    RaisePropertyChanged("EnableEnJours");
                }
            }
        }

        private bool _BoolEnJours;
        public bool BoolEnJours
        {
            get
            {
                return _BoolEnJours;
            }
            set
            {
                if (value != _BoolEnJours)
                {
                    _BoolEnJours = value;
                    RaisePropertyChanged("BoolEnJours");
                }
            }
        }

        private bool _EnableEnHeures;
        public bool EnableEnHeures
        {
            get
            {
                return _EnableEnHeures;
            }
            set
            {
                if (value != _EnableEnHeures)
                {
                    _EnableEnHeures = value;
                    RaisePropertyChanged("EnableEnHeures");
                }
            }
        }

        private bool _BoolEnHeures;
        public bool BoolEnHeures
        {
            get
            {
                return _BoolEnHeures;
            }
            set
            {
                if (value != _BoolEnHeures)
                {
                    _BoolEnHeures = value;
                    RaisePropertyChanged("BoolEnHeures");
                }
            }
        }
        
        private bool _EnableEnNombre;
        public bool EnableEnNombre
        {
            get
            {
                return _EnableEnNombre;
            }
            set
            {
                if (value != _EnableEnNombre)
                {
                    _EnableEnNombre = value;
                    RaisePropertyChanged("EnableEnNombre");
                }
            }
        }

        private bool _BoolEnNombre;
        public bool BoolEnNombre
        {
            get
            {
                return _BoolEnNombre;
            }
            set
            {
                if (value != _BoolEnNombre)
                {
                    _BoolEnNombre = value;
                    RaisePropertyChanged("BoolEnNombre");
                }
            }
        }

        private bool _EnableEnDates;
        public bool EnableEnDates
        {
            get
            {
                return _EnableEnDates;
            }
            set
            {
                if (value != _EnableEnDates)
                {
                    _EnableEnDates = value;
                    RaisePropertyChanged("EnableEnDates");
                }
            }
        }

        private bool _BoolEnDates;
        public bool BoolEnDates
        {
            get
            {
                return _BoolEnDates;
            }
            set
            {
                if (value != _BoolEnDates)
                {
                    _BoolEnDates = value;
                    RaisePropertyChanged("BoolEnDates");
                }
            }
        }

        private DateTime _DtDebut;
        public DateTime DtDebut
        {
            get
            {
                return _DtDebut;
            }
            set
            {
                if (value != _DtDebut)
                {
                    _DtDebut = value;
                    RaisePropertyChanged("DtDebut");
                }
            }
        }

        private DateTime _DtFin;
        public DateTime DtFin
        {
            get
            {
                return _DtFin;
            }
            set
            {
                if (value != _DtFin)
                {
                    _DtFin = value;
                    RaisePropertyChanged("DtFin");
                }
            }
        }

        public DateTime DtMois;
        
        private int _AvailableUnite;
        public int AvailableUnite
        {
            get
            {
                return _AvailableUnite;
            }
            set
            {
                _AvailableUnite = value;
                if (value == 0)
                {
                    this.EnableEnJours = true;
                    this.EnableEnHeures = true;
                }
                else if (value == 1)
                {
                    this.EnableEnJours = true;
                    this.EnableEnHeures = false;
                }
                else if (value == 2)
                {
                    this.EnableEnJours = false;
                    this.EnableEnHeures = true;
                }
            }
        }

        private int _IntUnite;
        public int  IntUnite
        {
            get
            {
                return _IntUnite;
            }
            set
            {
                _IntUnite = value;
                if (value == 0)
                {
                    this.BoolEnJours = true;
                    this.BoolEnHeures = true;
                }
                else if (value == 1)
                {
                    this.BoolEnJours = true;
                    this.BoolEnHeures = false;
                }
                else if (value == 2)
                {
                    this.BoolEnJours = false;
                    this.BoolEnHeures = true;
                }
            }
        }

        public string StCodeRubrique;
        public int IntCategorie;
        public string StTypeJour;
        public string StCodeFamille;
        public string StGenre;
        //public string StTable;
        public DataTypeTP typeTP;
        public string StClef
        {
            get
            {
                switch (this.typeTP)
                {
                    case DataTypeTP.TypeJour:
                        return this.StTypeJour;
                    case DataTypeTP.Genre:
                        return this.StGenre;
                    case DataTypeTP.Famille:
                        return this.StCodeFamille;
                    case DataTypeTP.Categorie:
                        return this.IntCategorie.ToString();
                    case DataTypeTP.Fonction:
                        return this.StCodeRubrique;
                    default:
                        return "";
                }
            }
        }


        public SolidColorBrush BackColor
        {
            get
            {
                if (Index % 2 == 1)
                {
                    switch (this.typeTP)
                    {
                        case DataTypeTP.TypeJour:
                            return JBrush;
                        case DataTypeTP.Genre:
                            return GBrush;
                        case DataTypeTP.Categorie:
                            return CBrush;
                        case DataTypeTP.Famille:
                            return FBrush;
                        case DataTypeTP.Fonction:
                            return FnBrush;
                        default:
                            return Brushes.Tomato;
                    }
                }
                else
                {
                    switch (this.typeTP)
                    {
                        case DataTypeTP.TypeJour:
                            return ColorHelpers.Lighten(JBrush, 0.5);
                        case DataTypeTP.Genre:
                            return ColorHelpers.Lighten(GBrush, 0.5);
                        case DataTypeTP.Categorie:
                            return ColorHelpers.Lighten(CBrush, 0.5);
                        case DataTypeTP.Famille:
                            return ColorHelpers.Lighten(FBrush, 0.5);
                        case DataTypeTP.Fonction:
                            return ColorHelpers.Lighten(FnBrush, 0.5);
                        default:
                            return Brushes.OrangeRed;
                    }
                }
            }
        }

        public LigneTransfert()
        {
            // TODO: Complete member initialization
        }

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            LigneTransfert objAsLigneCatTransfert = obj as LigneTransfert;
            if (objAsLigneCatTransfert == null)
                return false;
            else
                return Equals(objAsLigneCatTransfert);
        }

        public bool Equals(LigneTransfert ligneCatTransfert)
        {
            if (ligneCatTransfert.typeTP == this.typeTP &&
                ligneCatTransfert.StClef == this.StClef)
                return true;
            else
                return false;
        }

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
        #endregion
    }

    public class ColonneTransfert: ObservableObject
    {
        //public bool BoolModif;
        
        private string _StLibelle;
        public string StLibelle
        {
            get
            {
                return _StLibelle;
            }
            set
            {
                if (value != _StLibelle)
                {
                    _StLibelle = value;
                    RaisePropertyChanged("StLibelle");
                }
            }
        }

        private string _StDescription;
        public string StDescription
        {
            get
            {
                return _StDescription;
            }
            set
            {
                if (value != _StDescription)
                {
                    _StDescription = value;
                    RaisePropertyChanged("StDescription");
                }
            }
        }

        private bool _BoolSelection;
        public bool BoolSelection
        {
            get
            {
                return _BoolSelection;
            }
            set
            {
                if (value != _BoolSelection)
                {
                    _BoolSelection = value;
                    RaisePropertyChanged("BoolSelection");
                }
            }
        }

        public ColonneTransfert(string StLibelle, string StDescription, bool BoolSelection)
        {
            _StLibelle = StLibelle;
            _StDescription = StDescription;
            _BoolSelection = BoolSelection;
        }
    }

    public class TransfertPaieModel
    {
        private SqlConnection _oConnection;
        private StrucParam _ParamGlobaux;

        private string _StSQL;

        private DataSet _DsParamTransfertPaie = null;
        private DataSet _DsCodificationTypesJour = null;
        private DataSet _DsCategories = null;
        private DataSet _DsGenres = null;
        private DataSet _DsFamilles = null;
        private DataSet _DsModeles;
        private DataSet _DsParamUser;
        private DataSet _DsColonnesTransfert;

        private DateTime DtMois;

        private string _Droits;
        public string StModeleDefaut = "(défaut)";
        private string StModelePaie; //Modèle par défaut pour l'utilisateur courant

        public bool BoolModifColonnes;

        public TransfertPaieModel(StrucParam ParamGlobaux)
        {
            _ParamGlobaux = ParamGlobaux;
            _Droits = ClassGetMS.Droits.ValeurDroit(ParamGlobaux.Matricule, ParamGlobaux.IDEtablissement, ParamGlobaux.ConnectionString, "Transfert paie");
        }

        /// <summary>
        /// Initialize la liste des types de données extractibles avec les valeurs par défaut
        /// </summary>
        /// <returns>List<LigneTransfert> LignesTransfert</returns>
        public List<LigneTransfert> InitializeLignesTransfert()
        {
            List<LigneTransfert> LignesTransfert = new List<LigneTransfert>();
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);

            DateTime DtToday = DateTime.Today;
            this.DtMois = new DateTime(DtToday.Year, DtToday.Month, 1);

            // Types Jours
            _StSQL = @"Select Categorie, ""Type Jour"", description, ""Code Famille"", Genre, Unité 
                       From CodificationTypesJour
                       Where Supprimer = 0";
            _DsCodificationTypesJour = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "CodificationTypesJour", _StSQL);
            foreach (DataRow myRow in _DsCodificationTypesJour.Tables[0].Rows)
            {
                try
                {
                    //LigneTransfert.Add(new LigneTransfert(myRow["Type Jour"] + " " + myRow["description"].ToString(), "", "",
                    //    Convert.ToInt32(myRow["Categorie"]), myRow["Type Jour"].ToString(), myRow["Code Famille"].ToString(), myRow["Genre"].ToString(),
                    //    Convert.ToInt32(myRow["Unité"]), false, DataTypeTP.TypeJour,
                    //    this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
                    LignesTransfert.Add(new Models.LigneTransfert
                    {
                        StLibelle = myRow["Type Jour"] + " " + myRow["description"].ToString(),
                        IntCategorie = Convert.ToInt32(myRow["Categorie"]),
                        StTypeJour = myRow["Type Jour"].ToString(),
                        StCodeFamille = myRow["Code Famille"].ToString(),
                        StGenre = myRow["Genre"].ToString(),
                        AvailableUnite = Convert.ToInt32(myRow["Unité"]),
                        IntUnite = Convert.ToInt32(myRow["Unité"]),
                        EnableEnDates = true,
                        BoolEnDates = false,
                        typeTP = DataTypeTP.TypeJour,
                        DtMois = this.DtMois,
                        DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                        DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
                    });
                }
                catch (Exception exc)
                {
                    Debug.WriteLine("CatTransfert.Add: " + exc.Message);
                }
            }

            // Genres
            _StSQL = @"Select Genre, Libellé From Genres";
            _DsGenres = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "Genres", _StSQL);
            foreach (DataRow myRow in _DsGenres.Tables[0].Rows)
            {
                try
                {
                    //LigneTransfert.Add(new LigneTransfert(myRow["Libellé"].ToString(), "", "",
                    //    -1, "", "", myRow["Genre"].ToString(), 0, false, DataTypeTP.Genre,
                    //    this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
                    LignesTransfert.Add(new Models.LigneTransfert
                    {
                        StLibelle = myRow["Libellé"].ToString(),
                        IntCategorie = -1,
                        StGenre = myRow["Genre"].ToString(),
                        AvailableUnite = 0,
                        IntUnite = 0,
                        EnableEnDates = true,
                        BoolEnDates = false,
                        typeTP = DataTypeTP.Genre,
                        DtMois = this.DtMois,
                        DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                        DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
                    });
                }
                catch (Exception exc)
                {
                    Debug.WriteLine("CatTransfert.Add: " + exc.Message);
                }
            }

            // Familles
            _StSQL = @"Select ""Code Famille"", Description From Familles Where Supprimer=0";
            _DsFamilles = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "Familles", _StSQL);
            foreach (DataRow myRow in _DsFamilles.Tables[0].Rows)
            {
                try
                {
                    //LigneTransfert.Add(new LigneTransfert(myRow["Code Famille"] + " " + myRow["Description"].ToString(), "", "",
                    //    -1, "", myRow["Code Famille"].ToString(), "", 0, false, DataTypeTP.Famille,
                    //    this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
                    LignesTransfert.Add(new Models.LigneTransfert
                    {
                        StLibelle = myRow["Code Famille"] + " " + myRow["Description"].ToString(),
                        IntCategorie = -1,
                        StCodeFamille = myRow["Code Famille"].ToString(),
                        AvailableUnite = 0,
                        IntUnite = 0,
                        EnableEnDates = true,
                        BoolEnDates = false,
                        typeTP = DataTypeTP.Famille,
                        DtMois = this.DtMois,
                        DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                        DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
                    });
                }
                catch (Exception exc)
                {
                    Debug.WriteLine("CatTransfert.Add: " + exc.Message);
                }
            }

            // Catégories
            _StSQL = @"Select Catégorie, Libellé From Catégories";
            _DsCategories = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "Catégories", _StSQL);
            foreach (DataRow myRow in _DsCategories.Tables[0].Rows)
            {
                try
                {
                    //LigneTransfert.Add(new LigneTransfert(myRow["Catégorie"] + " " + myRow["Libellé"].ToString(), "", "",
                    //    Convert.ToInt32(myRow["Catégorie"]), "", "", "", 0, false, DataTypeTP.Categorie,
                    //    new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
                    LignesTransfert.Add(new Models.LigneTransfert
                    {
                        StLibelle = myRow["Catégorie"] + " " + myRow["Libellé"].ToString(),
                        IntCategorie = Convert.ToInt32(myRow["Catégorie"]),
                        AvailableUnite = 1,
                        IntUnite = 1,
                        EnableEnDates = true,
                        BoolEnDates = false,
                        typeTP = DataTypeTP.Categorie,
                        DtMois = new DateTime(DtMois.Year, DtMois.Month, 1),
                        DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                        DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
                    });
                }
                catch (Exception exc)
                {
                    Debug.WriteLine("CatTransfert.Add: " + exc.Message);
                }
            }
            

            // Autres fonctions
            //LigneTransfert.Add(new LigneTransfert("Total Repas", "Liste de tous les repas pris sur la période", "RT",
            //        -1, "", "", "", 2, false, DataTypeTP.Fonction,
            //        this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Total Repas",
                StDescription = "Liste de tous les repas pris sur la période",
                StCodeRubrique = "RT",
                IntCategorie = -1,
                AvailableUnite = -1,
                IntUnite = -1,
                EnableEnNombre = true,
                BoolEnNombre = true,
                BoolEnDates = false,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });
            //LigneTransfert.Add(new LigneTransfert("Repas payants", "Liste des repas payants pris sur la période", "RP",
            //        -1, "", "", "", 2, false, DataTypeTP.Fonction,
            //        this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Repas payants",
                StDescription = "Liste des repas payants pris sur la période",
                StCodeRubrique = "RP",
                IntCategorie = -1,
                AvailableUnite = -1,
                IntUnite = -1,
                EnableEnNombre = true,
                BoolEnNombre = true,
                BoolEnDates = false,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });
            //LigneTransfert.Add(new LigneTransfert("Repas gratuits", "Liste des repas gratuits pris sur la période", "RG",
            //        -1, "", "", "", 2, false, DataTypeTP.Fonction,
            //        this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Repas gratuits",
                StDescription = "Liste des repas gratuits pris sur la période",
                StCodeRubrique = "RG",
                IntCategorie = -1,
                AvailableUnite = -1,
                IntUnite = -1,
                EnableEnNombre = true,
                BoolEnNombre = true,
                BoolEnDates = false,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });
            //LigneTransfert.Add(new LigneTransfert("Repas Av/Nat", "Liste des repas avantage en nature pris sur la période", "RA",
            //        -1, "", "", "", 2, false, DataTypeTP.Fonction,
            //        this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Repas Av/Nat",
                StDescription = "Liste des repas avantage en nature pris sur la période",
                StCodeRubrique = "RA",
                IntCategorie = -1,
                AvailableUnite = -1,
                IntUnite = -1,
                EnableEnNombre = true,
                BoolEnNombre = true,
                BoolEnDates = false,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });
            //LigneTransfert.Add(new LigneTransfert("Heures W Dim. et Fériés", "Nb d'heures de travail les dimanches et jours fériés", "IDF",
            //        -1, "", "", "", 2, false, DataTypeTP.Fonction,
            //        this.DtMois, new DateTime(DtMois.Year, DtMois.Month, 1), new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))));
            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Heures W Dim. et Fériés",
                StDescription = "Nb d'heures de travail les dimanches et jours fériés",
                StCodeRubrique = "IDF",
                IntCategorie = -1,
                AvailableUnite = 2,
                IntUnite = 2,
                EnableEnDates = true,
                BoolEnDates = true,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });

            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Nuits W",
                StDescription = "Nb de nuits travaillées",
                StCodeRubrique = "Nuits",
                IntCategorie = -1,
                AvailableUnite = -1,
                IntUnite = -1,
                EnableEnNombre = true,
                BoolEnNombre = true,
                BoolEnDates = false,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });

            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Droits RCC",
                StDescription = "Nb d'heures de droits à Repos Compensateurs de coupure accumulés sur la période",
                StCodeRubrique = "DRCC",
                IntCategorie = -1,
                AvailableUnite = 2,
                IntUnite = 2,
                EnableEnDates = false,
                BoolEnDates = false,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });

            LignesTransfert.Add(new Models.LigneTransfert
            {
                StLibelle = "Droits RCN",
                StDescription = "Nb d'heures de droits à Repos Compensateurs de nuit accumulés sur la période",
                StCodeRubrique = "DRCN",
                IntCategorie = -1,
                AvailableUnite = 2,
                IntUnite = 2,
                EnableEnDates = false,
                BoolEnDates = false,
                typeTP = DataTypeTP.Fonction,
                DtMois = this.DtMois,
                DtDebut = new DateTime(DtMois.Year, DtMois.Month, 1),
                DtFin = new DateTime(DtMois.Year, DtMois.Month, DateTime.DaysInMonth(DtMois.Year, DtMois.Month))
            });

            return LignesTransfert;
        }

        public ObservableCollection<LigneTransfert> InitializeLignesTransfert(Modele modele, DateTime Mois)
        {
            List<LigneTransfert> TmpLigneTransfert = new List<LigneTransfert>();
            List<LigneTransfert> ResultLigneTransfert = new List<LigneTransfert>();
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);
            
            this.DtMois = new DateTime(Mois.Year, Mois.Month, 1);

            TmpLigneTransfert = InitializeLignesTransfert();

            if (modele != null)
            {
                // Récupère les paramètres de transfert paie depuis la base de données.
                _StSQL = @"Select Matricule, [Table], Clef, Modele, Libellé, Description, Unité, AffDate, AffNombre, Ordre, CodeRubrique, Catégorie, Mois, Debut, Fin
                       From ParamTransfertPaie
                       Where Modele='" + modele.StModele /* + @"' AND Matricule='" + ParamGlobaux.Matricule */ + @"'
                       Order by Ordre";
                _DsParamTransfertPaie = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "ParamTransfertPaie", _StSQL);
                _oConnection.Close();
                if (_DsParamTransfertPaie.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow DrParam in _DsParamTransfertPaie.Tables[0].Rows)
                    {
                        DateTime DtOldDebut = Convert.ToDateTime(DrParam["Debut"]);
                        int IntDebutDay = DtOldDebut.Day;
                        if (IntDebutDay == DateTime.DaysInMonth(DtOldDebut.Year, DtOldDebut.Month))
                            IntDebutDay = DateTime.DaysInMonth(Mois.Year, Mois.Month);
                        DateTime DtOldFin = Convert.ToDateTime(DrParam["Fin"]);
                        int IntFinDay = DtOldFin.Day;
                        if (IntFinDay == DateTime.DaysInMonth(DtOldFin.Year, DtOldFin.Month))
                            IntFinDay = DateTime.DaysInMonth(Mois.Year, Mois.Month);
                        
                        DateTime TmpDebut = Convert.ToDateTime(DrParam["Debut"]);
                        DateTime TmpOldMois = Convert.ToDateTime(DrParam["Mois"]);
                        int TmpNewMois = (Mois.Month + Tools.MonthDifference(TmpDebut, TmpOldMois)) % 12 == 0 ? 12 : (Mois.Month + Tools.MonthDifference(TmpDebut, TmpOldMois)) % 12;
                        DateTime DtNewDebut = new DateTime(Mois.Year, TmpNewMois, IntDebutDay);
                        
                        DateTime TmpFin = Convert.ToDateTime(DrParam["Fin"]);
                        TmpNewMois = (Mois.Month + Tools.MonthDifference(TmpFin, TmpOldMois)) % 12 == 0 ? 12 : (Mois.Month + Tools.MonthDifference(TmpFin, TmpOldMois)) % 12;
                        DateTime DtNewFin = new DateTime(Mois.Year, TmpNewMois, IntFinDay);
                        DataTypeTP dataTypeTP = (DataTypeTP)Enum.Parse(typeof(DataTypeTP), DrParam["Table"].ToString());
                        switch (dataTypeTP)
                        {
                            case DataTypeTP.TypeJour:
                                try
                                {
                                    ResultLigneTransfert.Add(new LigneTransfert
                                    {
                                        StLibelle = DrParam["Libellé"].ToString(),
                                        StDescription = DrParam["Description"].ToString(),
                                        StCodeRubrique = DrParam["CodeRubrique"].ToString(),
                                        IntCategorie = Convert.ToInt32(DrParam["Catégorie"]),
                                        StTypeJour = DrParam["Clef"].ToString(),
                                        IntUnite = Convert.ToInt32(DrParam["Unité"]),
                                        BoolEnDates = Convert.ToBoolean(DrParam["AffDate"]),
                                        typeTP = dataTypeTP,
                                        DtMois = this.DtMois,
                                        DtDebut = DtNewDebut,
                                        DtFin = DtNewFin
                                    });
                                }
                                catch (Exception exc)
                                {
                                    Debug.WriteLine("TmpLigneTransfert.Add: " + exc.Message);
                                }
                                break;
                            case DataTypeTP.Genre:
                                try
                                {
                                    ResultLigneTransfert.Add(new LigneTransfert
                                    {
                                        StLibelle = DrParam["Libellé"].ToString(),
                                        StDescription = DrParam["Description"].ToString(),
                                        StCodeRubrique = DrParam["CodeRubrique"].ToString(),
                                        IntCategorie = -1,
                                        StGenre = DrParam["Clef"].ToString(),
                                        IntUnite = Convert.ToInt32(DrParam["Unité"]),
                                        BoolEnDates = Convert.ToBoolean(DrParam["AffDate"]),
                                        typeTP = dataTypeTP,
                                        DtMois = this.DtMois,
                                        DtDebut = DtNewDebut,
                                        DtFin = DtNewFin
                                    });
                                }
                                catch (Exception exc)
                                {
                                    Debug.WriteLine("TmpLigneTransfert.Add: " + exc.Message);
                                }
                                break;
                            case DataTypeTP.Famille:
                                try
                                {
                                    ResultLigneTransfert.Add(new LigneTransfert
                                    {
                                        StLibelle = DrParam["Libellé"].ToString(),
                                        StDescription = DrParam["Description"].ToString(),
                                        StCodeRubrique = DrParam["CodeRubrique"].ToString(),
                                        IntCategorie = -1,
                                        StCodeFamille = DrParam["Clef"].ToString(),
                                        IntUnite = Convert.ToInt32(DrParam["Unité"]),
                                        BoolEnDates = Convert.ToBoolean(DrParam["AffDate"]),
                                        typeTP = dataTypeTP,
                                        DtMois = this.DtMois,
                                        DtDebut = DtNewDebut,
                                        DtFin = DtNewFin
                                    });
                                }
                                catch (Exception exc)
                                {
                                    Debug.WriteLine("TmpLigneTransfert.Add: " + exc.Message);
                                }
                                break;
                            case DataTypeTP.Categorie:
                                try
                                {
                                    ResultLigneTransfert.Add(new LigneTransfert
                                    {
                                        StLibelle = DrParam["Libellé"].ToString(),
                                        StDescription = DrParam["Description"].ToString(),
                                        StCodeRubrique = DrParam["CodeRubrique"].ToString(),
                                        IntCategorie = Convert.ToInt32(DrParam["Catégorie"]),
                                        IntUnite = Convert.ToInt32(DrParam["Unité"]),
                                        BoolEnDates = Convert.ToBoolean(DrParam["AffDate"]),
                                        typeTP = dataTypeTP,
                                        DtMois = this.DtMois,
                                        DtDebut = DtNewDebut,
                                        DtFin = DtNewFin
                                    });
                                }
                                catch (Exception exc)
                                {
                                    Debug.WriteLine("TmpLigneTransfert.Add: " + exc.Message);
                                }
                                break;
                            case DataTypeTP.Fonction:
                                try
                                {
                                    ResultLigneTransfert.Add(new LigneTransfert
                                    {
                                        StLibelle = DrParam["Libellé"].ToString(),
                                        StDescription = DrParam["Description"].ToString(),
                                        StCodeRubrique = DrParam["CodeRubrique"].ToString(),
                                        IntCategorie = -1,
                                        IntUnite = Convert.ToInt32(DrParam["Unité"]),
                                        BoolEnNombre = Convert.ToBoolean(DrParam["AffNombre"]),
                                        BoolEnDates = Convert.ToBoolean(DrParam["AffDate"]),
                                        typeTP = DataTypeTP.Fonction,
                                        DtMois = this.DtMois,
                                        DtDebut = DtNewDebut,
                                        DtFin = DtNewFin
                                    });
                                }
                                catch (Exception exc)
                                {
                                    Debug.WriteLine("TmpLigneTransfert.Add: " + exc.Message);
                                }
                                break;
                        }
                    }
                    // Si de nouveaux types existent (Types Prises, Familles, Genres, Catégories), les proposer dans les paramètres de transfert paie.
                    foreach (LigneTransfert ligneTransfert in TmpLigneTransfert)
                    {
                        if (!ResultLigneTransfert.Exists(x => x.Equals(ligneTransfert)))
                        {
                            ResultLigneTransfert.Add(ligneTransfert);
                        }
                    }

                    // Si des types (Types Prises, Familles, Genres, Catégories) ont été supprimés, on les supprime également des paramètres de transfert paie.
                    List<int> _idxToDelete = new List<int>();
                    int idx = 0;
                    foreach (LigneTransfert ligneTransfert in ResultLigneTransfert)
                    {
                        if (!TmpLigneTransfert.Exists(x => x.Equals(ligneTransfert)))
                        {
                            _idxToDelete.Add(idx);
                            try
                            {
                                Debug.WriteLine("ligneCatTransfert: " + ligneTransfert.StLibelle);
                                ClassLibraryProget.DataBase.CmdSqlServer(_oConnection, "ParamTransfertPaie", "Delete From ParamTransfertPaie " +
                                                                                                                "Where Matricule='" + _ParamGlobaux.StMatricule + "' " +
                                                                                                                "And Modele='" + modele.StModele + "' " +
                                                                                                                "And [Table]='" + ligneTransfert.typeTP.ToString() + "' " +
                                                                                                                "And Clef='" + ligneTransfert.StClef + "'");
                            }
                            catch (Exception exc)
                            {
                                Debug.WriteLine("InitializeLignesTransfert: " + exc.Message);
                            }
                        }
                        idx++;
                    }
                    _idxToDelete.Sort();
                    _idxToDelete.Reverse();
                    foreach (int i in _idxToDelete)
                    {
                        ResultLigneTransfert.RemoveAt(i);
                    }
                }
                else
                {
                    ResultLigneTransfert = TmpLigneTransfert;
                    //this.Serialize();
                }
            }
            else
                ResultLigneTransfert = TmpLigneTransfert;

            foreach (LigneTransfert ligne in ResultLigneTransfert)
            {
                LigneTransfert tmpLigne = TmpLigneTransfert.SingleOrDefault(l => l.StClef == ligne.StClef && l.typeTP == ligne.typeTP);
                ligne.EnableEnHeures = tmpLigne.EnableEnHeures;
                ligne.EnableEnJours = tmpLigne.EnableEnJours;
                ligne.EnableEnNombre = tmpLigne.EnableEnNombre;
                ligne.EnableEnDates = tmpLigne.EnableEnDates;
                ligne.BoolEnHeures = ligne.BoolEnHeures && tmpLigne.EnableEnHeures;
                ligne.BoolEnJours = ligne.BoolEnJours && tmpLigne.EnableEnJours;
                ligne.BoolEnNombre = ligne.BoolEnNombre && tmpLigne.EnableEnNombre;
                ligne.BoolEnDates = ligne.BoolEnDates && tmpLigne.EnableEnDates;
                //Debug.WriteLine("InitializeLignesTransfert, " + tmpLigne.StClef + ": " + tmpLigne.AvailableUnite);
                //Debug.WriteLine("    En Heures: " + tmpLigne.EnableEnHeures);
                //Debug.WriteLine("    En Jours : " + tmpLigne.EnableEnJours);
                //Debug.WriteLine("    En Dates : " + tmpLigne.EnableEnDates);
            }

            Trace.TraceInformation("TransfertPaieModel:InitializeLignesTransfert Transfert.Count: " + ResultLigneTransfert.Count);

            int j = 0;
            foreach (LigneTransfert lt in ResultLigneTransfert)
                lt.Index = j++;

            return new ObservableCollection<LigneTransfert>(ResultLigneTransfert);
        }

        public DataSet toDataSet(StrucParam ParamGlobaux, ObservableCollection<LigneTransfert> LignesTransfert, Modele modele)
        {
            //Table ParamTransfertPaie
            DataTable DtTransfert = new DataTable("ParamTransfertPaie");
            DtTransfert.Columns.Add("Matricule");
            DtTransfert.Columns.Add("Modele");
            DtTransfert.Columns.Add("Table");
            DtTransfert.Columns.Add("Clef");
            DtTransfert.Columns.Add("Libellé");
            DtTransfert.Columns.Add("Description");
            DtTransfert.Columns.Add("Unité");
            DtTransfert.Columns.Add("AffDate");
            DtTransfert.Columns.Add("Ordre");
            DtTransfert.Columns.Add("CodeRubrique");
            DtTransfert.Columns.Add("Catégorie");
            DtTransfert.Columns.Add("Mois");
            DtTransfert.Columns.Add("Debut");
            DtTransfert.Columns.Add("Fin");

            string Clef = "";
            int ordre = 0;
            foreach (LigneTransfert LctLigne in LignesTransfert)
            {
                int IntUnite = -1;
                if (LctLigne.BoolEnHeures && LctLigne.BoolEnJours)
                    IntUnite = 0;
                else if (!LctLigne.BoolEnHeures && LctLigne.BoolEnJours)
                    IntUnite = 1;
                else if (LctLigne.BoolEnHeures && !LctLigne.BoolEnJours)
                    IntUnite = 2;
                switch (LctLigne.typeTP)
                {
                    case DataTypeTP.TypeJour:
                        Clef = LctLigne.StTypeJour;
                        break;
                    case DataTypeTP.Genre:
                        Clef = LctLigne.StGenre;
                        break;
                    case DataTypeTP.Famille:
                        Clef = LctLigne.StCodeFamille;
                        break;
                    case DataTypeTP.Categorie:
                        Clef = LctLigne.IntCategorie.ToString();
                        break;
                    case DataTypeTP.Fonction:
                        Clef = LctLigne.StCodeRubrique;
                        break;
                }
                DtTransfert.Rows.Add(ParamGlobaux.Matricule, modele.StModele, LctLigne.typeTP, Clef, LctLigne.StLibelle, LctLigne.StDescription, IntUnite, LctLigne.BoolEnDates, ordre, LctLigne.StCodeRubrique, LctLigne.IntCategorie, LctLigne.DtMois, LctLigne.DtDebut, LctLigne.DtFin);
                ordre++;
            }

            DataSet DsTransfert = new DataSet();
            DsTransfert.Tables.Add(DtTransfert);

            return (DsTransfert);
        }

        public void SerializeLignes(ObservableCollection<LigneTransfert> LignesTransfert, Modele modele)
        {
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);
            DataSet DsTransfert = this.toDataSet(_ParamGlobaux, LignesTransfert, modele);

            for (int i = 0; i < DsTransfert.Tables[0].Rows.Count; i++)
            {
                ClassLibraryProget.DataBase.UPSERTSqlServer(_oConnection, DsTransfert, i, new string[] { "Matricule", "Modele", "Table", "Clef" });
            }
            _oConnection.Close();
        }


        public ObservableCollection<ColonneTransfert> InitializeColonnes(Modele modele)
        {
            ObservableCollection<ColonneTransfert> colonnesTransfert = new ObservableCollection<ColonneTransfert>();
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);

            if (modele != null)
            {
                _StSQL = @"Select Matricule, Modele, Libellé, Description, Ordre, Sélection
                       From ParamTransfertPaieColonnes
                       Where Matricule='" + _ParamGlobaux.Matricule + @"' And Modele='" + modele.StModele + @"'
                       Order by Ordre";
                _DsColonnesTransfert = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "ParamTransfertPaieColonnes", _StSQL);
            }
            if (modele != null || _DsColonnesTransfert.Tables[0].Rows.Count == 0)
            {
                _StSQL = @"Select Matricule, Modele, Libellé, Description, Ordre, Sélection
                       From ParamTransfertPaieColonnes
                       Where Matricule='proget' And Modele='' 
                       Order by Ordre";
                _DsColonnesTransfert = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "ParamTransfertPaieColonnes", _StSQL);
            }

            foreach (DataRow myRow in _DsColonnesTransfert.Tables[0].Rows)
            {
                try
                {
                    colonnesTransfert.Add(new ColonneTransfert(myRow["Libellé"].ToString(), myRow["Description"].ToString(), (bool)myRow["Sélection"]));
                }
                catch (Exception exc)
                {
                    Debug.WriteLine("CatTransfert.Add: " + exc.Message);
                }
            }

            return colonnesTransfert;
        }

        public DataSet ColonnesToDataSet(ObservableCollection<ColonneTransfert> colonnesTransfert, Modele modele)
        {
            DataTable DtColonnes = new DataTable("ParamTransfertPaieColonnes");
            DtColonnes.Columns.Add("Matricule");
            DtColonnes.Columns.Add("Modele");
            DtColonnes.Columns.Add("Libellé");
            DtColonnes.Columns.Add("Description");
            DtColonnes.Columns.Add("Ordre");
            DtColonnes.Columns.Add("Sélection");

            int ordre = 0;
            foreach (ColonneTransfert LctLigne in colonnesTransfert)
            {
                DtColonnes.Rows.Add(_ParamGlobaux.Matricule, modele.StModele, LctLigne.StLibelle, LctLigne.StDescription, ordre, LctLigne.BoolSelection);
                ordre++;
            }

            DataSet DsColonnes = new DataSet();
            DsColonnes.Tables.Add(DtColonnes);

            return (DsColonnes);
        }

        public void SerializeColonnes(ObservableCollection<ColonneTransfert> colonnesTransfert, Modele modele)
        {
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);

            DataSet DsColonnes = this.ColonnesToDataSet(colonnesTransfert, modele);

            for (int i = 0; i < DsColonnes.Tables[0].Rows.Count; i++)
            {
                ClassLibraryProget.DataBase.UPSERTSqlServer(_oConnection, DsColonnes, i, new string[] { "Matricule", "Modele", "Libellé" });
            }

            _oConnection.Close();
        }

        public ObservableCollection<Modele> GetModeles()
        {
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);
            ObservableCollection<Modele> ListModeles = new ObservableCollection<Modele>();

            _StSQL = @"Select Matricule, Modele, [Public]
                     From ParamTransfertPaieModele";
            _DsModeles = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "ParamTransfertPaieModele", _StSQL);

            _StSQL = @"Select Matricule, ModelePaie From ParamUtilisateur Where Matricule='" + _ParamGlobaux.Matricule + @"'";
            _DsParamUser = ClassLibraryProget.DataBase.SELECTSqlServer(_oConnection, "ParamUtilisateur", _StSQL);

            if ((_DsParamUser.Tables[0].Rows.Count != 0) && (_DsParamUser.Tables[0].Rows[0]["ModelePaie"].ToString() != ""))
            {
                StModelePaie = _DsParamUser.Tables[0].Rows[0]["ModelePaie"].ToString();
            }
            else
            {
                StModelePaie = StModeleDefaut;
            }
            if ((_DsParamUser.Tables[0].Rows.Count == 0))
            {
                _DsParamUser = new DataSet();
                _DsParamUser = ClassLibraryProget.DataBase.SchémaSQLServer(_oConnection, "ParamUtilisateur");
            }

            if (_DsModeles.Tables[0].Rows.Count == 0)
            {
                _DsModeles = new DataSet();
                _DsModeles = ClassLibraryProget.DataBase.SchémaSQLServer(_oConnection, "ParamTransfertPaieModele");
            }
            else
            {
                bool BoModeleDefaut;
                foreach (DataRow DrModeles in _DsModeles.Tables[0].Rows)
                {
                    BoModeleDefaut = DrModeles["Modele"].ToString() == StModelePaie ? true : false;
                    ListModeles.Add(new Modele(DrModeles["Modele"].ToString(), 
                                               DrModeles["Matricule"].ToString(), 
                                               (bool)DrModeles["Public"],
                                               _Droits != "L" && _ParamGlobaux.Matricule == DrModeles["Matricule"].ToString(), 
                                               BoModeleDefaut, 
                                               true,
                                               DrModeles["Matricule"].ToString() == _ParamGlobaux.Matricule || (bool)DrModeles["Public"]));
                }
            }
            if ((_DsModeles.Tables[0].Rows.Count == 0) || ((_DsModeles.Tables[0].Rows.Count != 0) && (_DsParamUser.Tables[0].Rows.Find(_ParamGlobaux.Matricule) == null)))
            {
                //ListModeles.Add(new Modele(StModeleDefaut, ParamGlobaux.Matricule, false, true));
                //this.Serialise(StModeleDefaut);
            }
            _oConnection.Close();
            Trace.TraceInformation("TransfertPaieModel.GetModeles: Lignes: " + ListModeles.Count);
            return ListModeles;
        }

        public void DeleteModele(string StModele)
        {
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);
            _StSQL = @"Delete From ParamTransfertPaieModele Where Matricule='" + _ParamGlobaux.Matricule + @"' And Modele='" + StModele + @"'";
            ClassLibraryProget.DataBase.CmdSqlServer(_oConnection, "ParamTransfertPaieModele", _StSQL);
            _oConnection.Close();

            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);
            _StSQL = @"Delete From ParamTransfertPaie Where Matricule='" + _ParamGlobaux.Matricule + @"' And Modele='" + StModele + @"'";
            ClassLibraryProget.DataBase.CmdSqlServer(_oConnection, "ParamTransfertPaie", _StSQL);
            _oConnection.Close();

            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);
            _StSQL = @"Delete From ParamTransfertPaieColonnes Where Matricule ='" + _ParamGlobaux.Matricule + @"' And Modele='" + StModele + @"'";
            ClassLibraryProget.DataBase.CmdSqlServer(_oConnection, "ParamTransfertPaieColonnes", _StSQL);
            _oConnection.Close();
        }

        public void SerializeModele(Modele modele)
        {
            if ((_ParamGlobaux.Matricule == _ParamGlobaux.StNomProget) ||
                ((ClassGetMS.Droits.ValeurDroit(_ParamGlobaux.Matricule, _ParamGlobaux.IDEtablissement, _ParamGlobaux.ConnectionString, "Transfert paie") != "L") &&
                (modele != null) && (modele.StMatricule == _ParamGlobaux.Matricule)))
            {
                DataSet DsModele = new DataSet();
                DsModele = ClassLibraryProget.DataBase.SchémaSQLServer(_oConnection, "ParamTransfertPaieModele");

                DataRow DrModele = DsModele.Tables[0].NewRow();
                DrModele["Matricule"] = _ParamGlobaux.Matricule;
                DrModele["Modele"] = modele.StModele;
                DrModele["Public"] = modele.BoolPublic;
                DsModele.Tables[0].Rows.Add(DrModele);
                Debug.WriteLine("Modeles.Serialize, modele.BoolPublic: " + modele.BoolPublic);
                Debug.WriteLine("Modeles.Serialize, DrModele[\"Public\"]: " + DrModele["Public"]);
                _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);
                ClassLibraryProget.DataBase.UPSERTSqlServer(_oConnection, DsModele, 0, new string[] { "Matricule", "Modele" });

                if (_DsParamUser.Tables[0].Rows.Count != 0)
                {
                    _DsParamUser.Tables[0].Rows[0]["ModelePaie"] = modele.StModele;
                    ClassLibraryProget.DataBase.UPDATESqlServer(_oConnection, _DsParamUser, 0, new string[] { "Matricule" });
                }
                else
                {
                    _DsParamUser = ClassLibraryProget.DataBase.SchémaSQLServer(_oConnection, "ParamUtilisateur");
                    DataRow DrParamUser = _DsParamUser.Tables[0].NewRow();
                    DrParamUser["Matricule"] = _ParamGlobaux.Matricule;
                    DrParamUser["Format Horaire"] = true;
                    DrParamUser["MatriceVerticale"] = true;
                    DrParamUser["ModelePaie"] = modele.StModele;
                    _DsParamUser.Tables[0].Rows.Add(DrParamUser);
                    ClassLibraryProget.DataBase.INSERTSqlServer(_oConnection, _DsParamUser, 0);
                }
                _oConnection.Close();
                Trace.TraceInformation("TransfertPaieModel.SerializeModel: Fin");
            }
            else
            {
                throw new Exception("Vous n'avez pas les droits pour enregistrer un modèle ou ce nom de modèle existe déjà.");
            }
        }

        public void SaveDefaultModele(Modele modele)
        {
            _oConnection = ClassLibraryProget.DataBase.OpenSqlServer(_ParamGlobaux.ConnectionString);

            if (_DsParamUser.Tables[0].Rows.Count != 0)
            {
                _DsParamUser.Tables[0].Rows[0]["ModelePaie"] = modele.StModele;
                ClassLibraryProget.DataBase.UPDATESqlServer(_oConnection, _DsParamUser, 0, new string[] { "Matricule" });
            }
            else
            {
                _DsParamUser = ClassLibraryProget.DataBase.SchémaSQLServer(_oConnection, "ParamUtilisateur");
                DataRow DrParamUser = _DsParamUser.Tables[0].NewRow();
                DrParamUser["Matricule"] = _ParamGlobaux.Matricule;
                DrParamUser["Format Horaire"] = true;
                DrParamUser["MatriceVerticale"] = false;
                DrParamUser["ModelePaie"] = modele.StModele;
                DrParamUser["AffichageTreeView"] = true;
                DrParamUser["ActiverBoutonSP"] = true;
                _DsParamUser.Tables[0].Rows.Add(DrParamUser);
                ClassLibraryProget.DataBase.INSERTSqlServer(_oConnection, _DsParamUser, 0);
            }
            _oConnection.Close();
            Trace.TraceInformation("Modeles.SaveDefaut: Fin");
        }
    }

    public class Modele : ObservableObject, IEquatable<Modele>
    {
        private string _StModele;
        public string StModele
        {
            get
            {
                return _StModele;
            }
            set
            {
                if (value != _StModele)
                {
                    _StModele = value;
                    RaisePropertyChanged("StModele");
                }
            }
        }


        private string _StMatricule;
        public string StMatricule
        {
            get
            {
                return _StMatricule;
            }
            set
            {
                if (value != _StMatricule)
                {
                    _StMatricule = value;
                    RaisePropertyChanged("StMatricule");
                }
            }
        }


        private bool _BoolPublic;
        public bool BoolPublic
        {
            get
            {
                return _BoolPublic;
            }
            set
            {
                if (value != _BoolPublic)
                {
                    _BoolPublic = value;
                    RaisePropertyChanged("BoolPublic");
                }
            }
        }


        private bool _EnablePublic;
        public bool EnablePublic
        {
            get
            {
                return _EnablePublic;
            }
            set
            {
                if (value != _EnablePublic)
                {
                    _EnablePublic = value;
                    RaisePropertyChanged("EnablePublic");
                }
            }
        }


        private bool _BoolDefaut;
        public bool BoolDefaut
        {
            get
            {
                return _BoolDefaut;
            }
            set
            {
                if (value != _BoolDefaut)
                {
                    _BoolDefaut = value;
                    RaisePropertyChanged("BoolDefaut");
                }
            }
        }


        private bool _Serialized;
        public bool Serialized
        {
            get
            {
                return _Serialized;
            }
            set
            {
                if (value != _Serialized)
                {
                    _Serialized = value;
                    RaisePropertyChanged("Serialized");
                }
            }
        }


        private bool _Visible;
        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                if (value != _Visible)
                {
                    _Visible = value;
                    RaisePropertyChanged("Visible");
                }
            }
        }


        public Modele(string StModele, string StMatricule, bool BoolPublic, bool EnablePublic, bool BoolDefaut, bool Serialized, bool Visible)
        {
            _StModele = StModele;
            _StMatricule = StMatricule;
            _BoolPublic = BoolPublic;
            _EnablePublic = EnablePublic;
            _BoolDefaut = BoolDefaut;
            _Serialized = Serialized;
            _Visible = Visible;
        }

        public Modele(Modele Modele)
        {
            _StModele = Modele.StModele;
            _StMatricule = Modele.StMatricule;
            _BoolPublic = Modele.BoolPublic;
            _EnablePublic = Modele.EnablePublic;
            _BoolDefaut = Modele.BoolDefaut;
            _Visible = Modele.Visible;
        }

        #region IEquatable<Modele> Member

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
            return StModele.GetHashCode();
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
            return Equals(obj as Modele);
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
        public bool Equals(Modele other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (this.StModele != other.StModele)
                return false;
            else
                return true;
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
        public static bool operator ==(Modele left, Modele right)
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
        public static bool operator !=(Modele left, Modele right)
        {
            if (ReferenceEquals(left, right))
                return false;

            if (ReferenceEquals(left, null))
                return true;

            return !left.Equals(right);
        }
        #endregion
    }

    public enum DataTypeTP
    {
        TypeJour,
        Genre,
        Categorie,
        Famille,
        Fonction
    }

    public delegate Task<double> FonctionCalcul(StrucParam ParamGlobaux, List<Parametre> Params, DateTime DtDebut, DateTime DtFin);
    public delegate Task<DataSet> FonctionCalculDates(StrucParam ParamGlobaux, DateTime DtDebut, DateTime DtFin);
}
