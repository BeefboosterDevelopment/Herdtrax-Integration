using System.Collections.Generic;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class CalvingTwinProcessing : ICalvingTwinProcessing
    {
        private readonly BBModel _bbModel;

        public CalvingTwinProcessing(BBModel bbModel)
        {
            _bbModel = bbModel;
        }

        public void Process(IEnumerable<Herd> herds)
        {
            foreach (Herd herd in herds)
            {
                List<RawCalf> twinCalfList = herd.Calves.Where(calf => !string.IsNullOrEmpty(calf.TwinType)).ToList();
 
                // Set sibling 
                foreach (RawCalf calf in twinCalfList)
                {
                    RawCalf tc = calf;

                    // Sibling is calf with same mother and different animal id
                    RawCalf sibling =
                        herd.Calves.FirstOrDefault(c => c.HerdtraxAnimalId != tc.HerdtraxAnimalId && c.DamSN == tc.DamSN);
                    if (sibling != null)
                    {
                        calf.SiblingCalfAnimalId = sibling.HerdtraxAnimalId;
                        //sibling.SiblingCalfAnimalId = calf.HerdtraxAnimalId;
                    }
                }


                // Set Surrogate mother 
                foreach (RawCalf calf in twinCalfList)
                {
                    int herdSN = herd.HerdSN;
                    RawCalf gc = calf;

                    tblDam surrogateDam = _bbModel.tblDams.FirstOrDefault(
                        d =>
                            d.DamHerd_SN == herdSN && d.DamTag_Str == gc.SurrogateTagNumber &&
                            d.DamYr_Code == gc.SurrogateTagLetter);
                    if (surrogateDam != null)
                    {
                        calf.SurrogateDam_SN = surrogateDam.Dam_SN;
                    }
                }
            }
        }
    }
}