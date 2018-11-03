using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asn1Core.Test
{
    [TestClass]
    public class DerReaderTests
    {
        private Stream certificateStream => Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("Asn1Core.Test.TestData.certificate.cer");

        [TestMethod]
        public void TestDerReaderReadCertificateIsConstructed()
        {
            var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var reader = new DerReader(certificateStream);

            var encoding = reader.Read();

            Assert.AreEqual(EncodingType.Constructed, encoding.EncodingType);
        }

        [TestMethod]
        public void TestDerReaderReadCertificateHasValues()
        {
            var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var reader = new DerReader(certificateStream);

            var encoding = reader.Read();

            Assert.AreNotEqual(0, ((ConstructedDerEncoding)encoding).Values.Count());
        }
    }
}
