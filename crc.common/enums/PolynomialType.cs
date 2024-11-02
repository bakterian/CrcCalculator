namespace crc.common.enums
{
    public enum PolynomialType : uint
    {
        Crc32Normal = 0x04C11DB7u,
        Crc32Reversed = 0xEDB88320u,
        Crc32Reciprocal = 0xDB710641u,
        Crc32ReversedReciprocal = 0x82608EDBu,
        Crc32CNormal = 0x1EDC6F41u,
        Crc32CReversed = 0x82F63B78u,
        Crc32CReciprocal = 0x05EC76F1u,
        Crc32CReversedReciprocal = 0x8F6E37A0u,
        Crc32KNormal = 0x741B8CD7u,
        Crc32KReversed = 0xEB31D82Eu,
        Crc32KReciprocal = 0xD663B05Du,
        Crc32KReversedReciprocal = 0xBA0DC66Bu,
        Crc32K2Normal = 0x32583499u,
        Crc32K2Reversed = 0x992C1A4Cu,
        Crc32K2Reciprocal = 0x32583499u,
        Crc32K2ReversedReciprocal = 0x992C1A4Cu,
        Crc32QNormal = 0x814141ABu,
        Crc32QReversed = 0xD5828281u,
        Crc32QReciprocal = 0xAB050503u,
        Crc32QReversedReciprocal = 0xC0A0A0D5u
    };
}