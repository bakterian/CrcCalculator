using crc.common.models;
using crc.common.enums;

namespace crc.logic.services
{
    public static class CrcParametersFactory
    {
        public static CrcParameters createTypicalParams()
        {
            var parameters = new CrcParameters()
            {
                CrcType = ChecksumType.Crc32,
                PolyType = PolynomialType.Crc32Normal,
                ComputationType = CrcComputationType.BitByBit,
                InitialValue = 0x00u,
                InputReflected = true,
                ResultReflected = true,
                FinalXorValue = 0xFFFFFFFFu
            };

            return parameters;
        }
    }
}
