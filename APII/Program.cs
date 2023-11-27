using System;
using System.Collections.Generic;
using ElsysPayloadDecoder;
using FLSmartPayloadDecoder;
using FLFreshPayloadDecoder;
using FLFineDustPayloadDecoder;

namespace Decoders
{
    interface IDecoder
    {
        Dictionary<string, object> Decode(string payloadHexStr);
    }
    

    class Decoder : IDecoder
    {
        public Dictionary<string, object> Decode(string payloadHexStr)
        {
            Dictionary<string, object> decodedData;

            if (payloadHexStr.StartsWith("031EC5"))
            {
                // Use FLFineDustPayloadDecoder for FLFineDust payload
                decodedData = DecodeFLFineDustLoRaPayloadDecoder.DecodeFLFineDustPayload(payloadHexStr);
            }
            else if (payloadHexStr.StartsWith("0500A0"))
            {
                // Use FLFreshPayloadDecoder for FLFresh payload
                decodedData = DecodeFlFreshPayloadDecoder.DecodeFlFreshPayload(payloadHexStr);
            }
            else if (payloadHexStr.StartsWith("0A00003C"))
            {
                // Use FLSmartPayloadDecoder for FLSmart payload
                decodedData = DecodeFLSmartPayloadDecoder.DecodeFLSmartPayload(payloadHexStr);
            }
            else if (payloadHexStr.StartsWith("0100e202"))
            {
                // Use ElsysPayloadDecoder for Elsys payload
                decodedData = PayloadDecoder.ElsysPayloadDecoder.DecodeElsysPayload(payloadHexStr);
            }
            else
            {
                // Default to a generic decoding mechanism or throw an exception
                throw new ArgumentException("Unsupported payload type");
            }

            // Other decoding logic if needed

            return decodedData;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Decoder decoder = new Decoder();
            Dictionary<string, object> decodedData = decoder.Decode("0500A086000032000001F4010000000A");

            // Print the decoded data
            foreach (var entry in decodedData)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
