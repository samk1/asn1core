using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asn1Core.Test
{
    [TestClass]
    public class IdentifierTests
    {
        [TestMethod]
        public void TestEncodeSequenceIdentifier()
        {
            var expected = new byte[] { 0x30 };
            var buffer = new MemoryStream(1);
            Identifier.SEQUENCE.Write(buffer);

            CollectionAssert.AreEqual(expected, buffer.ToArray());
        }

        [TestMethod]
        public void TestEncodeIntegerIdentifier()
        {
            var expected = new byte[] {0x02};
            var buffer = new MemoryStream(1);
            Identifier.INTEGER.Write(buffer);

            CollectionAssert.AreEqual(expected, buffer.ToArray());
        }

        [TestMethod]
        public void TestEncodeObjectIdIdentifier()
        {
            var expected = new byte[] { 0x06 };
            var buffer = new MemoryStream(1);
            Identifier.OBJECT_ID.Write(buffer);

            CollectionAssert.AreEqual(expected, buffer.ToArray());
        }

        [TestMethod]
        public void TestEncodeNullIdentifier()
        {
            var expected = new byte[] { 0x05 };
            var buffer = new MemoryStream(1);
            Identifier.NULL.Write(buffer);

            CollectionAssert.AreEqual(expected, buffer.ToArray());
        }

        [TestMethod]
        public void TestDecodeSequenceIdentifier()
        {
            var buffer = new MemoryStream(new byte[] { 0x30 });

            Assert.AreSame(Identifier.SEQUENCE, Identifier.Read(buffer));
        }
    }
}
