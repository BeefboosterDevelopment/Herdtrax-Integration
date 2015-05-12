using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public class CalvingDoNoImport
    {

        public void DoNotImport(List<RawCalf> calves)
        {
            foreach (var rawCalf in calves)
            {
                if (rawCalf.Group == "Yard Cows")
                {
                    rawCalf.DoNotImport = true;
                    rawCalf.NotImportedReason = string.Format("ID:{0} - Management Group is not a breeding strain {1}",
                        rawCalf.HerdtraxAnimalId, rawCalf.Group);
                }

            }
        } 

    }
}