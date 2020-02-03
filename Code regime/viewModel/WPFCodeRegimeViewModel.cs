using System;
using GalaSoft.MvvmLight;
using MvvmDialogs;
using System.Windows.Media;
using ClassGetMS.Models;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using ClassGetMSReferences.ViewModel;
using ClassGetMSReferences.Views;

namespace ClassGetMSReferences.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WPFCodeRegimeViewModel : ViewModelBase, IModalDialogViewModel
    {
        // Structure de données transmise entre les écrans permettant de retrouver les caractèristiques de l'application        
        public static ClassGetMS.StrucParam ParamGlobaux = new ClassGetMS.StrucParam();

        // instanciation d'un DialogService
        private IDialogService _dialogService;

        public bool? DialogResult
        {
            get
            {
                return true;
            }
        }

        //On créer une variable du type ObservableObject (Celle ci se remplira au fur et a mesure de la saisie des informations dans le WPFCodeRegimeAjouter)
        private CodeRegimeObservable _ObsCodeRegime;
        public CodeRegimeObservable ObsCodeRegime
        {
            get
            {
                return _ObsCodeRegime;
            }
            set
            {
                if( value != _ObsCodeRegime )
                {
                    _ObsCodeRegime = value;
                    RaisePropertyChanged( "ObsCodeRegime" );
                }
            }
        }

        /// <summary>
        /// pour afficher les elements dans la ListView, il va nous falloir une Liste d'objets observables qui récupére les objets observables CODEREGIME
        /// => Création de la liste de type ObservableCollection (pour Binder)
        /// </summary>
        private ObservableCollection<CodeRegimeObservable> _ListViewListe;
        public ObservableCollection<CodeRegimeObservable> ListViewListe
        {
            get
            {
                return _ListViewListe;
            }
            set
            {
                if( value != _ListViewListe )
                {
                    _ListViewListe = value;
                    RaisePropertyChanged( "ListViewListe" );
                }
            }
        }

        /// <summary>
        /// propriété récupérant la valeur de la couleur selectionnées dans le WPFCodeRegimeAjouter
        /// </summary>
        private Color _couleurSelectionnee;
        public Color couleurSelectionnee
        {
            get
            {
                return _couleurSelectionnee;
            }
            set
            {
                if( value != _couleurSelectionnee )
                {
                    _couleurSelectionnee = value;
                    RaisePropertyChanged( "couleurSelectionnee" );
                }
            }
        }

        private static bool _val;
        public static bool val
        {
            get
            {
                return _val;
            }
            set
            {
                if( value != _val )
                {
                    _val = value;
                }
            }
        }

        /// <summary>
        /// couleur retournée dans la listview à la sortie de la base
        /// </summary>
        private Color _couleurVraie;
        public Color couleurVraie
        {
            get
            {
                return _couleurVraie;

            }
            set
            {
                if( value != _couleurVraie )
                {
                    _couleurVraie = value;
                    RaisePropertyChanged( "couleurVraie" );
                }
            }
        }

        /// <summary>
        /// booléen ramenant la valeur de l'état de Check de la CheckBox "tous les Grands Régimes"
        /// </summary>
        private bool _CbxChecked;
        public bool CbxChecked
        {
            get
            {
                return _CbxChecked;
            }
            set
            {
                if( value != _CbxChecked )
                {
                    _CbxChecked = value;
                    RaisePropertyChanged( "CbxChecked" );
                }
            }
        }

        /// <summary>
        /// booléen permettant de faire varier la fenetre WPFCodeRegimeAjouter entre la fonction "Ajouter" et la fonction "Modifier"
        /// </summary>
        private bool _checkCode;
        public bool checkCode
        {
            get
            {
                return _checkCode;
            }
            set
            {
                if( value != _checkCode )
                {
                    _checkCode = value;
                    RaisePropertyChanged( "checkCode" );
                }
            }
        }

        ///////////////////////////////////////////////////
        //  Déclaration des RelayCommand de WPFCodeRegime
        ///////////////////////////////////////////////////

        // création de la commande loaded
        public RelayCommand LoadedPage { get; set; }

        // création d'une commande qui controlera le bouton "ajouter"
        public RelayCommand Ajouter { get; set; }

        // création d'une commande qui controlera le bouton "modifier"
        public RelayCommand<CodeRegimeObservable> Modifier { get; set; }

        // création d'une commande qui controlera le bouton "CbkCalendrier"
        public RelayCommand CbkCodeRegime { get; set; }

        // création d'une commande qui unchecked la CheckBox "tous les calendriers"
        public RelayCommand CbkCodeRegimeUnchecked { get; set; }

        // création d'une commande qui ferme la fenetre WPFCaisseSecus
        public RelayCommand<Window> CodeRegimeQuitter { get; set; }

        // création d'une commande qui gère la checkbox "supprimer"
        public RelayCommand<CodeRegimeObservable> CbxSupprimerListView { get; set; }

        // création de al commande gérant le bouton d'aide
        public RelayCommand Aide { get; set; }

        // création de la commande gérant le menu contextuel de suppression
        public RelayCommand<CodeRegimeObservable> CMSupprimerListView { get; set; }

        // création de la commande gérant la suppression
        public RelayCommand<CodeRegimeObservable> SupprimerListView { get; set; }

        /////////////////////////////////////////////////////////
        //  Déclaration des RelayCommand de WPFCodeRegimeAjouter
        /////////////////////////////////////////////////////////

        // déclaration de la commande ajoutant une Caisse       
        public RelayCommand CodeRegimeAjouterValider { get; set; }

        //déclaration d'une commande qui fermera la page
        public RelayCommand<Window> CodeRegimeAjouterQuitter { get; set; }

        //déclaration de la commande recevant le changement de couleur dans la fenetre WPFCaisseSecusAjouter
        public RelayCommand couleurChange { get; set; }
        public Window WPFCodeRegimeAjouter { get; private set; }

        /// <summary>
        /// constructeur
        /// </summary>
        public WPFCodeRegimeViewModel( IDialogService dialogService )
        {
            //déclaration du Dialog dans le constructeur
            _dialogService = dialogService;

            //*******************************************************************************************************************************************
            // GESTION DE LA PAGE WPFCodeRegime
            //*******************************************************************************************************************************************

            LoadedPage = new RelayCommand( () =>
            {
                ////////////////////////
                //  Gestion des Droits
                ////////////////////////
                CbxChecked = false;
                // vérification des droits
                using( ProgetEntities pg = new ProgetEntities() )
                {

                    Droit droitUtilisateur = pg.Droits.Where( a => a.Matricule == ParamGlobaux.Matricule && a.IDEtablissement == ParamGlobaux.IDEtablissement &&
                    a.Fonctions == "Gestion des codes Régime" ).SingleOrDefault();
                    //On s'assure de ne pas avoir de "Null Pointer Exception"
                    if( droitUtilisateur != null && droitUtilisateur.Droit1 == "CT" )
                    {
                        ControleDroit = true;

                    }
                    else
                    {
                        ControleDroit = false;

                    }
                }
                ChargementListView();

            } );

            ////////////////////////////////////
            //  Gestion des boutons de WPFCaisse
            ////////////////////////////////////

            //------------------
            //  Bouton Quitter
            //------------------

            CodeRegimeQuitter = new RelayCommand<Window>( FermetureFenetre );

            //-----------------
            //  Bouton Ajouter
            //-----------------

            Ajouter = new RelayCommand( () =>
            {
                // instanciation d'un objet vide pour repartir de 0 pour le nouvel ajout
                ObsCodeRegime = new CodeRegimeObservable();
                checkCode = true;
                // on met la couleur de fond du colorpicker à blanc
                couleurSelectionnee = Color.FromRgb( 255, 255, 255 );

                //on envoit vers la fenetre
                _dialogService.ShowDialog<WPFCodeRegimeAjouter>( this, this );
            } );

            //------------------
            //  Evenement Modifier
            //------------------

            Modifier = new RelayCommand<CodeRegimeObservable>( ( item ) =>
            {
                if( item != null )
                {
                    checkCode = false;
                    //on envoit vers la fenetre
                    _dialogService.ShowDialog<WPFCodeRegimeAjouter>( this, this );

                }
                else
                {
                    System.Windows.MessageBox.Show( "aucune ligne selectionnée" );
                }
            } );

            //------------------
            //  Bouton Aide
            //------------------

            Aide = new RelayCommand( () =>
            {
                AffichageAide( "AideAjouterGrandRegime.pdf" );
            } );




            //---------------------
            //  CheckBox Supprimer
            //---------------------

            CbxSupprimerListView = new RelayCommand<CodeRegimeObservable>( ( item ) =>
            {
                    // connexion à la base
                    using( ProgetEntities pg = new ProgetEntities() )
                    {
                        // création d'un nouveau Code regime avec les données recueillies             
                        CodeRegime codeRegime = pg.CodeRegimes.Where( a => a.IDCodeRegime == item.id ).SingleOrDefault();

                        // recherche si ce Code est utilisée dans la table Usager
                        List<Usager> pu = pg.Usagers.Where( a => a.IDCodeRegime == codeRegime.IDCodeRegime ).ToList();
                     



                    if( pu.Count == 0 )
                    {
                            // dans ce cas, ce Code n'existe pas dans la table PlanningUsager, il est dont totalement inutile et va donc être supprimé définitivement
                            pg.CodeRegimes.Remove( codeRegime );
                            pg.SaveChanges();
                        }
                        else
                        {
                            //on met à Code Régime la valeur de item.supprimer (objet porteur de la valeur changée)
                            codeRegime.SUPPRIMER_CodeRegime = item.supprimer;

                            //on sauvegarde les changements
                            pg.SaveChanges();
                        }
                        ChargementListView();
                    }
               

            } );
        

            //----------------------------
            //  CheckBox "Tous les Grands Régimes"
            //----------------------------

            CbkCodeRegime = new RelayCommand( () =>
            {
                using( ProgetEntities pg = new ProgetEntities() )
                {
                    //création de la liste de Caisse qui va recevoir toutes les données de la requête Entity
                    List<CodeRegime> ListeCodes = pg.CodeRegimes.ToList();

                    // récupérons la liste générée par WPFCodeRegime
                    ObservableCollection<CodeRegime> ListViewListeRecup = new ObservableCollection<CodeRegime>( ListeCodes );

                    // on instancie la listViewList
                    ListViewListe = new ObservableCollection<CodeRegimeObservable>();

                    // parcourons la liste pour en extraire les éléments
                    foreach( CodeRegime item in ListViewListeRecup )
                    {
                        //on passe les données à la ListView                     
                        ListViewListe.Add( new CodeRegimeObservable( item.IDCodeRegime, item.LIBELLE_CodeRegime, item.COULEUR_CodeRegime, item.SUPPRIMER_CodeRegime ) );
                    }
                }
            } );

            //gestion du Unchecked de la CheckBox "tous les codes régimes"
            CbkCodeRegimeUnchecked = new RelayCommand( () =>
            {
                ChargementListView();
            } );


            //---------------------
            //  Bouton Supprimer
            //---------------------

            CMSupprimerListView = new RelayCommand<CodeRegimeObservable>( ( item ) =>
            {
                if( item != null )
                {
                    // connexion à la base
                    using( ProgetEntities pg = new ProgetEntities() )
                    {
                        // création d'un nouveau Code regime avec les données recueillies             
                        CodeRegime codeRegime = pg.CodeRegimes.Where( a => a.IDCodeRegime == item.id ).SingleOrDefault();

                        // recherche si ce Code est utilisée dans la table Usager
                        List<Usager> pu = pg.Usagers.Where( a => a.IDCodeRegime == codeRegime.IDCodeRegime ).ToList();

                        if( pu.Count == 0 )
                        {
                            // dans ce cas, ce Code n'existe pas dans la table PlanningUsager, il est dont totalement inutile et va donc être supprimé définitivement
                            pg.CodeRegimes.Remove( codeRegime );
                            pg.SaveChanges();
                        }
                        else
                        {
                            //on met à Code Régime la valeur de item.supprimer (objet porteur de la valeur changée)
                            codeRegime.SUPPRIMER_CodeRegime = !item.supprimer;

                            //on sauvegarde les changements
                            pg.SaveChanges();
                        }
                        ChargementListView();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show( "aucune ligne selectionnée" );
                }
                
            } );

            //*******************************************************************************************************************************************
            // GESTION DE LA PAGE WPFCodeRegimeuAjouter
            //*******************************************************************************************************************************************

            ////////////////////////////////////////////////
            //  Gestion des boutons de WPFCodeRegimeAjouter
            ////////////////////////////////////////////////

            //------------------
            //  Bouton Quitter
            //------------------
            //Si aucune modification n'est en cours, ou on vient de valider notre ajout, on quitte directement
            //Autrement on affiche un message d'avertissement
            CodeRegimeAjouterQuitter = new RelayCommand<Window>( ( WPFCodeRegimeAjouter ) =>
            {
                if( ObsCodeRegime.AllowExit )
                {
                    //Si notre booleen est vrai, pas de modif en attente
                    FermetureFenetre( WPFCodeRegimeAjouter );
                    //On ferme la page
                }
                else
                {
                    //Si notre booleen est faux, nous avons des nouvelles modifications 
                    if( MessageBox.Show( "Vous allez perdre vos modifications en cours", "Attention", MessageBoxButton.OKCancel ) == MessageBoxResult.OK )
                    {
                        //Si l'utilisateur appuie sur "OK" la page est fermée
                        ObsCodeRegime.AllowExit = true;
                        FermetureFenetre( WPFCodeRegimeAjouter );

                        //Lorsque l'ont ferme la page on passe notre booleen a vrai
                    }
                }
            } );


          
            //------------------------
            //  Bouton BtnCodeRegimeAjouter
            //------------------------

            CodeRegimeAjouterValider = new RelayCommand( () =>
            {
                try
                {
                    //on initialise l'accès à la base de donnée (Projet3Entities est le nom de la classe porteuse du dbcontxt dans Projet3BDD.Context.cs)                    
                    using ( ProgetEntities pg = new ProgetEntities() )
                    {
                        // recherche dans la BDD s'il existe déja une Caisse identique
                        CodeRegime existe = pg.CodeRegimes.Where( a => a.IDCodeRegime == ObsCodeRegime.id ).SingleOrDefault();

                        // nous sommes en "Ajouter"
                        if( checkCode == true )
                        {
                           
                            //Verification caractères spéciaux del'id
                            if( !Regex.IsMatch( ObsCodeRegime.id.ToString(), @"^[0-9]+$" ) == false )
                            {


                                //s'il n'existe pas d'objet en bas ayant la même id 
                                if( existe == null )
                                {
                                    if( ObsCodeRegime.id == "" || ObsCodeRegime.libelle == "" || ObsCodeRegime.couleur == 0 )
                                    {
                                        System.Windows.MessageBox.Show( "Veuillez remplir l'ensemble des champs avec une étoile * " );
                                    }
                                    else if( !Regex.IsMatch( ObsCodeRegime.id.ToString(), @"^[a-zA-Z 0-9]+$" ) == true || !Regex.IsMatch( ObsCodeRegime.libelle.ToString(), @"^[a-zA-Z 0-9]+$" ) == true )
                                    {
                                        System.Windows.MessageBox.Show( "caractère spéciaux interdits (lettres et chiffres uniquement)" );
                                    }
                                    // on insère la Caisse dans la BDD
                                    else if( ObsCodeRegime.id != "" && ObsCodeRegime.libelle != "" && ObsCodeRegime.couleur != 0 && !Regex.IsMatch( ObsCodeRegime.libelle.ToString(), @"^[a-zA-Z 0-9]+$" ) == false )
                                    {
                                        pg.CodeRegimes.Add( CodeRegimeObservable.ConversionEnCodeRegime( ObsCodeRegime ) );

                                        // on sauvegarde les changements dans la BDD
                                        pg.SaveChanges();
                                        val = false;

                                        //on met à jour la listeView
                                        ListViewListe.Add( ObsCodeRegime );
                                        System.Windows.MessageBox.Show( "Nouveau Grand Regime Ajouté" );
                                        ObsCodeRegime.AllowExit = true;
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show( "Il existe déja une Caisse portant ce code" );
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show( "Chiffres uniquement" );
                            }

                        }

                        // nous sommes en "Modifier"
                        if( checkCode == false )
                        {
                            
                            //Debug.WriteLine( ObsCodeRegime.libelle.ToString() );
                            //Debug.WriteLine( !Regex.IsMatch( ObsCodeRegime.libelle.ToString(), @"^[a-zA-Z 0-9]+$" ) );

                            // si libellé n'est pas vide et que couleur n'est pas égale à 0
                            if( ObsCodeRegime.libelle == "" || ObsCodeRegime.couleur == 0 )
                            {
                                System.Windows.MessageBox.Show( "Veuillez remplir l'ensemble des champs avec une étoile * " );
                            }
                            else if( !Regex.IsMatch( ObsCodeRegime.libelle.ToString(), @"^[a-zA-Z 0-9]+$" ) == true)
                            {
                                System.Windows.MessageBox.Show( "caractère spéciaux interdits (lettres et chiffres uniquement)" );
                            }
                            else if( ObsCodeRegime.libelle != "" && ObsCodeRegime.couleur != 0 && !Regex.IsMatch( ObsCodeRegime.libelle.ToString(), @"^[a-zA-Z 0-9]+$" ) == false)
                            {
                                existe.LIBELLE_CodeRegime = ObsCodeRegime.libelle;
                                existe.COULEUR_CodeRegime = ObsCodeRegime.couleur;

                                pg.SaveChanges();
                                ObsCodeRegime.AllowExit = true;
                                val = false;

                                System.Windows.MessageBox.Show( "Modifications enregistrées" );

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
            } );

            // commande gérant la reception du changement de couleur de WPFCodeRegimeAjouter
            couleurChange = new RelayCommand( () =>
            {
                ObsCodeRegime.couleur = ColorToBase( couleurSelectionnee );
            } );
        }
          


        //////////////////////////////////////
        //  Gestion de l'affichage de l'aide
        //////////////////////////////////////  

        private void AffichageAide( string AideAAfficher )
        {
            string NomServeur;
            using( ProgetEntities pg = new ProgetEntities() )
            {
                NomServeur = pg.ParamApplis.SingleOrDefault().serveur_Name;
                AideAAfficher = @"\\" + NomServeur + @"\Proget\Get-MS\pdf\" + AideAAfficher;

                if( File.Exists( AideAAfficher ) )
                {
                    ProcessStartInfo psi = new ProcessStartInfo( AideAAfficher, "" );
                    Process.Start( psi );
                }
                else
                {
                    System.Windows.MessageBox.Show( "Le fichier d'aide n'existe pas" );
                }
            }
        }


        ///////////////////////////////////////////////////////////////
        //  Gestion de l'affichage de la ListView (affichage logique)
        ///////////////////////////////////////////////////////////////            

        public void ChargementListView()
        {
            //on initialise l'accès à la base de donnée (Projet3Entities est le nom de la classe porteuse du dbcontxt dans Projet3BDD.Context.cs)
            using( ProgetEntities pg = new ProgetEntities() )
            {
                // on place immédiatement le résultat de la requête dans une liste (on n'affiche que les Codes dont la propriété est différente de 0)
                List<CodeRegime> ListeCode = pg.CodeRegimes.Where( a => a.SUPPRIMER_CodeRegime == false ).ToList(); ;

                // récupérons la liste générée par WPFCodeRegime
                ObservableCollection<CodeRegime> ListViewListeRecup = new ObservableCollection<CodeRegime>( ListeCode );

                // on instancie la listViewList
                ListViewListe = new ObservableCollection<CodeRegimeObservable>();

                // parcourons la liste pour en extraire les éléments
                foreach( CodeRegime item in ListViewListeRecup )
                {
                    //on passe les données à la ListView                     
                    ListViewListe.Add( new CodeRegimeObservable( item.IDCodeRegime, item.LIBELLE_CodeRegime, item.COULEUR_CodeRegime, item.SUPPRIMER_CodeRegime ) );
                }
                if( CbxChecked == true )
                {
                    CbxChecked = false;
                }

            }
        }


        //////////////////////////
        //  Gestion des couleurs
        //////////////////////////  

        /// <summary>
        /// Convertit une Color en format compatible avec la BDD
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int ColorToBase( Color value )
        {
            System.Drawing.Color drawingColor = new System.Drawing.Color();
            drawingColor = System.Drawing.Color.FromArgb( value.A, value.R, value.G, value.B );
            return ( drawingColor.ToArgb() );
        }

        /// <summary>
        /// Convertit le format de couleur de la BDD en Color
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Color BaseToColor( int value )
        {
            System.Drawing.Color DrawingColor = new System.Drawing.Color();
            DrawingColor = System.Drawing.Color.FromArgb( Convert.ToInt32( value ) );

            Color mediaColor = new Color();
            mediaColor = Color.FromArgb( DrawingColor.A, DrawingColor.R, DrawingColor.G, DrawingColor.B );

            return mediaColor;
        }
        /// <summary>
        /// permet l'affichage de couleur dans le xaml
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SolidColorBrush ColorBaseToBrush( string value )
        {
            System.Drawing.Color DrawingColor = new System.Drawing.Color();
            DrawingColor = System.Drawing.Color.FromArgb( Convert.ToInt32( value ) );

            Color mediaColor = new Color();
            mediaColor = Color.FromArgb( DrawingColor.A, DrawingColor.R, DrawingColor.G, DrawingColor.B );

            return new SolidColorBrush( mediaColor );
        }

        
        // méthode gérant la fermeture des fenetres
        private void FermetureFenetre( Window fenetre )
        {
            fenetre.Close();
            ChargementListView();
        }

        //////////////////////////////
        //  ACCES A LA BASE DE DONNEE
        //////////////////////////////

        /// <summary>
        /// méthode récupérant tous les établissements dans la BDD (grace à entity)
        /// </summary>
        /// <returns></returns>
        private static List<Etablissement> SelectTousEtablissements()
        {
            //on initialise l'accès à la base de donnée (Projet3Entities est le nom de la classe porteuse du dbcontxt dans Projet3BDD.Context.cs)
            using( ProgetEntities pg = new ProgetEntities() )
            {

                // on place immédiatement le résultat de la requête dans une liste
                List<Etablissement> ListeEtab = pg.Etablissements.ToList();

                // on retourne le résultat de la requête
                return ListeEtab;
            }
        }

        //////////////////////////////////////
        //  Gestion des RaisePropertyChanged
        //////////////////////////////////////

        /// <summary>
        /// variable controlant l'affichage du bouton ajouter en fonction des droits de l'utilisateur
        /// </summary>
        private bool _ControleDroit;
        public bool ControleDroit
        {
            get
            {
                return _ControleDroit;
            }
            set
            {
                if( value != _ControleDroit )
                {
                    _ControleDroit = value;
                    RaisePropertyChanged( "ControleDroit" );
                }
            }
        }

        public class EtablissementObjetObservable : ObservableObject
        {
            public EtablissementObjetObservable( string IDEtablissement )
            {
                this.IDEtablissement = IDEtablissement;
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
                    if( value != _IDEtablissement )
                    {
                        _IDEtablissement = value;
                        RaisePropertyChanged( "IdEtablissement" );
                    }
                }
            }
        }

        //}



        public class CodeRegimeObservable : ObservableObject
        {

            //Declaration d'un booleen gerant si l'on peut sortir sans message d'avertissement
            //A chaque modification d'un attribut, on le passe a faux
            private bool _AllowExit;
            public bool AllowExit
            {
                get
                {
                    return _AllowExit;
                }
                set
                {
                    if( value != _AllowExit )
                    {
                        _AllowExit = value;
                        RaisePropertyChanged( "AllowExit" );
                    }
                }
            }

            //Déclaration de l'id
            private string _id;
            public string id
            {
                get
                {
                    return _id;
                }
                set
                {
                    if( value != _id )
                    {
                        _id = value;
                        RaisePropertyChanged( "id" );
                        this.AllowExit = false;
                    }
                }
            }

            //Déclaration de la variable libellé
            private string _libelle;
            public string libelle
            {
                get
                {
                    return _libelle;
                }
                set
                {
                    if( value != _libelle )
                    {
                        _libelle = value;
                        RaisePropertyChanged( "libelle" );
                        this.AllowExit = false;
                    }
                }
            }

            //Déclaration de la couleur
            private int? _couleur;
            public int? couleur
            {
                get
                {
                    return _couleur;
                }
                set
                {
                    if( value != _couleur )
                    {
                        _couleur = value;
                        RaisePropertyChanged( "couleur" );
                        this.AllowExit = false;
                    }
                }
            }

            //Déclaration de la variable supprimer
            private bool? _supprimer;
            public bool? supprimer
            {
                get
                {
                    return _supprimer;
                }
                set
                {
                    if( value != supprimer )
                    {
                        _supprimer = value;
                        RaisePropertyChanged( "supprimer" );
                        this.AllowExit = false;
                    }
                }
            }


            ///gestion es couleurs affichées
            ///
            private SolidColorBrush _couleurVraie;
            public SolidColorBrush couleurVraie
            {
                get
                {
                    return _couleurVraie;
                }
                set
                {
                    if( value != _couleurVraie )
                    {
                        _couleurVraie = value;
                        RaisePropertyChanged( "couleurVraie" );
                    }
                }
            }

            /// <summary>
            /// booléen ramenant la valeur de l'état de Check de la CheckBox "tous les Grands Régimes"
            /// </summary>
            private bool _CbxChecked;
            public bool CbxChecked
            {
                get
                {
                    return _CbxChecked;
                }
                set
                {
                    if( value != _CbxChecked )
                    {
                        _CbxChecked = value;
                        RaisePropertyChanged( "CbxChecked" );
                    }
                }
            }


            ///<summary>
            ///constructeur de la classe
            ///</summary>
            ///<param name ="id"></param>
            ///<param name="libelle"></param>
            ///<param name="couleur"></param>
            ///<param name="supprimer"></param>
            public CodeRegimeObservable( string id, string libelle, int? couleur, bool? supprimer )
            {
                this.id = id;
                this.libelle = libelle;
                this.couleur = couleur;
                this.supprimer = supprimer;
                this.AllowExit = true;
                //couleurVraie = ColorBaseToBrush( "-16776961" );
                if(couleur == null )
                {
                    couleurVraie = ColorBaseToBrush( "0" );
                }
                else
                {
                    couleurVraie = ColorBaseToBrush( couleur.ToString() );
                }
                


            }      


            public CodeRegimeObservable()
            {
                id = "";
                libelle = "";
                couleur = 0;
                supprimer = false;
                this.AllowExit = true;
                Debug.WriteLine( couleur.ToString() );


            }

            public CodeRegimeObservable( CodeRegime coderegime )
            {
                this.id = coderegime.IDCodeRegime;
                this.libelle = coderegime.LIBELLE_CodeRegime;
                this.couleur = coderegime.COULEUR_CodeRegime;
                this.supprimer = coderegime.SUPPRIMER_CodeRegime;
                this.AllowExit = true;

                Debug.WriteLine( couleur.ToString() );
            }

            /// <summary>
            /// méthode de conversion d'un ObjetObservable en Coderegime
            /// </summary>
            /// <param name="ObjetObservable"></param>
            /// <returns></returns>
            public static CodeRegime ConversionEnCodeRegime( CodeRegimeObservable ObjetObservable )
            {
                CodeRegime codeRegime = new CodeRegime();

                codeRegime.IDCodeRegime = ObjetObservable.id;
                codeRegime.LIBELLE_CodeRegime = ObjetObservable.libelle;
                codeRegime.COULEUR_CodeRegime = ObjetObservable.couleur;               
                codeRegime.SUPPRIMER_CodeRegime = ObjetObservable.supprimer;

                return codeRegime;
            }


        }

    }
    /*
    /////////////////////////////////////////////////////
    //  CONTROLE DE SAISIE DU FORMULAIRE WPFCODEREGIMEAJOUTER
    /////////////////////////////////////////////////////
    /// <summary>
    /// méthode gérant le contrôle de saisie du code
    /// </summary>
    public class IdCodeRegime : ValidationRule
    {
        public override ValidationResult Validate( object value, CultureInfo cultureInfo )
        {
            
            if( value == null )
            {
                return new ValidationResult( false, "Veuillez saisir une valeur" );
            }
            else
            {
                WPFCodeRegimeViewModel.val = true;
                if( !Regex.IsMatch( value.ToString(), @"^[a-zA-Z0-9]+$" ) )
                {
                    return new ValidationResult( false, "caractère spéciaux interdits" );
                }
            }
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// méthode gérant le contrôle de saisie du libellé Code Régime
    /// </summary>
    public class LibelleCoderegime : ValidationRule
    {
        public override ValidationResult Validate( object value, CultureInfo cultureInfo )
        {
            if( value == null )
            {
                return new ValidationResult( false, "Veuillez saisir une valeur" );
            }
            else
            {
                WPFCaisseSecusViewModel.val = true;
                if( !Regex.IsMatch( value.ToString(), @"^[a-zA-Z0-9]+$" ) )
                {
                    return new ValidationResult( false, "caractère spéciaux interdits" );
                }
            }
            return ValidationResult.ValidResult;
        }
    }
    */
    /// <summary>
    /// Classe interne
    /// Structure de données des composants de la ListView, selon la table Emplois
    /// Chaque emploi dans la liste est représenté par une instance de cette classe
    /// </summary>
    public class ListViewCodeRegime : System.Windows.DependencyObject
    {

        //// propriété ID
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register( "ID", typeof( string ),
            typeof( ListViewCodeRegime ), new UIPropertyMetadata( "0" ) );

        public string ID
        {
            get { return (string)GetValue( IDProperty ); }
            set { SetValue( IDProperty, value ); }
        }

        // propriété libelle
        public static readonly DependencyProperty LibelleProperty =
            DependencyProperty.Register( "Libelle", typeof( string ),
            typeof( ListViewCodeRegime ), new UIPropertyMetadata( "" ) );

        public string Libelle
        {
            get { return (string)GetValue( LibelleProperty ); }
            set { SetValue( LibelleProperty, value ); }
        }

        // propriété supprimer
        public static readonly DependencyProperty SupprimerProprety =
            DependencyProperty.Register( "Supprimer", typeof( bool ),
            typeof( ListViewCodeRegime ), new UIPropertyMetadata( false ) );

        public bool Supprimer
        {
            get { return (bool)GetValue( SupprimerProprety ); }
            set { SetValue( SupprimerProprety, value ); }
        }       

        // propriété droit d'accès
        public static readonly DependencyProperty DroitProprety =
            DependencyProperty.Register( "Droit", typeof( bool ),
            typeof( ListViewCodeRegime ), new UIPropertyMetadata( false ) );

        public bool Droit
        {
            get { return (bool)GetValue( DroitProprety ); }
            set { SetValue( DroitProprety, value ); }
        }


    }
}