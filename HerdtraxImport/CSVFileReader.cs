using System;
using System.IO;

namespace HerdtraxImport
{
    public class CSVFileReader
    {
        public int DigestFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            int rowCount = 0;
            try
            {
                if (!File.Exists(fileName))
                    throw new ApplicationException(string.Format("File does not exist. {0}", fileName));


                var sr = new StreamReader(fileName);

                // First row must be the headings
                string heading = sr.ReadLine();
                if (heading == null)
                    throw new ApplicationException(string.Format("Empty file: {0}", fileName));

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
            catch (Exception)
            {
                throw;
            }
            return rowCount;
        }
    }
}