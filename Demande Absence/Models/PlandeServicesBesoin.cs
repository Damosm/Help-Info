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
    
    public partial class PlandeServicesBesoin
    {
        public string PlanService { get; set; }
        public int Roulement { get; set; }
        public string Type_Jour { get; set; }
        public string Type_Prise1 { get; set; }
        public Nullable<System.DateTime> Heure_début1 { get; set; }
        public Nullable<System.DateTime> Heure_fin1 { get; set; }
        public string Pause1 { get; set; }
        public string Type_Prise2 { get; set; }
        public Nullable<System.DateTime> Heure_début2 { get; set; }
        public Nullable<System.DateTime> Heure_fin2 { get; set; }
        public string Pause2 { get; set; }
        public string Type_Prise3 { get; set; }
        public Nullable<System.DateTime> Heure_début3 { get; set; }
        public Nullable<System.DateTime> Heure_fin3 { get; set; }
        public Nullable<System.DateTime> Durée { get; set; }
        public string Type_Durée { get; set; }
        public bool Nuit { get; set; }
        public Nullable<int> NBAgents { get; set; }
        public bool Lundi { get; set; }
        public bool Mardi { get; set; }
        public bool Mercredi { get; set; }
        public bool Jeudi { get; set; }
        public bool Vendredi { get; set; }
        public bool Samedi { get; set; }
        public bool Dimanche { get; set; }
    }
}