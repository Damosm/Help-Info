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
    
    public partial class GestionDocument
    {
        public string IDDOCUMENT { get; set; }
        public string Code_Usager { get; set; }
        public string NOM_DOCUMENT { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
        public byte[] LOCALISATION_DOCUMENT { get; set; }
        public Nullable<bool> SUPPRIMER_DOCUMENT { get; set; }
        public Nullable<int> COULEUR_DOCUMENT { get; set; }
    }
}
