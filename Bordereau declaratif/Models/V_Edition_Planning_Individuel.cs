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
    
    public partial class V_Edition_Planning_Individuel
    {
        public System.DateTime Jour { get; set; }
        public string Num_Contrat { get; set; }
        public string Avenant { get; set; }
        public Nullable<System.DateTime> Date_début { get; set; }
        public Nullable<System.DateTime> Date_Fin { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> Temps_Base { get; set; }
        public string IDRégime { get; set; }
        public string Régime_Mod { get; set; }
        public Nullable<decimal> Base_contractuelle { get; set; }
        public bool RTT { get; set; }
        public bool travail_de_nuit { get; set; }
        public bool Règle_équivalence { get; set; }
        public bool MultiEtablissement { get; set; }
        public string Matricule_agent_remplace { get; set; }
        public string Motif_remplacement { get; set; }
        public string Convention_collective { get; set; }
        public string Num_contrat_base { get; set; }
        public string IDetablissement { get; set; }
        public string Matricule { get; set; }
        public string Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string Nom_jeune_fille { get; set; }
        public bool Cadre { get; set; }
        public string N_SS { get; set; }
        public string Matricule_paie { get; set; }
        public Nullable<int> Coeff_base { get; set; }
        public Nullable<int> Coeff_MAJ { get; set; }
        public Nullable<decimal> CS { get; set; }
        public bool Expr1 { get; set; }
        public string IDQualification { get; set; }
        public string IDEmploi { get; set; }
        public string IDFiliere { get; set; }
        public string IDPaie { get; set; }
        public Nullable<int> Num_semaine { get; set; }
        public Nullable<int> Num_jour_année { get; set; }
        public Nullable<int> Num_jour_Semaine { get; set; }
        public Nullable<int> Num_mois { get; set; }
        public Nullable<int> Num_Jour_matrice { get; set; }
        public Nullable<int> Cycle { get; set; }
        public string Type_Jour { get; set; }
        public Nullable<System.DateTime> Heure_début1 { get; set; }
        public Nullable<System.DateTime> Heure_fin1 { get; set; }
        public string Pause1 { get; set; }
        public string Repas1 { get; set; }
        public string Type_Prise1 { get; set; }
        public string IDEtablissement1 { get; set; }
        public string IDSection1 { get; set; }
        public string IDService1 { get; set; }
        public Nullable<int> Couleur1 { get; set; }
        public bool Visible1 { get; set; }
        public bool Verrouiller1 { get; set; }
        public Nullable<System.DateTime> Heure_début2 { get; set; }
        public Nullable<System.DateTime> Heure_fin2 { get; set; }
        public string Pause2 { get; set; }
        public string Repas2 { get; set; }
        public string Type_Prise2 { get; set; }
        public string IDEtablissement2 { get; set; }
        public string IDSection2 { get; set; }
        public string IDService2 { get; set; }
        public Nullable<int> Couleur2 { get; set; }
        public bool Visible2 { get; set; }
        public bool Verrouiller2 { get; set; }
        public Nullable<System.DateTime> Heure_début3 { get; set; }
        public Nullable<System.DateTime> Heure_fin3 { get; set; }
        public string Repas3 { get; set; }
        public string Type_Prise3 { get; set; }
        public string IDEtablissement3 { get; set; }
        public string IDSection3 { get; set; }
        public string IDService3 { get; set; }
        public Nullable<int> Couleur3 { get; set; }
        public bool Visible3 { get; set; }
        public bool Verrouiller3 { get; set; }
        public Nullable<System.DateTime> Durée { get; set; }
        public string Type_Durée { get; set; }
        public string IDEtablissement_Durée { get; set; }
        public string IDSection_Durée { get; set; }
        public string IDService_Durée { get; set; }
        public Nullable<int> Couleur_Durée { get; set; }
        public bool Visible_Durée { get; set; }
        public bool Verrouiller_Durée { get; set; }
        public bool Férié { get; set; }
        public bool nuit { get; set; }
        public Nullable<int> BackColor { get; set; }
        public string Commentaire { get; set; }
        public Nullable<System.DateTime> Date_MAJ { get; set; }
        public string IDMatrice { get; set; }
        public string Categorie1 { get; set; }
        public string Categorie2 { get; set; }
        public string CategorieD { get; set; }
        public string Categorie3 { get; set; }
        public string Libellé { get; set; }
        public string genre3 { get; set; }
        public string genre2 { get; set; }
        public string genre1 { get; set; }
        public string genreD { get; set; }
        public Nullable<decimal> TpsPauseSans { get; set; }
        public Nullable<decimal> TpsPauseAvec { get; set; }
        public Nullable<decimal> CA_N { get; set; }
        public Nullable<decimal> CA_N_1 { get; set; }
        public Nullable<decimal> TpsBase { get; set; }
        public Nullable<decimal> SeuilHSupp { get; set; }
        public Nullable<decimal> TpsW { get; set; }
        public Nullable<decimal> TpsAbs { get; set; }
        public Nullable<decimal> TpsAbsNP { get; set; }
        public Nullable<decimal> CPA { get; set; }
        public Nullable<decimal> CESS { get; set; }
        public Nullable<decimal> CA { get; set; }
        public Nullable<decimal> CumulRTT { get; set; }
        public Nullable<decimal> Nb_heures { get; set; }
        public bool Supprimer { get; set; }
        public string AgNom1 { get; set; }
        public string AgPrenom1 { get; set; }
    }
}
