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
    
    public partial class CaisseSecu
    {
        public string IDCaisseSecu { get; set; }
        public string NOM_CaisseSecu { get; set; }
        public string ADRESSE_CaisseSecu { get; set; }
        public string CP_CaisseSecu { get; set; }
        public string VILLE_CaisseSecu { get; set; }
        public string IDCaisseSecu_PARENTE { get; set; }
        public string TELEPHONE_CaisseSecu { get; set; }
        public Nullable<int> COULEUR_CaisseSecu { get; set; }
        public bool SUPPRIMER_CaisseSecu { get; set; }
    }
}