using System.IO;
using HerdtraxImport.Calving;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HerdtraxImport.UnitTests
{
    [TestClass]
    public class ImportUnitTestBase
    {
        private const string TestDataDirectory = @".\TestData\";
        protected const string EmptyFile = TestDataDirectory + "EmptyFile.csv";

        protected Mock<ICalvingFileReader> _mockCalvingFileReader;

        [TestInitialize]
        public void RunFirst()
        {
            VerifyTestDataFileExists(EmptyFile);

            _mockCalvingFileReader = new Mock<ICalvingFileReader>();
        }


        private void VerifyTestDataFileExists(string fileName)
        {
            if (!File.Exists(fileName))
                Assert.Fail(string.Format("Unit test fails - test data file does not exist. File:{0}",fileName));
        }
    }
}