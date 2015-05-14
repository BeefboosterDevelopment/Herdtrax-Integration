using System.Collections.Generic;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public class ProcessRawCalves : IProcessRawCalves
    {
        private readonly BBModel _bbModel;
        private readonly ICalvingHerdAssignment _herdAssignment;
        private readonly ICalvingSexCodeAssignment _sexCodeAssignment;
        private readonly ICalvingTwinProcessing _twinProcessing;
        public ProcessRawCalves(BBModel bbModel, ICalvingHerdAssignment herdAssignment, ICalvingSexCodeAssignment sexCode, ICalvingTwinProcessing twinProcessing)
        {
            _bbModel = bbModel;
            _herdAssignment = herdAssignment;
            _sexCodeAssignment = sexCode;
            _twinProcessing = twinProcessing;
        }

        public List<Herd> SortCalvesIntoBeefboosterHerds(List<RawCalf> listOfRawCalves)
        {
            // Eliminate calves that are not in breeding herds
            foreach (var rawCalf in listOfRawCalves)
            {
                //TODO: Config the 'non' breeder groups - for AB
                if (rawCalf.Group == "Yard Cows")
                {
                    rawCalf.DoNotImport = true;
                    rawCalf.NotImportedReason = string.Format("ID:{0} - Management Group is not a breeding strain {1}",
                        rawCalf.HerdtraxAnimalId, rawCalf.Group);
                }
            }

            // Herd Assignment
            IEnumerable<Herd> herds = _herdAssignment.GroupByHerd(listOfRawCalves);

            // Gender => Sex_Code
            _sexCodeAssignment.SetSexCode(herds);

            // Graft, Sibling
            _twinProcessing.Process(herds);

            return herds.ToList();
        }

    }
}