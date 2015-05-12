using System;
using System.Collections.Generic;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class CalvingImportRawCalves
    {
        private readonly BBModel _model;

        public CalvingImportRawCalves(BBModel model)
        {
            _model = model;
        }


        private static string YearLetterCode(short yr)
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
                default:throw new Exception("No year letter defined for birth year");

            }

        }


        private int ImportOneHerd(Herd herd)
        {
            var counter = 0;
            var numberOfNewCalfSerialNumbersNeeded = herd.Calves.Count(c => c.CalfSN == 0);
            var lastSN = _model.ReserveSNRange(numberOfNewCalfSerialNumbersNeeded);
            var nextSN = lastSN - numberOfNewCalfSerialNumbersNeeded;

            var calvesToImport = herd.Calves.Where(c => !c.DoNotImport).ToList();


            foreach (var rawCalf in calvesToImport)
            {
                var calfBirthYear = (short)(rawCalf.BirthDate.Year);

                // Insert one calf
                _model.tblCalves.Add(new tblCalf
                {
                    Calf_SN = nextSN,
                    Dam_SN = rawCalf.DamSN,
                    CalfHerd_SN = herd.HerdSN,
                    HerdtraxAnimalId = rawCalf.HerdtraxAnimalId,
                    CalfBirthYr_Num = calfBirthYear,
                    CalfTag_Num = int.Parse(rawCalf.TagNumber),
                    CalfYr_Code = YearLetterCode(calfBirthYear),
                    Birth_Date = rawCalf.BirthDate,
                    Birth_Wt = (short)rawCalf.BirthWt,
                    Sex_Code = rawCalf.SexCode,
                });
                counter++;
            }
            return counter;
        }

        public int Import(IEnumerable<Herd> herds)
        {
            var totalImportCount = 0;

            foreach (var herd in herds)
            {
                totalImportCount += ImportOneHerd(herd);
            }


            var saveChangesResult = _model.SaveChanges();
            return totalImportCount;
        }
    }
}