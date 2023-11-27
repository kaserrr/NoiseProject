using System;
using System.Collections.Generic;
using System.Linq;
using Decoders;

namespace FLFreshPayloadDecoder
{
    class DecodeFlFreshPayloadDecoder
    {
        public static Dictionary<string, object> DecodeFlFreshPayload(string payloadHex)
        {
            // Convert hex string to byte array
            byte[] payloadBytes = Enumerable.Range(0, payloadHex.Length)
                                             .Where(x => x % 2 == 0)
                                             .Select(x => Convert.ToByte(payloadHex.Substring(x, 2), 16))
                                             .ToArray();
            Console.WriteLine(payloadBytes.Length);
            if (payloadBytes.Length != 10)
            {
                return new Dictionary<string, object> { { "Error", "Invalid payload length" } };
            }

            Dictionary<string, object> decodedData = new Dictionary<string, object>();

            // Extracting data bytes
            byte attemptsPending = payloadBytes[0];
            ushort batteryVoltage = BitConverter.ToUInt16(payloadBytes, 1);
            float batteryVoltageValue = batteryVoltage * 0.01f;
            ushort co2Level = BitConverter.ToUInt16(payloadBytes, 3);
            float temperature = BitConverter.ToUInt16(payloadBytes, 5) / 10.0f;
            float humidity = BitConverter.ToUInt16(payloadBytes, 7) / 10.0f;
            uint airPressure = BitConverter.ToUInt32(payloadBytes, 9);

            decodedData.Add("Attempts Pending", attemptsPending);
            decodedData.Add("Battery Voltage (V)", batteryVoltageValue);
            decodedData.Add("CO2 Level (ppm)", co2Level);
            decodedData.Add("Temperature (°C)", temperature);
            decodedData.Add("Humidity (%)", humidity);
            decodedData.Add("Air Pressure", airPressure);

            return decodedData;
        }
    }
}

