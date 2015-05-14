using System;
using System.Collections.Generic;

namespace HerdtraxImport.Calving
{
    public interface ICalvingFileReader
    {
        RawCalf MakeRawCalf(CalvingDataColumnDefinition[] coldefs, string[] colvals);
        List<CalvingDataColumnDefinition> ProcessHeadings(string headings);
    }


    public class CalvingFileReader : ICalvingFileReader
    {
        public RawCalf MakeRawCalf(CalvingDataColumnDefinition[] coldefs, string[] colvals)
        {
            if (coldefs == null) throw new ArgumentNullException("coldefs");
            if (colvals == null) throw new ArgumentNullException("colvals");
            if (coldefs.Length > colvals.Length)
                throw new ArgumentException(string.Format(
                    "More required columns than there are data values. There are {0} required column definitions, and only {1} data values present in.",
                    coldefs.Length, colvals.Length));

            var calf = ExtractValuesUsingColumnDefinitionIndexes(coldefs, colvals);

            return calf;
        }

        public List<CalvingDataColumnDefinition> ProcessHeadings(string headings)
        {
            // when time permits allow variations on the order and the names of the columns

            // for now though...
            return new List<CalvingDataColumnDefinition>
            {
                // hardcode a subset ... theres more.....
                new CalvingDataColumnDefinition(CalvingColumn.CalfHerdtraxId, 0),
                new CalvingDataColumnDefinition(CalvingColumn.CalfVID, 1),
                new CalvingDataColumnDefinition(CalvingColumn.CalfRegId, 2),
                new CalvingDataColumnDefinition(CalvingColumn.BirthWeight, 3),
                new CalvingDataColumnDefinition(CalvingColumn.CalfCCIA, 4),
                new CalvingDataColumnDefinition(CalvingColumn.BirthDate, 5),
                new CalvingDataColumnDefinition(CalvingColumn.DamCCIA, 6),
                new CalvingDataColumnDefinition(CalvingColumn.DamVID, 7),
                new CalvingDataColumnDefinition(CalvingColumn.Gender, 8),
                new CalvingDataColumnDefinition(CalvingColumn.TagNumber, 10),
                new CalvingDataColumnDefinition(CalvingColumn.TagColor, 11),
                new CalvingDataColumnDefinition(CalvingColumn.Twins, 12),
                new CalvingDataColumnDefinition(CalvingColumn.Hoof, 13),
                new CalvingDataColumnDefinition(CalvingColumn.Udder, 14),
                new CalvingDataColumnDefinition(CalvingColumn.ManagementGroup, 15),
                new CalvingDataColumnDefinition(CalvingColumn.DamTagNumber, 17),
                new CalvingDataColumnDefinition(CalvingColumn.DamTagLetter, 16),
                new CalvingDataColumnDefinition(CalvingColumn.DamTagColor, 24),
                new CalvingDataColumnDefinition(CalvingColumn.DamRegId, 18),
                new CalvingDataColumnDefinition(CalvingColumn.CalfDNA, 19),
                new CalvingDataColumnDefinition(CalvingColumn.Ease, 20),
                new CalvingDataColumnDefinition(CalvingColumn.SurrogateTagNumber, 21),
                new CalvingDataColumnDefinition(CalvingColumn.SurrogateTagLetter, 22),
                new CalvingDataColumnDefinition(CalvingColumn.SurrogateTagColor, 23),
                new CalvingDataColumnDefinition(CalvingColumn.TagLetter, 25),

            };
        }

        private RawCalf ExtractValuesUsingColumnDefinitionIndexes(IEnumerable<CalvingDataColumnDefinition> coldefs,
            IReadOnlyList<string> colvals)
        {
            var calf = new RawCalf();
            foreach (var coldef in coldefs)
            {
                switch (coldef.Column)
                {
                    case CalvingColumn.ManagementGroup:
                        calf.Group = colvals[coldef.Index];
                        break;
                    case CalvingColumn.HerdLocation:
                        break;
                    case CalvingColumn.DamRegId:
                        calf.DamRegistrationNumber = colvals[coldef.Index];
                        calf.DamSN = ConvertInt(colvals[coldef.Index]);
                        break;
                    case CalvingColumn.DamHerdtraxId:
                        break;
                    case CalvingColumn.DamCCIA:
                        break;
                    case CalvingColumn.DamVID:
                        calf.DamVID = colvals[coldef.Index];
                        break;
                    case CalvingColumn.DamTagNumber:
                        calf.DamTagNumber = colvals[coldef.Index];
                        break;
                    case CalvingColumn.DamTagLetter:
                        calf.DamTagLetter = colvals[coldef.Index];
                        break;
                    case CalvingColumn.DamTagColor:
                        calf.DamTagColor = colvals[coldef.Index];
                        break;
                    case CalvingColumn.CalfRegId:
                        calf.RegistrationNumber = colvals[coldef.Index];
                        break;
                    case CalvingColumn.CalfHerdtraxId:
                        calf.HerdtraxAnimalId = ConvertInt(colvals[coldef.Index]);
                        break;
                    case CalvingColumn.CalfVID:
                        calf.VID = colvals[coldef.Index];
                        break;
                    case CalvingColumn.CalfCCIA:
                        calf.CCIA = colvals[coldef.Index];
                        break;
                    case CalvingColumn.Gender:
                        calf.Gender = colvals[coldef.Index];
                        break;
                    case CalvingColumn.Hoof:
                        calf.HoofScore = ConvertInt(colvals[coldef.Index]);
                        break;
                    case CalvingColumn.Udder:
                        calf.UdderScore = ConvertInt(colvals[coldef.Index]);
                        break;
                    case CalvingColumn.Ease:
                        calf.EaseScore = ConvertInt(colvals[coldef.Index]);
                        break;
                    case CalvingColumn.BirthWeight:
                        calf.BirthWt = ConvertInt(colvals[coldef.Index]);
                        break;
                    case CalvingColumn.BirthDate:
                        calf.BirthDate = ConvertDate(colvals[coldef.Index]);
                        break;
                    case CalvingColumn.TagNumber:
                        calf.TagNumber = colvals[coldef.Index];
                        break;
                    case CalvingColumn.TagLetter:
                        calf.TagLetter = colvals[coldef.Index];
                        break;
                    case CalvingColumn.TagColor:
                        calf.TagColor = colvals[coldef.Index];
                        break;
                    case CalvingColumn.Twins:
                        calf.TwinType = colvals[coldef.Index];
                        break;
                    case CalvingColumn.CalfDNA:
                        calf.DNATag = colvals[coldef.Index];
                        break;
                    case CalvingColumn.SurrogateTagNumber:
                        calf.SurrogateTagNumber = colvals[coldef.Index];
                        break;
                    case CalvingColumn.SurrogateTagLetter:
                        calf.SurrogateTagLetter = colvals[coldef.Index];
                        break;
                    case CalvingColumn.SurrogateTagColor:
                        calf.SurrogateTagColor = colvals[coldef.Index];
                        break;
                }
            }
            return calf;
        }

        private int ConvertInt(string val)
        {
            int i;
            return int.TryParse(val, out i) ? i : 0;
        }

        private DateTime ConvertDate(string val)
        {
            DateTime d;
            return DateTime.TryParse(val, out d) ? d : DateTime.MinValue;
        }


    }
}