using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public interface ICalvingTwinProcessing
    {
        void Process(IEnumerable<Herd> herds);
    }
}