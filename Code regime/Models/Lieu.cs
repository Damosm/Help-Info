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
    
    public partial class Lieu
    {
        public string IDLieu { get; set; }
        public string NOM_Lieu { get; set; }
        public string ADRESSE_Lieu { get; set; }
        public string CP_Lieu { get; set; }
        public string VILLE_Lieu { get; set; }
        public string TELEPHONE_Lieu { get; set; }
        public int COULEUR_Lieu { get; set; }
        public bool SUPPRIMER_Lieu { get; set; }
        public string IDEtablissement { get; set; }
    }
}