//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassGetMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlanningUsager
    {
        public string IDPlanningUsager { get; set; }
        public string CodeActe { get; set; }
        public string IDEmploiDuTemps { get; set; }
        public string Code_Usager { get; set; }
        public string IDLieu { get; set; }
        public System.DateTime JOUR_PlanningUsager { get; set; }
        public System.DateTime HEUREDEBUT_PlanningUsager { get; set; }
        public System.DateTime HEUREFIN_PlanningUsager { get; set; }
        public Nullable<int> NUMJOURSEMAINE_PlanningUsager { get; set; }
        public Nullable<int> NUMJOURANNEE_PlanningUsager { get; set; }
        public Nullable<int> NUMMOIS_PlanningUsager { get; set; }
        public Nullable<int> CYCLE_PlanningUsager { get; set; }
        public string COMMENTAIRE_PlanningUsager { get; set; }
        public Nullable<bool> HORSSITE_PlanningUsager { get; set; }
        public Nullable<int> COULEUR_PlanningUsager { get; set; }
        public Nullable<System.DateTime> DATEDERNIEREMAJ_PlanningUsager { get; set; }
        public Nullable<int> NUMSEMAINEMATRICE_PlanningUsager { get; set; }
        public string TACHE_PlanningUsager { get; set; }
        public string IDResponsabilite { get; set; }
        public string Matricule { get; set; }
        public string Code_Contact { get; set; }
    }
}
