using System;
using System.IO;
using System.Linq;

namespace Asn1Core
{
    public partial class Identifier
    {
        public IdentifierClass IdentifierClass { get; set; }

        public int Tag { get; set; }

        public EncodingType EncodingType { get; set; }

        public static Identifier Read(Stream stream)
        {
            byte firstOctet = (byte) stream.ReadByte();
            if (WellKnownIdentifiers.ContainsKey(firstOctet))
            {
                return WellKnownIdentifiers[firstOctet];
            }

            IdentifierClass identifierClass = (IdentifierClass)(firstOctet & 0b11000000);
            EncodingType encodingType = (EncodingType)(firstOctet & 0b00100000);
            int tag = firstOctet & 0b00011111;

            if (tag != 0b00011111)
            {
                var identifier = new Identifier
                {
                    IdentifierClass = identifierClass,
                    EncodingType = encodingType,
                    Tag = tag
                };

                return identifier;
            }

            throw new NotImplementedException();
        }

        public static void WriteIdentifier(Stream stream, IdentifierClass identifierClass, EncodingType encodingType,
            int tag)
        {
            var identifier = new Identifier
            {
                IdentifierClass = IdentifierClass.Universal,
                EncodingType = encodingType,
                Tag = tag
            };

            identifier.Write(stream);
        }

        public static void WriteUniversalIdentifier(Stream stream, EncodingType encodingType, int tag)
        {
            WriteIdentifier(stream, IdentifierClass.Universal, encodingType, tag);
        }

        public void Write(Stream stream)
        {
            if (Tag <= 30)
            {
                stream.WriteByte((byte)((int)IdentifierClass | (int)EncodingType | Tag));
                return;
            }

            throw new NotImplementedException();
        }

        public byte[] GetBytes()
        {
            var stream = new MemoryStream();
            this.Write(stream);
            return stream.ToArray();
        }
    }
}