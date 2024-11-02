using crc.common.enums;

namespace crc.common.models
{
    public class CrcParameters
    {
        public ChecksumType CrcType { get; set; }
        public PolynomialType PolyType { get; set; }
        public CrcComputationType ComputationType { get; set; }
        public uint InitialValue { get; set; }
        public bool InputReflected { get; set; }
        public bool ResultReflected { get; set; }
        public uint FinalXorValue { get; set; }
    }
}
