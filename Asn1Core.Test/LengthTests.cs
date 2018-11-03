using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asn1Core.Test
{
    [TestClass]
    public class LengthTests
    {
        [TestMethod]
        public void TestWrite38ShortForm()
        {
            var expected = new byte[] { 0b00100110 };
            var length = new Length
            {
                OctetCount = 38
            };

            var buffer = new MemoryStream(1);
            length.Write(buffer);

            CollectionAssert.AreEqual(expected, buffer.ToArray());
        }

        [TestMethod]
        public void TestWrite201LongForm()
        {
            var expected = new byte[] {0b10000001, 0b11001001};
            var length = new Length
            {
                OctetCount = 201
            };

            var buffer = new MemoryStream(2);
            length.Write(buffer);

            CollectionAssert.AreEqual(expected, buffer.ToArray());
        }

        [TestMethod]
        public void TestWrite266LongForm()
        {
            var expected = new byte[] {0x82, 0x01, 0x0a};
            var length = new Length
            {
                OctetCount = 266
            };

            var buffer = new MemoryStream(3);
            length.Write(buffer);

            CollectionAssert.AreEqual(expected, buffer.ToArray());
        }

        [TestMethod]
        public void TestRead38ShortForm()
        {
            var stream = new MemoryStream(new byte[] { 0b00100110 });

            Assert.AreEqual((uint)38, Length.Read(stream).OctetCount);
        }

        [TestMethod]
        public void TestRead201LongForm()
        {
            var stream = new MemoryStream(new byte[] { 0b10000001, 0b11001001 });

            Assert.AreEqual((uint)201, Length.Read(stream).OctetCount);
        }

        [TestMethod]
        public void TestRead266LongForm()
        {
            var stream = new MemoryStream(new byte[] { 0x82, 0x01, 0x0a });

            Assert.AreEqual((uint)266, Length.Read(stream).OctetCount);
        }

        [TestMethod]
        public void TestReadingIndefiniteLengthFails()
        {
            var indefiniteLengthStream = new MemoryStream(new byte[] {0b10000000});

            try
            {
                Length.Read(indefiniteLengthStream);
            }
            catch (NotImplementedException)
            {
                return;
            }

            Assert.Fail();
        }
    }
}
