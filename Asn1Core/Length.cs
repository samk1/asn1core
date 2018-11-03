using System;
using System.Buffers.Binary;
using System.IO;
using System.Linq;

namespace Asn1Core
{
    public class Length
    {
        public uint OctetCount { get; set; }

        public static Length Read(Stream stream)
        {
            byte firstOctet = (byte) stream.ReadByte();

            if (firstOctet == 0b10000000)
            {
                throw new NotImplementedException("Cannot read indefinite length");
            }

            if (firstOctet <= 127)
            {
                return new Length
                {
                    OctetCount = firstOctet
                };
            }

            int lengthByteCount = 0b10000000 ^ firstOctet;

            if (lengthByteCount > sizeof(uint))
            {
                throw new NotImplementedException();
            }

            uint octetCount = 0;

            for (int i = lengthByteCount; i > 0; i--)
            {
                uint nextByte = (uint) stream.ReadByte();
                octetCount += nextByte << ((i - 1) * 8);
            }

            return new Length()
            {
                OctetCount = octetCount
            };
        }

        public static void WriteLength(Stream stream, int octetCount)
        {
            var length = new Length
            {
                OctetCount = (uint) octetCount
            };

            length.Write(stream);
        }

        public void Write(Stream stream)
        {
            if (OctetCount <= 127)
            {
                stream.WriteByte((byte)OctetCount);
                return;
            }

            int lengthOctetCount = 0;
            uint shiftedOctetCount = OctetCount;
            byte[] lengthBuffer = new byte[4];
            while (shiftedOctetCount != 0)
            {
                lengthBuffer[lengthOctetCount] = (byte)(shiftedOctetCount & 0xff);
                shiftedOctetCount >>= 8;
                lengthOctetCount++;
            }

            var lengthSpan = new Span<byte>(lengthBuffer, 0, lengthOctetCount);
            lengthSpan.Reverse();

            stream.WriteByte((byte) (0x80 | lengthOctetCount));
            stream.Write(lengthSpan.ToArray(), 0, lengthOctetCount);
        }
    }
}