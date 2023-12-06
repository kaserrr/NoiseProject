using System;
using System.Collections.Generic;
using System.Linq;
using Decoders;

namespace FLFreshPayloadDecoder
{
    class DecodeFlFreshPayloadDecoder
    {
        public static Dictionary<string, object> DecodeFlFreshPayload(byte[] payloadBytes)
        {
            if (payloadBytes.Length != 10)
            {
                return new Dictionary<string, object> { { "Error", "Invalid payload length" } };
            }

            Dictionary<string, object> decodedData = new Dictionary<string, object>();

            // Extracting data bytes
            ushort batteryVoltage = BitConverter.ToUInt16(payloadBytes, 0);
            float batteryVoltageValue = batteryVoltage * 0.01f;
            ushort co2Level = BitConverter.ToUInt16(payloadBytes, 2);
            float temperature = BitConverter.ToUInt16(payloadBytes, 4) / 10.0f;
            float humidity = BitConverter.ToUInt16(payloadBytes, 6) / 10.0f;
            uint airPressure = BitConverter.ToUInt32(payloadBytes, 8);

            decodedData.Add("Battery Voltage (V)", batteryVoltageValue);
            decodedData.Add("CO2 Level (ppm)", co2Level);
            decodedData.Add("Temperature (°C)", temperature);
            decodedData.Add("Humidity (%)", humidity);
            decodedData.Add("Air Pressure", airPressure);

            return decodedData;
        }
    }

}
