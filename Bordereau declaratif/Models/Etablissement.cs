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
    
    public partial class Etablissement
    {
        public string IDEtablissement { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Adresse_suite { get; set; }
        public string Code_Postal { get; set; }
        public string Ville { get; set; }
        public string Tél { get; set; }
        public string Fax { get; set; }
        public string Site_Web { get; set; }
        public bool Droit_Local { get; set; }
        public System.DateTime Début_période { get; set; }
        public System.DateTime Fin_période { get; set; }
        public string CheminPhoto { get; set; }
        public string IDOrganisation { get; set; }
        public string IDCalendrier { get; set; }
        public bool Supprimer { get; set; }
    }
}
