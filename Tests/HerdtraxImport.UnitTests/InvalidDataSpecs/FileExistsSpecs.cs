using HerdtraxImport.Calving;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HerdtraxImport.UnitTests.Read
{
    [TestClass]
    public class WhenDigesting
    {
        private Mock<ICalvingFileReader> _mockCalvingFileReader;


        [TestInitialize]
        public void EachTest()
        {
            _mockCalvingFileReader = new Mock<ICalvingFileReader>();
        }

        [TestMethod]
        public void File()
        {
            var systemUnderTest = new CSVFileReader(_mockCalvingFileReader.Object);
            systemUnderTest.DigestFile("");
        }

        [TestMethod]
        public void ReadsFile()
        {
            var systemUnderTest = new CSVFileReader(_mockCalvingFileReader.Object);
            systemUnderTest.DigestFile("");
        }
    }
}