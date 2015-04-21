using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HerdtraxImport.UnitTests.Read
{



    [TestClass]
    public class WhenDigesting
    {
        [TestMethod]
        public void File()
        {
            var systemUnderTest = new CSVFileReader();
            systemUnderTest.DigestFile("");
        }

        [TestMethod]
        public void ReadsFile()
        {
            var systemUnderTest = new CSVFileReader();
            systemUnderTest.DigestFile("");            
        }

    }
}
