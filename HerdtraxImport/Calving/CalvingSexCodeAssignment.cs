using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public class CalvingSexCodeAssignment : ICalvingSexCodeAssignment
    {
        public void SetSexCode(IEnumerable<Herd> herds)
        {
            foreach (var herd in herds)
            {
                foreach (var rawCalf in herd.Calves)
                {
                    switch (rawCalf.Gender)
                    {
                        case "Calf-M":
                            rawCalf.SexCode = "M";
                            break;
                        case "Calf-F":
                            rawCalf.SexCode = "F";
                            break;
                        //default:
                        //    rawCalf.SexCode = string.Empty;
                    }
                }
            }
        }
    }
}