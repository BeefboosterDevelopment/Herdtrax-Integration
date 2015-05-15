using System.Collections.Generic;

namespace HerdtraxImport
{
    public interface IImportCalving
    {
        List<string> Issues { get; set; }
        //List<RawCalf> Calves { get; set; }
        //List<Herd> Herds { get; set; }
        int NumberRowsChanged { get; set; }
        bool Import(string fileName);
    }
}