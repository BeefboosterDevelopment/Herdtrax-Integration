using System.Collections.Generic;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class CalvingHerdAssignment
    {
        private readonly BBModel _model;
        private List<Herd> _herds;

        public CalvingHerdAssignment(BBModel model)
        {
            _model = model;
        }

        private List<Herd> Herds
        {
            get { return _herds ?? (_herds = new List<Herd>()); }
        }

        private void AddToHerd(int sn, RawCalf calf)
        {
            var toHerd = Herds.FirstOrDefault(h => h.HerdSN == sn);

            if (toHerd == null)
            {
                Herds.Add(new Herd
                {
                    Calves = new List<RawCalf>(),
                    HerdSN = sn
                });
                toHerd = Herds.First(h => h.HerdSN == sn);
            }

            toHerd.Calves.Add(calf);
        }

        public List<Herd> GroupByHerd(IEnumerable<RawCalf> calves)
        {
            foreach (var rawCalf in calves.Where(c => !c.DoNotImport))
            {
                AddToHerd(_model.LookupDamHerd(rawCalf.DamSN), rawCalf);
            }

            return Herds.OrderBy(h => h.HerdSN).ToList();
        }
    }
}