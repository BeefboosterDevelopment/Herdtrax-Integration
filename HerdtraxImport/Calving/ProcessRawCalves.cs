using System.Collections.Generic;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class ProcessRawCalves : IProcessRawCalves
    {
        private readonly BBModel _bbModel;
        private readonly ICalvingHerdAssignment _calvingHerdAssignment;
        private readonly ICalvingSexCodeAssignment _calvingSexCodeAssignment;
        public ProcessRawCalves(BBModel bbModel, ICalvingHerdAssignment calvingHerdAssignment, ICalvingSexCodeAssignment calvingSexCode)
        {
            _bbModel = bbModel;
            _calvingHerdAssignment = calvingHerdAssignment;
            _calvingSexCodeAssignment = calvingSexCode;
        }

        public List<Herd> SortCalvesIntoBeefboosterHerds(List<RawCalf> listOfRawCalves)
        {
            // Eliminate some calves - those that are not in breeding herds
            foreach (var rawCalf in listOfRawCalves)
            {
                //TODO: Config the 'non' breeder groups - for AB th
                if (rawCalf.Group == "Yard Cows")
                {
                    rawCalf.DoNotImport = true;
                    rawCalf.NotImportedReason = string.Format("ID:{0} - Management Group is not a breeding strain {1}",
                        rawCalf.HerdtraxAnimalId, rawCalf.Group);
                }
            }

            // Herd Assignment
            IEnumerable<Herd> herds = _calvingHerdAssignment.GroupByHerd(listOfRawCalves);

            // Gender => Sex_Code
            _calvingSexCodeAssignment.SetSexCode(herds);

            return herds.ToList();
        }

    }
}