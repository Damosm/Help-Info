using System;
using System.Windows.Media;

namespace ClassGetMS.Models
{
    public class TypesJours : IComparable<TypesJours>
    {
        public int Categorie { get; set; }
        public EnumGenre Genre { get; set; }
        public string Famille { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
        public Brush BGColor { get; set; }
        public Brush FGColor { get; set; }
        public bool Supprime { get; set; }
        public bool Matrice { get; set; }
        public int? Ordre { get; set; }

        #region IComparable<DatePeriode>

        public int CompareTo(TypesJours typeJour)
        {
            if (this.Ordre != null && typeJour.Ordre != null)
            {
                if (this.Ordre > typeJour.Ordre)
                    return 1;
                else
                    return -1;
            }
            else
            {
                if (this.Ordre == null && typeJour.Ordre != null)
                    return 1;
                else if (typeJour.Ordre == null && this.Ordre != null)
                    return -1;
                else
                {
                    if (this.Categorie > typeJour.Categorie)
                        return 1;
                    else if (this.Categorie < typeJour.Categorie)
                        return -1;
                    else
                        return String.Compare(this.Libelle, typeJour.Libelle);
                }
            }
        }

        // Define the is greater than operator.
        public static bool operator >(TypesJours operand1, TypesJours operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        // Define the is less than operator.
        public static bool operator <(TypesJours operand1, TypesJours operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(TypesJours operand1, TypesJours operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(TypesJours operand1, TypesJours operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        #endregion
    }

    public class ParamMessage
    {
        //public ClassGetMS.StrucParam ParamGlobaux;
        //public string StMatricule;
        //public DateTime DtJour;
        public bool boolInitPlanning;
        public bool boolComputeWhenAssign = true;
    }

    //public class DataMessage
    //{
    //    public string StTypeJour;
    //    public string StTypePrise1;
    //    public string StTypePrise2;
    //    public string StTypePrise3;
    //    public string StTypeDuree;
    //    public string HeureDebPrise1;
    //    public string HeureFinPrise1;
    //    public string HeureDebPrise2;
    //    public string HeureFinPrise2;
    //    public string HeureDebPrise3;
    //    public string HeureFinPrise3;
    //    public string Duree;
    //    public string Pause1;
    //    public string Pause2;
    //    public string Repas1;
    //    public string Repas2;
    //    public string Repas3;
    //    public bool Nuit;
    //    public string Commentaire;
    //}
}
