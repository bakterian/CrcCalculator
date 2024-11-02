
using crc.common.interfaces;
using crc.common.models;
using crc.common.enums;

namespace crc.logic.services
{
    public class CrcCalcAssistant : ICrcCalcAssistant
    {
        CrcParameters _crcParameters;
        int _polynomialBitwidth;
        uint _crcMsb;
        uint[] _crcTable;

        public CrcCalcAssistant(CrcParameters crcParameters)
        {
            if (crcParameters == null) throw new ArgumentNullException("CrcParameters input arg is null");
            _crcParameters = crcParameters;
            _polynomialBitwidth = determinePolynomialBitwidth();
            _crcMsb = getCrcMsb();
            _crcTable = new uint[256];
            calculateCrcTable();
        }

        public byte Reflect8(byte input)
        {
            byte resByte = 0;

            for (var i = 0; i < 8; i++)
            {
                if ((input & (1 << i)) != 0)
                {
                    resByte |= (byte)((1 << (7 - i)) & 0xFFu);
                }
            }
            return resByte;
        }

        public uint Reflect32(uint input)
        {
            uint reflected = 0u;

            for (var i = 0; i < 32; i++)
            {
                if ((input & (1 << i)) != 0)
                {
                    reflected |= (uint)((1 << (31 - i)) & 0xFFFFFFFFu);
                }
            }
            return reflected;
        }


        public uint getInitialDevident()
        {
            return _crcParameters.InitialValue;
        }

        public uint getDivsor()
        {
            return (uint)_crcParameters.PolyType;
        }

        public byte[] GetCrcPayload(byte[] rawPayload)
        {
            if(rawPayload == null)  throw new ArgumentNullException("rawPayload input arg is null");

            var payload = rawPayload;

            if(_crcParameters.InputReflected)
            {
                payload = rawPayload.Select( b => Reflect8(b) ).ToArray();
            }

            return payload;
        }

        public uint ShiftNextPaylodByteInToPlace(byte val)
        {
            return (uint)(val << (_polynomialBitwidth - 8));
        }

        public bool IsMsbSet(uint crc)
        {
            return ((crc & _crcMsb) == _crcMsb);
        }

        public uint GetCrcResult(uint reminder)
        {
            var crcReflected = _crcParameters.ResultReflected ? Reflect32(reminder) : reminder;

            var crcXored = (uint)(crcReflected ^ _crcParameters.FinalXorValue);

            return crcXored;
        }

        private int determinePolynomialBitwidth()
        {
            var crcShiftAmount = 32;

            switch (_crcParameters.CrcType)
            {
                case ChecksumType.Crc8:
                    crcShiftAmount = 8;
                    break;
                case ChecksumType.Crc16:
                    crcShiftAmount = 16;
                    break;
                case ChecksumType.Crc32:
                // using default crcShiftAmount alread set to 31.
                default:
                    break;
            }

            return crcShiftAmount;
        }

        private uint getCrcMsb()
        {
            return (1u << (_polynomialBitwidth - 1));
        }

        private void calculateCrcTable()
        {
            if (_crcParameters?.CrcType != ChecksumType.Crc32)
            {
                throw new NotImplementedException("currenlty only crc xor result tables are computed for the CRC-32!");
            }

            var polynomial = getDivsor();

            for (int dividend = 0; dividend < 256; dividend++) /* iterate over all possible input byte values 0 - 255 */
            {
                uint curByte = (uint)(dividend << 24); /* move dividend byte into MSB of 32Bit CRC */
                for (byte bit = 0; bit < 8; bit++)
                {
                    if ((curByte & 0x80000000) != 0)
                    {
                        curByte <<= 1;
                        curByte ^= polynomial;
                    }
                    else
                    {
                        curByte <<= 1;
                    }
                }

                _crcTable[dividend] = curByte;
            }
        }
    }
}
