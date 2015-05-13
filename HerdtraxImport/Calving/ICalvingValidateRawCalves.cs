using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public interface ICalvingValidateRawCalves
    {
        List<string> Validate(IEnumerable<Herd> herds);
    }
}