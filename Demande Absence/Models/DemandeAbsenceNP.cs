using GalaSoft.MvvmLight;
using System;

namespace ClassGetMS.Models
{
    public class DemandeAbsenceNP : ObservableObject
    {
        #region class demande absence avec nom et prenom
        
            public string Matricule { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public System.DateTime Date_debut { get; set; }
            public System.DateTime Date_fin { get; set; }
            public string Type_Jour { get; set; }
            public string Commentaire_agent { get; set; }
            public string Commentaire_gestionnaire { get; set; }
            public string Etat { get; set; }
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

            public DemandeAbsenceNP() { }
            public DemandeAbsenceNP(string Matricule, string Nom, string Prenom, DateTime Date_debut, DateTime Date_fin,
                string Type_Jour, string Commentaire_agent, string Commentaire_gestionnaire, string Etat)
            {
                this.Matricule = Matricule;
                this.Nom = Nom;
                this.Prenom = Prenom;
                this.Date_debut = Date_debut;
                this.Date_fin = Date_fin;
                this.Type_Jour = Type_Jour;
                this.Commentaire_agent = Commentaire_agent;
                this.Commentaire_gestionnaire = Commentaire_gestionnaire;
                this.Etat = Etat;
            }

            public DemandeAbsenceNP(string Matricule, string Nom, string Prenom, DateTime Date_debut, DateTime Date_fin,
                string Type_Jour)
            {
                this.Matricule = Matricule;
                this.Nom = Nom;
                this.Prenom = Prenom;
                this.Date_debut = Date_debut;
                this.Date_fin = Date_fin;
                this.Type_Jour = Type_Jour;

            }

        
        #endregion







    }
}