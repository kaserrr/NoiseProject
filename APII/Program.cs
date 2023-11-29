using System;
using System.Text;
using System.Collections.Generic;
using ElsysPayloadDecoder;
using FLSmartPayloadDecoder;
using FLFreshPayloadDecoder;
using FLFineDustPayloadDecoder;

namespace Decoders
{
    interface IDecoder
    {
        Dictionary<string, object> Decode(byte[] payloadBytes);
    }

    class Decoder : IDecoder
    {
        public Dictionary<string, object> Decode(byte[] payloadBytes)
        {
            Dictionary<string, object> decodedData;

            // Use the appropriate payload decoder
            // Comment/Uncomment the desired decoder based on your requirements
            /*decodedData = DecodeFLFineDustLoRaPayloadDecoder.DecodeFLFineDustPayload(payloadBytes);*/
            /*decodedData = DecodeFlFreshPayloadDecoder.DecodeFlFreshPayload(payloadBytes);*/
            /*decodedData = DecodeFLSmartPayloadDecoder.DecodeFLSmartPayload(payloadBytes);*/
            decodedData = PayloadDecoder.DecodeElsysPayload(payloadBytes);
            return decodedData;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Decoder decoder = new Decoder();

            // Replace this string with your actual Base64 payload
            string FLDust = "SGVsbG8sIFdvcmxkIQAAAAAAAA==";
            string FLFresh = "SGVsbG8sIFdvcmxkIQ==";
            string FLSmart = "AAMAAKAAAQAAAAMAAAAAAHgAAAAYAAADAAAAGAAAABgAAAAoAAAAEAAAACAAAAAQAAAAkAAA";
            string Elsys = "SGVsbG8sIFdvcmxkIQAAAAAAAA==";

            // Convert Base64 to bytes
            byte[] payloadBytes = Convert.FromBase64String(Elsys);
            Console.WriteLine("Payload Bytes:");
            foreach (byte b in payloadBytes)
            {
                Console.Write(b.ToString("X2") + " "); // Prints each byte in hexadecimal format
            }

            Dictionary<string, object> decodedData = decoder.Decode(payloadBytes);

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
