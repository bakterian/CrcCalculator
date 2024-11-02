using crc.common.models;

namespace crc.common.interfaces
{
    public interface ICrcCalcAssistant
    {
        byte Reflect8(byte input);

        uint Reflect32(uint input);

        uint getInitialDevident();

        uint getDivsor();

        byte[] GetCrcPayload(byte[] rawPayload);

        uint ShiftNextPaylodByteInToPlace(byte val);

        bool IsMsbSet(uint crc);

        public uint GetCrcResult(uint reminder);
    }
}

