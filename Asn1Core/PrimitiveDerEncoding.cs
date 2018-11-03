using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Asn1Core
{
    class PrimitiveDerEncoding : DerEncoding, IEncoding
    {
        public IdentifierClass IdentifierClass { get; set; }
        public EncodingType EncodingType { get; set; }
        public int Tag { get; set; }
        public int Length { get; set; }
        public byte[] Contents { get; set; }

        public override void WriteIdentifier(Stream stream)
        {
            Identifier.WriteIdentifier(stream, IdentifierClass, EncodingType.Primitive, Tag);
        }

        public override void WriteContents(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override int GetLength()
        {
            throw new NotImplementedException();
        }

        public static DerEncoding Read(Stream stream, Identifier identifier, uint length)
        {
            byte[] contents = new byte[length];
            stream.Read(contents, 0, (int) length);

            return new PrimitiveDerEncoding
            {
                IdentifierClass = identifier.IdentifierClass,
                EncodingType = EncodingType.Primitive,
                Tag = identifier.Tag,
                Length = contents.Length,
                Contents = contents
            };
        }
    }
}
