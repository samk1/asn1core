using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Asn1Core
{
    public class DerReader
    {
        private readonly Stream stream;

        public DerReader(Stream stream)
        {
            this.stream = stream;
        }

        public IEncoding Read()
        {
            var identifier = Identifier.Read(stream);
            uint length = Length.Read(stream).OctetCount;

            switch (identifier.EncodingType)
            {
                case EncodingType.Primitive:
                {
                    return (IEncoding) PrimitiveDerEncoding.Read(stream, identifier, length);
                }
                case EncodingType.Constructed:
                {
                    return (IEncoding) ConstructedDerEncoding.Read(stream, identifier, length, this);
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
