using BBDM;

namespace HerdtraxImport.Calving
{
    public interface ICalvingCalfBuilder
    {
        tblCalf BuildFromRawCalf(RawCalf raw, Herd herd);
    }
}