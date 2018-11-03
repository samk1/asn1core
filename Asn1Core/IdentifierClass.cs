namespace Asn1Core
{
    public enum IdentifierClass
    {
        Universal = 0b00000000,
        Application = 0b01000000,
        ContextSpecific = 0b10000000,
        Private = 0b11000000
    }
}