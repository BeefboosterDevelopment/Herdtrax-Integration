using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HerdtraxImport.Calving;

namespace HerdtraxImport
{
    public class CSVFileReader
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
            
            var allCalves = new List<RawCalf>();

            int idx = 0;
            if (!File.Exists(fileName))
                throw new ApplicationException(string.Format("CSV file does not exist. {0}", fileName));

            var allLines = File.ReadAllLines(fileName);

            var coldefs = _calvingFileReader.ProcessHeadings(allLines[0]).ToArray();

              
            for (idx = 1; idx < allLines.Length; idx++)
                allCalves.Add(_calvingFileReader.MakeRawCalf(coldefs, allLines[idx].Split(',')));

            return allCalves;

/*

                var sr = new StreamReader(fileName);

                // First row must be the headings
                string headings = sr.ReadLine();
                if (headings == null)
                    throw new ApplicationException(string.Format("Empty file: {0}", fileName));

                var coldefs = _calvingFileReader.ProcessHeadings(headings).ToArray();

                var allCalves = new List<RawCalf>();

                var rowCount = 0;
                String line = sr.ReadLine();
                while (line != null)
                {
                    rowCount++;
                    Console.WriteLine(line);

                    string[] vals = line.Split(',');
                    if (vals.Length != coldefs.Length )
                        throw new Exception(string.Format("Invalid file format. Looking for {0} data columns, but found {1}", coldefs.Length, vals.Length));

                    allCalves.Add(_calvingFileReader.MakeRawCalf(coldefs, vals));
                   

                    line = sr.ReadLine();
                }
*/



            /*
                var sr = new StreamReader(fileName);

                // First row must be the headings
                string headings = sr.ReadLine();
                if (headings == null)
                    throw new ApplicationException(string.Format("Empty file: {0}", fileName));*/

/*                heading = heading.Trim().Replace(" ", "");
                if (!heading.Equals("DisplayID,DeviceID", StringComparison.CurrentCultureIgnoreCase))
                    throw new Exception("Invalid iButton file format, must be DisplayID,DeviceID");
                // first line of data
                String line = sr.ReadLine();
                while (line != null)
                {
                    rowCount++;             
                    Console.WriteLine(line);
                    string[] vals = line.Split(',');
                    if (vals.Length != 2)
                        throw new Exception("Invalid iButton file format, 2 columns only");
                    InVals.Add(vals[0], vals[1]);
                    line = sr.ReadLine();
                }*/


        }
    }
}