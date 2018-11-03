using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Asn1Core
{
    public partial class Identifier
    {
        public static readonly Identifier SEQUENCE = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Constructed,
            Tag = 16
        };

        public static readonly Identifier INTEGER = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Primitive,
            Tag = 2
        };

        public static readonly Identifier OBJECT_ID = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Primitive,
            Tag = 6
        };

        public static readonly Identifier NULL = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Primitive,
            Tag = 5
        };

        public static readonly Identifier SET = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Constructed,
            Tag = 17
        };

        public static readonly Identifier PRINTABLE_STRING = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Primitive,
            Tag = 19
        };

        public static readonly Identifier UTC_TIME = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Primitive,
            Tag = 23
        };

        public static readonly Identifier BIT_STRING = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Primitive,
            Tag = 3
        };

        public static readonly Identifier BOOL = new Identifier
        {
            IdentifierClass = IdentifierClass.Universal,
            EncodingType = EncodingType.Primitive,
            Tag = 1
        };

        public static readonly IReadOnlyDictionary<byte, Identifier> WellKnownIdentifiers = GetWellKnownIdentifiersList();

        private static IReadOnlyDictionary<byte, Identifier> GetWellKnownIdentifiersList()
        {
            return typeof(Identifier)
                .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(prop => prop.FieldType == typeof(Identifier))
                .Select(prop => prop.GetValue(null))
                .Cast<Identifier>()
                .ToDictionary(ident => ident.GetBytes().Single(), ident => ident);
        }
    }
}
