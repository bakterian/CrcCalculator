using crc.common.models;

namespace crc.common.interfaces
{
    public interface ICrcCalculator
    {
        uint ComputeCrc(byte[] crcPayload);
    }
}
