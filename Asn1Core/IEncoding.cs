using System;
using System.Collections.Generic;
using System.Text;

namespace Asn1Core
{
    public interface IEncoding
    {
        IdentifierClass IdentifierClass { get; set; }
        EncodingType EncodingType { get; set; }
        int Tag { get; set; }
        int Length { get; set; }
    }
}
