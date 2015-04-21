using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HerdtraxImport.UnitTests
{
    [TestClass]
    public class ImportUnitTestBase
    {
        private const string TestDataDirectory = @".\TestData\";
        protected const string EmptyFile = TestDataDirectory + "EmptyFile.csv";


        [TestInitialize]
        public void RunFirst()
        {
            VerifyTestDataFileExists(EmptyFile);
        }


        private void VerifyTestDataFileExists(string fileName)
        {
            if (!File.Exists(fileName))
                Assert.Fail(string.Format("Unit test fails - test data file does not exist. File:{0}",fileName));
        }
    }
}