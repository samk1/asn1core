using System;
using System.IO;

namespace Asn1Core
{
    using System.Data.Common;
    using System.Xml.Schema;

    public abstract class DerEncoding
    {
        public abstract void WriteIdentifier(Stream stream);

        public abstract void WriteContents(Stream stream);

        public abstract int GetLength();

        public void WriteBytes(Stream stream)
        {
            WriteIdentifier(stream);
            Length.WriteLength(stream, GetLength());
            WriteContents(stream);
        }
    }
}
