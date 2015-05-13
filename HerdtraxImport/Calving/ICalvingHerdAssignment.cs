using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public interface ICalvingHerdAssignment
    {
        List<Herd> GroupByHerd(IEnumerable<RawCalf> calves);
    }
}