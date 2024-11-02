// See https://aka.ms/new-console-template for more information
using crc.common.interfaces;
using crc.logic.services;

var data4CRC = new byte[] { 0x01, 0x01, 0x01, 0x01 };

var typicalCrc32Params = CrcParametersFactory.createTypicalParams();

ICrcCalculator crcCalculator = new CrcCalculator(typicalCrc32Params);

var crc32Simple = crcCalculator.ComputeCrc(data4CRC);

Console.WriteLine("crc32Simple = {0:X}", crc32Simple);

//TODO fix table creation and calculation
//var crc32Table = crcCalculator.compute_CRC32_from_table(data4CRC);
//Console.WriteLine("crc32Table = {0:X}", crc32Table);

