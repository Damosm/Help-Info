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
    
    public partial class Contact
    {
        public string Code_Contact { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string TypePersonne { get; set; }
        public string AdresseCabinet { get; set; }
        public string Code_Postal { get; set; }
        public string email { get; set; }
        public string Tel { get; set; }
        public string Portable { get; set; }
        public string Sexe { get; set; }
        public string Ville { get; set; }
        public bool Supprimer { get; set; }
    }
}