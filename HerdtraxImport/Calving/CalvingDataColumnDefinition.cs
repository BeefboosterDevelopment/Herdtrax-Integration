using System;
using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public class CalvingDataColumnDefinition
    {
        public CalvingDataColumnDefinition(CalvingColumn column, int index)
        {
            Column = column;
            Index = index;
        }

        public CalvingColumn Column { get; private set; }
        public int Index { get; private set; }
    }


    public class Herd
    {
        public int HerdSN { get; set; }
        public List<RawCalf> Calves { get; set; }
    }


    public class RawCalf
    {
        public int HerdtraxAnimalId { get; set; }
        public int CalfSN { get; set; }
        public int DamSN { get; set; }

        public string SexCode { get; set; }
        public bool DoNotImport { get; set; }
        public string NotImportedReason { get; set; }

        public string DamRegistrationNumber { get; set; }
        public string DamVID { get; set; }
        public string DamTagNumber { get; set; }
        public string DamTagLetter { get; set; }
        public string DamTagColor { get; set; }

        public string RegistrationNumber { get; set; }
        public string CCIA { get; set; }
        public string VID { get; set; }
        public string Group { get; set; }
        public string Gender { get; set; }

        public string TagNumber { get; set; }
        public string TagLetter { get; set; }
        public string TagColor { get; set; }
        public string DNATag { get; set; }
        public DateTime BirthDate { get; set; }
        public int BirthWt { get; set; }
        public int EaseScore { get; set; }
        public int HoofScore { get; set; }
        public int UdderScore { get; set; }

        public string TwinType { get; set; }
        public string SurrogateTagNumber { get; set; }
        public string SurrogateTagLetter { get; set; }
        public string SurrogateTagColor { get; set; }
        public int SurrogateDam_SN { get; set; }
        public int SiblingCalfAnimalId { get; set; }
    }
}