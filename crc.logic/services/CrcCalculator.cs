
using crc.common.enums;
using crc.common.interfaces;
using crc.common.models;
using static System.Linq.Enumerable;

namespace crc.logic.services
{
    public class CrcCalculator : ICrcCalculator
    {
        CrcParameters _crcParameters;

        // can be substitued during unit testing
        public ICrcCalcAssistant CalcAssitant { get; set; }

        public CrcCalculator(CrcParameters crcParameters)
        {
            _crcParameters = crcParameters;
            CalcAssitant = new CrcCalcAssistant(_crcParameters);
        }

        public uint ComputeCrc(byte[] crcPayload)
        {
            uint computedCrc = 0XDEADBEEFu;
            if (crcPayload == null) throw new Exception("Crc payload is null nothing I can do here...");

            switch (_crcParameters.CrcType)
            {
                case ChecksumType.Crc8:
                    throw new NotImplementedException();
                case ChecksumType.Crc16:
                    throw new NotImplementedException();
                case ChecksumType.Crc32:
                default:
                    if (_crcParameters.ComputationType == CrcComputationType.XorResTable)
                    {
                        computedCrc = computeUsingCrcTable(crcPayload);
                    }
                    else
                    {
                        computedCrc = computeCrcBitByBit(crcPayload);
                    }
                    break;
            }
            return computedCrc;
        }

        private uint computeCrcBitByBit(byte[] bytes)
        {
            var polynomial = CalcAssitant.getDivsor();
            uint crc = CalcAssitant.getInitialDevident();

            var crcPayload = CalcAssitant.GetCrcPayload(bytes);
            foreach (byte b in crcPayload)
            {
                crc ^= CalcAssitant.ShiftNextPaylodByteInToPlace(b); 

                foreach (var i in Range(0,8))
                {
                    if (CalcAssitant.IsMsbSet(crc))
                    {
                        crc = (uint)((crc << 1) ^ polynomial);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }

            crc = CalcAssitant.GetCrcResult(crc);
            return crc;
        }

        
        private uint computeUsingCrcTable(byte[] bytes)
        {
            throw new NotImplementedException("Xor Table approach not ready yet.");
            uint crc = 0;
            foreach (byte b in bytes)
            {
                /* XOR-in next input byte into MSB of crc and get this MSB, that's our new intermediate dividend */
                byte pos = (byte)((crc ^ (b << 24)) >> 24);
                /* Shift out the MSB used for division per lookuptable and XOR with the remainder */
                //crc = (uint)((crc << 8) ^ (uint)(_crcTable[pos]));
            }

            return crc;
        }

    }
}
