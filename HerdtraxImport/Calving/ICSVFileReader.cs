using System.Collections.Generic;
using HerdtraxImport.Calving;

namespace HerdtraxImport
{
    public interface ICSVFileReader
    {
        List<RawCalf> DigestFile(string fileName);
    }
}