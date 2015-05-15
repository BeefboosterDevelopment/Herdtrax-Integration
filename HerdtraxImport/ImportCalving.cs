using System;
using System.Collections.Generic;
using System.Data.Entity;
using BBDM;
using HerdtraxImport.Calving;

namespace HerdtraxImport
{
    public class ImportCalving : IImportCalving
    {
        private readonly IProcessRawCalves _calfProcesssor;
        private readonly ICSVFileReader _csvFileReader;
        private readonly IWriteToDatababase _dbwriter;

        private readonly BBModel _model;
        private readonly ICalvingValidateRawCalves _validator;
        private List<RawCalf> _calves;
        private List<Herd> _herds;
        private List<string> _issueList;

        public ImportCalving(ICSVFileReader csvFileReader, IProcessRawCalves calfProcesssor,
            ICalvingValidateRawCalves validator, IWriteToDatababase dbwriter, BBModel model)
        {
            _csvFileReader = csvFileReader;
            _calfProcesssor = calfProcesssor;
            _validator = validator;
            _dbwriter = dbwriter;
            _model = model;
        }

        private List<RawCalf> Calves
        {
            get { return _calves ?? (_calves = new List<RawCalf>()); }
            set { _calves = value; }
        }

        private List<Herd> Herds
        {
            get { return _herds ?? (_herds = new List<Herd>()); }
            set { _herds = value; }
        }

        public List<string> Issues
        {
            get { return _issueList ?? (_issueList = new List<string>()); }
            set { _issueList = value; }
        }

        public int NumberRowsChanged { get; set; }

        public bool Import(string fileName)
        {
            Calves = _csvFileReader.DigestFile(fileName);
            Herds = _calfProcesssor.SortCalvesIntoBeefboosterHerds(Calves);
            Issues = _validator.Validate(Herds);
            if (Issues.Count == 0)
            {
                DbContextTransaction transaction = _model.Database.BeginTransaction();
                try
                {
                    NumberRowsChanged = _dbwriter.WriteCalfData(Herds);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Transaction was rolled back.", exception);
                }
            }
            return Issues.Count == 0;
        }
    }
}