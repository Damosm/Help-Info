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
    
    public partial class Matrix
    {
        public string IDMatrice { get; set; }
        public string IDEtablissement { get; set; }
        public int Num_jour_matrice { get; set; }
        public string IDRegime { get; set; }
        public Nullable<float> Tps_Base { get; set; }
        public string Nom { get; set; }
        public Nullable<int> Num_jour_sem { get; set; }
        public string Type_jour { get; set; }
        public Nullable<System.DateTime> deb_prise_1 { get; set; }
        public Nullable<System.DateTime> fin_prise_1 { get; set; }
        public string pause_1 { get; set; }
        public string Repas1 { get; set; }
        public string Type_prise_1 { get; set; }
        public Nullable<System.DateTime> deb_prise_2 { get; set; }
        public Nullable<System.DateTime> fin_prise_2 { get; set; }
        public string pause_2 { get; set; }
        public string Repas2 { get; set; }
        public string type_prise_2 { get; set; }
        public Nullable<System.DateTime> deb_prise_3 { get; set; }
        public Nullable<System.DateTime> fin_prise_3 { get; set; }
        public string Repas3 { get; set; }
        public string type_prise_3 { get; set; }
        public Nullable<System.DateTime> Durée { get; set; }
        public string type_durée { get; set; }
        public bool nuit { get; set; }
        public bool MultiEtablissement { get; set; }
        public Nullable<int> couleur { get; set; }
        public string Description { get; set; }
        public string IDService { get; set; }
        public string IDSection { get; set; }
    }
}
