using System.Collections.Generic;
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
            List<Herd> herdList = herds.ToList();
            AssignCalfSN(herdList);

            
            foreach (Herd herd in herdList)
            {
                foreach (RawCalf rawCalf in herd.Calves.Where(c => !c.DoNotImport).ToList())
                {
                    _model.tblCalves.Add(_calfBuilder.BuildFromRawCalf(rawCalf, herd));
                }
            }
            return _model.SaveChanges();
        }

        private void AssignCalfSN(List<Herd> herdList)
        {
            int numCalves = herdList.Sum(h => h.Calves.Count(c => !c.DoNotImport));
            int startingSN = _model.ReserveSNRange(numCalves);
            foreach (Herd herd in herdList)
                foreach (RawCalf c in herd.Calves.Where(c => !c.DoNotImport))
                {
                    c.CalfSN = startingSN++;
                }
            //return --startingSN;
/*            foreach (Herd herd in herdList)
            {
                var l = herd.Calves.Where(h => h.CalfSN==0);
            }*/
        }
    }
}