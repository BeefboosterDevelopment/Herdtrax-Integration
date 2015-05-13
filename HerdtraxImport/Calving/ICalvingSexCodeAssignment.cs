using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public interface ICalvingSexCodeAssignment
    {
        void SetSexCode(IEnumerable<Herd> herds);
    }
}