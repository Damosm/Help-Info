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
    
    public partial class CodificationTypesJour
    {
        public string Categorie { get; set; }
        public string Type_Jour { get; set; }
        public string description { get; set; }
        public string Code_Famille { get; set; }
        public string Genre { get; set; }
        public bool RTT { get; set; }
        public Nullable<decimal> Quota_RTT { get; set; }
        public Nullable<short> SeuilRTT { get; set; }
        public Nullable<short> TAM { get; set; }
        public Nullable<decimal> Quota_Supp { get; set; }
        public Nullable<short> Carence_Supp { get; set; }
        public Nullable<short> Unité { get; set; }
        public Nullable<byte> Valorisation { get; set; }
        public bool Motif_remplacement { get; set; }
        public Nullable<int> Couleur { get; set; }
        public string Commentaire { get; set; }
        public bool Supprimer { get; set; }
        public bool Matrice { get; set; }
        public Nullable<short> Ordre { get; set; }
    }
}