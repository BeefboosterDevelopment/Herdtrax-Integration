using System.Collections.Generic;
using HerdtraxImport.Calving;

namespace HerdtraxImport
{
    public class ImportCalving : IImportCalving
    {
        private readonly IProcessRawCalves _calfProcesssor;
        private readonly ICSVFileReader _csvFileReader;
        private readonly IWriteToDatababase _dbwriter;
        private readonly ICalvingValidateRawCalves _validator;

        private List<RawCalf> _calves;
        private List<Herd> _herds;
        private List<string> _issueList;

        public ImportCalving(ICSVFileReader csvFileReader, IProcessRawCalves calfProcesssor,
            ICalvingValidateRawCalves validator, IWriteToDatababase dbwriter)
        {
            _csvFileReader = csvFileReader;
            _calfProcesssor = calfProcesssor;
            _validator = validator;
            _dbwriter = dbwriter;
        }

        public List<string> Issues
        {
            get { return _issueList ?? (_issueList = new List<string>()); }
            set { _issueList = value; }
        }

        public List<RawCalf> Calves
        {
            get { return _calves ?? (_calves = new List<RawCalf>()); }
            set { _calves = value; }
        }

        public List<Herd> Herds
        {
            get { return _herds ?? (_herds = new List<Herd>()); }
            set { _herds = value; }
        }

        public int NumberRowsChanged { get; set; }

        public bool Import(string fileName)
        {
            Calves = _csvFileReader.DigestFile(fileName);
            Herds = _calfProcesssor.SortCalvesIntoBeefboosterHerds(Calves);
            Issues = _validator.Validate(Herds);
            if (Issues.Count == 0)
            {
                NumberRowsChanged = _dbwriter.WriteCalfData(Herds);             
            }
            return Issues.Count == 0;
        }
    }
}