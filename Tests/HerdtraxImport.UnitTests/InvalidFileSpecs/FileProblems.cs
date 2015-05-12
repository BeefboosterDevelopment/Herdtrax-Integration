using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HerdtraxImport.UnitTests.InvalidFileSpecs
{

    [TestClass]
    public class FileNameIsMissing : ImportUnitTestBase
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void MustNotBeNull()
        {


            var systemUnderTest = new CSVFileReader(_mockCalvingFileReader.Object);
            systemUnderTest.DigestFile(null);
            Assert.IsTrue(false, "Should not have made here!");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void MustNotBeZeroLength()
        {
            var systemUnderTest = new CSVFileReader(_mockCalvingFileReader.Object);
            systemUnderTest.DigestFile("");
            Assert.IsTrue(false, "Should not have made here!");
        }
    }

    [TestClass]
    public class FileDoesNotExist:ImportUnitTestBase
    {
        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Fails()
        {
            var systemUnderTest = new CSVFileReader(_mockCalvingFileReader.Object);
            systemUnderTest.DigestFile("this file does not exist");
            Assert.IsTrue(false,"Should not have made here!");
        }
    }

    [TestClass]
    public class FileIsEmpty : ImportUnitTestBase
    {
        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Fails()
        {
            var systemUnderTest = new CSVFileReader(_mockCalvingFileReader.Object);
            systemUnderTest.DigestFile(EmptyFile);
            Assert.IsTrue(false, "Should not have made here!");
        }

    }
}
