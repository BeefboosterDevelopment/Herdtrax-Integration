using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public interface IWriteToDatababase
    {
        int WriteCalfData(IEnumerable<Herd> herds);
    }
}