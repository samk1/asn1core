using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Asn1Core
{
    public class ConstructedDerEncoding : DerEncoding, IEncoding
    {
        public override void WriteIdentifier(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override void WriteContents(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override int GetLength()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEncoding> Values { get; set; }

        public static DerEncoding Read(Stream stream, Identifier identifier, uint length, DerReader derReader)
        {
            long position = stream.Position;
            List<IEncoding> values = new List<IEncoding>();

            while (stream.Position <= position + length - 1)
            {
               values.Add(derReader.Read());
            }

            return new ConstructedDerEncoding
            {
                IdentifierClass = identifier.IdentifierClass,
                EncodingType = EncodingType.Constructed,
                Tag = identifier.Tag,
                Length = (int) length,
                Values = values,
            };
        }

        public IdentifierClass IdentifierClass { get; set; }
        public EncodingType EncodingType { get; set; }
        public int Tag { get; set; }
        public int Length { get; set; }
    }
}
