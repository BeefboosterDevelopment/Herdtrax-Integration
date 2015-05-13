using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class WriteToDatababase : IWriteToDatababase
    {
        private readonly BBModel _model;

        public WriteToDatababase(BBModel model)
        {
            _model = model;
        }

        public int WriteCalfData(IEnumerable<Herd> herds)
        {
            int nRowsChanged = 0;
            DbContextTransaction transaction = _model.Database.BeginTransaction();

/*
            int numberOfNewCalfSerialNumbersNeeded = herds.Calves.Count(c => c.CalfSN == 0);
            int lastSN = _model.ReserveSNRange(numberOfNewCalfSerialNumbersNeeded);
            int nextSN = lastSN - numberOfNewCalfSerialNumbersNeeded + 1;
*/

            // assign the calf sn's


            try
            {
                foreach (Herd herd in herds)
                    ImportOneHerd(herd);
                nRowsChanged = _model.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                //throw;
            }
            return nRowsChanged;
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
                default:
                    throw new Exception("No year letter defined for birth year");
            }
        }


        private int ImportOneHerd(Herd herd)
        {
            int counter = 0;

            List<RawCalf> calvesToImport = herd.Calves.Where(c => !c.DoNotImport).ToList();

            foreach (RawCalf rawCalf in calvesToImport)
            {
                var calfBirthYear = (short) (rawCalf.BirthDate.Year);
                // Insert calf
                _model.tblCalves.Add(new tblCalf
                {
                    Calf_SN = rawCalf.CalfSN,
                    Dam_SN = rawCalf.DamSN,
                    CalfHerd_SN = herd.HerdSN,
                    HerdtraxAnimalId = rawCalf.HerdtraxAnimalId,
                    CalfBirthYr_Num = calfBirthYear,
                    CalfTag_Num = int.Parse(rawCalf.TagNumber),
                    CalfYr_Code = YearLetterCode(calfBirthYear),
                    Birth_Date = rawCalf.BirthDate,
                    Birth_Wt = (short) rawCalf.BirthWt,
                    Sex_Code = rawCalf.SexCode,
                    Twin_Flag = string.IsNullOrEmpty(rawCalf.TwinType)
                });
                counter++;
            }

            // process twins
            //CalfTwin_SN=0 

            return counter;
        }
    }
}