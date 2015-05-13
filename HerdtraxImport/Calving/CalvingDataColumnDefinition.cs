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
        public string DamTagLetter { get; set; }
        public string DamTagNumber { get; set; }

        public string RegistrationNumber { get; set; }
        public string CCIA { get; set; }
        public string VID { get; set; }
        public string Group { get; set; }
        public string Gender { get; set; }

        public string TagLetter { get; set; }
        public string TagNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int BirthWt { get; set; }

        public string TwinType { get; set; }
        public int TwinAnimalId { get; set; }

    }


}