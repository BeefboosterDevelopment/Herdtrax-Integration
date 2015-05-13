using System;
using System.Collections.Generic;
using System.IO;

namespace HerdtraxImport.Calving
{
    public class CSVFileReader : ICSVFileReader
    {
        private readonly ICalvingFileReader _calvingFileReader;

        public CSVFileReader(ICalvingFileReader calvingFileReader)
        {
            _calvingFileReader = calvingFileReader;
        }

        public List<RawCalf> DigestFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ApplicationException(string.Format("CSV file does not exist. {0}", fileName));

            var allCalves = new List<RawCalf>();
            int idx = 0;
            string[] allLines = File.ReadAllLines(fileName);

            CalvingDataColumnDefinition[] coldefs = _calvingFileReader.ProcessHeadings(allLines[0]).ToArray();


            for (idx = 1; idx < allLines.Length; idx++)
                allCalves.Add(_calvingFileReader.MakeRawCalf(coldefs, allLines[idx].Split(',')));

            return allCalves;
        }
    }
}