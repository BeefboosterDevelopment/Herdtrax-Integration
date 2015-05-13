using System;
using System.Collections.Generic;
using BBDM;
using HerdtraxImport;
using HerdtraxImport.Calving;

namespace TestHarness
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //new DatabaseTests().Test();
            //new CalvingFileReaderTests().Test();

            var cfr = new CalvingFileReader();
            var sut = new CSVFileReader(cfr);


            Console.WriteLine("DigestFile processing file: {0}", TestingConstants.TestCSVFileName);
            var listOfRawCalves = sut.DigestFile(TestingConstants.TestCSVFileName);

            Console.WriteLine("DigestFile returned {0} raw calves.", listOfRawCalves.Count);

            foreach (var calf in listOfRawCalves)
            {
                Console.WriteLine("Tag:{0}", calf.TagNumber);
            }


/*            foreach (var rawCalf in listOfRawCalves)
            {
                rawCalf.HerdSN = LookupHerdSN(rawCalf.Group);
            }
 */


            var bbModel = new BBModel();

 /*           // Eliminate some calves
            var calvingDoNotImport = new CalvingDoNotImport();
            calvingDoNotImport.DoNotImport(listOfRawCalves);*/


            // Herd Assignment
            var calvingHerdAssignment = new CalvingHerdAssignment(bbModel);
            IEnumerable<Herd> herds = calvingHerdAssignment.GroupByHerd(listOfRawCalves);


            // Gender => Sex_Code
            var calvingSexCodeAssignment = new CalvingSexCodeAssignment();
            calvingSexCodeAssignment.SetSexCode(herds);


            // Validate
            var validator = new CalvingValidateRawCalves(bbModel);
            var issues = validator.Validate(herds);
            if (issues.Count > 0)
            {
                Console.WriteLine("{0} FATAL ISSUES FOUND ---- CAN NOT IMPORT.", issues.Count);
                foreach (var issue in issues)
                {
                    Console.WriteLine(issue);
                }
                Console.WriteLine();
            }
            else
            {

                // Write the calves
               
                var writer = new WriteToDatababase(bbModel);
                var totalImportCount = writer.WriteCalfData(herds);

                Console.WriteLine("\nNUMBER OF CALVES IMPORTED:{0}",totalImportCount);
    
            }



            Console.Write("Hit Enter to quit the test harness");
            Console.Read();
        }

        private static int LookupHerdSN(string group)
        {
            //TODO: config or new table


            if (group == "Yard Cows")
                return 0;

            if (group == "LS/Heifers")
                return 505;

            return 5;
        }
    }
}