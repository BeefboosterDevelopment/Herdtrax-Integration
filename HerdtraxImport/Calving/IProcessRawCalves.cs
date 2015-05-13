using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public interface IProcessRawCalves
    {
        List<Herd> SortCalvesIntoBeefboosterHerds(List<RawCalf> listOfRawCalves);
    }
}