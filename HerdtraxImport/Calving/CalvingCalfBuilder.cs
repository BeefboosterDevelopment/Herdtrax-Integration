using System;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class CalvingCalfBuilder : ICalvingCalfBuilder
    {
        public tblCalf BuildFromRawCalf(RawCalf rawCalf, Herd herd)
        {
            var calfBirthYear = (short) (rawCalf.BirthDate.Year);
            var newCalf = new tblCalf
            {
                Calf_SN = rawCalf.CalfSN,
                Dam_SN = rawCalf.DamSN,
                CalfHerd_SN = herd.HerdSN,
                HerdtraxAnimalId = rawCalf.HerdtraxAnimalId,
                CalfBirthYr_Num = calfBirthYear,
                CalfTag_Num = int.Parse(rawCalf.TagNumber),
                CalfYr_Code = YearLetterCode(calfBirthYear),
                DNA_Tag = string.IsNullOrEmpty(rawCalf.DNATag) ? (int?) null : int.Parse(rawCalf.DNATag),
                Birth_Date = rawCalf.BirthDate,
                Birth_Wt = rawCalf.BirthWt == 0 ? (short?) null : (short) rawCalf.BirthWt,
                Sex_Code = rawCalf.SexCode,
                Cull_Pull_Flag = rawCalf.EaseScore > 0,
                Udder_Score = rawCalf.UdderScore == 0 ? (byte?) null : (byte) rawCalf.UdderScore,
            };

            // Twin stuff
            if (!string.IsNullOrEmpty(rawCalf.TwinType))
            {
                newCalf.Twin_Flag = true;
                newCalf.HerdtraxTwinType = rawCalf.TwinType;

                if (rawCalf.SiblingCalfAnimalId > 0)
                {
                    RawCalf sib = herd.Calves.First(c => c.HerdtraxAnimalId == rawCalf.SiblingCalfAnimalId);
                    newCalf.CalfTwin_SN = sib.CalfSN;
                }

                if (rawCalf.SurrogateDam_SN > 0)
                {
                    //RawCalf sib = herd.Calves.First(c => c.HerdtraxAnimalId == rawCalf.SiblingCalfAnimalId);
                    //newCalf.CalfTwin_SN = sib.CalfSN;
                    newCalf.GraftedFromDam_SN = rawCalf.SurrogateDam_SN;
                    newCalf.Grafted_Flag = true;
                }
            }
            return newCalf;
        }


        private string YearLetterCode(short yr)
        {
            switch (yr)
            {
                case 2015:
                    return "C";
                case 2016:
                    return "D";
                case 2017:
                    return "E";
                case 2018:
                    return "F";
                default:
                    throw new Exception("No year letter defined for birth year");
            }
        }
    }
}