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
    
    public partial class HistoRCF
    {
        public string Matricule { get; set; }
        public System.DateTime Jour_RCF { get; set; }
        public Nullable<System.DateTime> Jour_Férié { get; set; }
        public Nullable<decimal> Nb_heures { get; set; }
    }
}
