using System;
using System.Collections.Generic;
using System.Linq;
using BBDM;

namespace HerdtraxImport.Calving
{
    public enum IssueSeverity
    {
        Fatal,
        Warn,
        Info
    }

    public class CalvingValidateRawCalves
    {
        private readonly BBModel _model;
        private List<string> _issues;

        public CalvingValidateRawCalves(BBModel model)
        {
            _model = model;
        }

        public List<string> IssueList
        {
            get { return _issues ?? (_issues = new List<string>()); }
        }

        public List<string> Validate(IEnumerable<Herd> herds)
        {
            if (herds == null)
                throw new ArgumentNullException("herds");

            List<Herd> herdList = herds.ToList();
            if (!herdList.Any())
            {
                IssueList.Add("No herds in the list");
                return IssueList;
            }

            foreach (Herd herd in herdList)
            {
                foreach (RawCalf rawCalf in herd.Calves)
                {
                    ValidateDam(rawCalf);
                    ValidateCalf(rawCalf, herd);


                    // TODO: check for duplicates (may not be required if Herdtrax is the source)
                }
            }

            return IssueList;
        }

        private void AddIssue(RawCalf calf, string issueDescription)
        {
            AddIssue(calf, issueDescription,IssueSeverity.Fatal);
           
        }

        private void AddIssue(RawCalf calf, string issueDescription, IssueSeverity issueSeverity)
        {
            switch (issueSeverity)
            {
                case IssueSeverity.Fatal:
                    IssueList.Add(string.Format("FATAL. Id:{0} - {1}", calf.HerdtraxAnimalId, issueDescription));
                    break;  
                case IssueSeverity.Warn:
                    IssueList.Add(string.Format("WARNING. Id:{0} - {1}", calf.HerdtraxAnimalId, issueDescription));
                    break;  
                case IssueSeverity.Info:
                    IssueList.Add(string.Format("Id:{0} - {1}", calf.HerdtraxAnimalId, issueDescription));
                    break;  
            }
        }
        private void ValidateCalf(RawCalf rawCalf, Herd herd)
        {
            // Calf Registration # should be null
            if (!string.IsNullOrWhiteSpace(rawCalf.RegistrationNumber))
            {
                AddIssue(rawCalf,
                    string.Format("Calf registration number ({0}) should not exist for new calves.",
                        rawCalf.RegistrationNumber));
            }

            // Herdtrax Animal Id
            if (rawCalf.HerdtraxAnimalId == 0)
            {
                AddIssue(rawCalf,
                    string.Format("Herdtrax Animal Id is required for every calf. Its missing for calf with VID {0}",
                        rawCalf.VID));
            }
            else
            {
                tblCalf dbCalf = _model.tblCalves.FirstOrDefault(c => c.HerdtraxAnimalId == rawCalf.HerdtraxAnimalId);
                if (dbCalf != null)
                    AddIssue(rawCalf,
                        string.Format(
                            "Animal Id exists in tblCalf. This import progam is not prepared to UPDATE... only INSERTS.  Calf_SN:{0}", dbCalf.Calf_SN));
            }

            // Sex
            if (string.IsNullOrWhiteSpace(rawCalf.Gender))
            {
                AddIssue(rawCalf, string.Format("No gender (Calf-M/Calf-F)."));
            }

            if ((rawCalf.SexCode != "F") && (rawCalf.SexCode != "M"))
            {
                AddIssue(rawCalf,
                    string.Format("Missing or Invalid Sex Code - {0},  Herdtrax Gender:{1}", rawCalf.SexCode, rawCalf.Gender));
            }

            // Date
            if (rawCalf.BirthDate == DateTime.MinValue)
            {
                AddIssue(rawCalf, string.Format("Birth date is missing."));
            }

            // Weight
            if ((rawCalf.BirthWt > 0) && ((rawCalf.BirthWt < 20) || (rawCalf.BirthWt > 150)))
            {
                AddIssue(rawCalf, string.Format("Birth weight is out of range ({0})", rawCalf.BirthWt));
            }


            // Tag


            if (string.IsNullOrWhiteSpace(rawCalf.TagNumber))
            {
                AddIssue(rawCalf, string.Format("Calf tag number is null or missing"));
            }
            else
            {
                int tagnum;
                if (!int.TryParse(rawCalf.TagNumber, out tagnum))
                {
                }
                else
                {
                    tblCalf dbCalf = _model.tblCalves.FirstOrDefault(
                        c =>
                            c.CalfHerd_SN == herd.HerdSN && c.CalfBirthYr_Num == rawCalf.BirthDate.Year &&
                            c.CalfTag_Num == tagnum);
                    if (dbCalf != null)
                    {
                        AddIssue(rawCalf, string.Format("Calf exists: HerdSN:{0}  BirthYrNum:{1}  TagNumber{2}.  Calf_SN={3}", herd.HerdSN, rawCalf.BirthDate.Year, rawCalf.TagNumber, dbCalf.Calf_SN));
                    }
                }
            }
        }

        private void ValidateDam(RawCalf rawCalf)
        {
            if (rawCalf.DamSN <= 0)
            {
                AddIssue(rawCalf, string.Format("Dam SN is missing or invalid ({0})", rawCalf.DamSN));
            }
            else if (_model.tblDams.Find(rawCalf.DamSN) == null)
                AddIssue(rawCalf, string.Format("Dam SN {0} not found.", rawCalf.DamSN));
        }
    }
}