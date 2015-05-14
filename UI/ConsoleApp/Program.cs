using System;
using System.Collections.Generic;
using System.IO;
using BBDM;
using HerdtraxImport;
using HerdtraxImport.Calving;
using Ninject;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<ICSVFileReader>().To<CSVFileReader>();
            kernel.Bind<ICalvingFileReader>().To<CalvingFileReader>();
            kernel.Bind<ICalvingHerdAssignment>().To<CalvingHerdAssignment>();
            kernel.Bind<IProcessRawCalves>().To<ProcessRawCalves>();
            kernel.Bind<BBModel>().ToSelf().InSingletonScope();
            kernel.Bind<ICalvingSexCodeAssignment>().To<CalvingSexCodeAssignment>();
            kernel.Bind<ICalvingTwinProcessing>().To<CalvingTwinProcessing>();
            kernel.Bind<ICalvingValidateRawCalves>().To<CalvingValidateRawCalves>();
            kernel.Bind<IImportCalving>().To<ImportCalving>();
            kernel.Bind<IWriteToDatababase>().To<WriteToDatababase>();
            kernel.Bind<ICalvingCalfBuilder>().To<CalvingCalfBuilder>();

            int idxFile = Array.IndexOf(args, "-f");
            if (idxFile == -1)
            {
                Console.WriteLine("Usage:   ConsoleApp -f filename.csv");
                Console.Write("Press enter to quit");
                Console.ReadLine();
            }

/*
            int idxAcct = Array.IndexOf(args, "-a");
            if ((idxFile == -1) || (idxAcct == -1))
            {
                Console.WriteLine("Usage:   ConsoleApp -f filename.csv     -a account Id");
            }
*/


            string filename = args[idxFile + 1];
            if (!File.Exists(filename))
                Console.WriteLine("File not found:{0}", filename);
            else
            {
                var importer = kernel.Get<IImportCalving>();

                if (importer.Import(filename))
                {
                    Console.WriteLine("\nNUMBER OF CALVES IMPORTED={0}", importer.NumberRowsChanged);
                }
                else
                {
                    ListIssues(importer.Issues);
                }
            }


            Console.Write("Press enter to quit");
            Console.ReadLine();
        }

        private static void ListIssues(List<string> issues)
        {
            if (issues.Count == 0)
                return;

            Console.WriteLine("{0} issues were discovered. No calves were imported.", issues.Count);
            foreach (string issue in issues)
            {
                Console.WriteLine(issue);
            }
        }
    }
}