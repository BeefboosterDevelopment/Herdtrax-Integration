using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class WriteToDatababase : IWriteToDatababase
    {
        private readonly ICalvingCalfBuilder _calfBuilder;
        private readonly BBModel _model;

        public WriteToDatababase(BBModel model, ICalvingCalfBuilder calfBuilder)
        {
            _model = model;
            _calfBuilder = calfBuilder;
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
                    ImportHerd(herd);
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


        private void ImportHerd(Herd herd)
        {
            List<RawCalf> calvesToImport = herd.Calves.Where(c => !c.DoNotImport).ToList();
            foreach (RawCalf rawCalf in calvesToImport)
            {
                tblCalf newCalf = _calfBuilder.BuildFromRawCalf(rawCalf, herd);
                _model.tblCalves.Add(newCalf);
            }
            ;
        }
    }
}