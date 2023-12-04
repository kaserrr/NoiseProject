using System;
using System.Collections.Generic;
using ElsysPayloadDecoder;
using FLSmartPayloadDecoder;
using FLFreshPayloadDecoder;
using FLFineDustPayloadDecoder;
using System.Reflection.Metadata.Ecma335;

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

            /*decodedData = DecodeFLFineDustLoRaPayloadDecoder.DecodeFLFineDustPayload(payloadHexStr);*/

            decodedData = DecodeFlFreshPayloadDecoder.DecodeFlFreshPayload(payloadHexStr);

            /*decodedData = DecodeFLSmartPayloadDecoder.DecodeFLSmartPayload(payloadHexStr);*/

            /*decodedData = PayloadDecoder.ElsysPayloadDecoder.DecodeElsysPayload(payloadHexStr);*/

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