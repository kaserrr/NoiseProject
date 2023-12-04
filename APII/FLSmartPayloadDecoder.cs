using System;
using System.Collections.Generic;
using Decoders;

namespace FLSmartPayloadDecoder
{
    class DecodeFLSmartPayloadDecoder
    {
        public static Dictionary<string, object> DecodeFLSmartPayload(byte[] payloadBytes)
        {
            if (payloadBytes.Length < 16)
            {
                return new Dictionary<string, object> { { "Error", "Invalid payload length" } };
            }

            Dictionary<string, object> decodedData = new Dictionary<string, object>();

            // Extracting data bytes
            byte attempts = payloadBytes[0];
            ushort pending = BitConverter.ToUInt16(payloadBytes, 1);
            ushort batteryVoltage = BitConverter.ToUInt16(payloadBytes, 3);
            float batteryVoltageValue = batteryVoltage * 0.01f;
            ushort co2Level = BitConverter.ToUInt16(payloadBytes, 5);
            float temperature = BitConverter.ToUInt16(payloadBytes, 7) * 0.01f;
            uint humidity = BitConverter.ToUInt32(payloadBytes, 9);
            float humidityValue = humidity * 0.1f;
            byte lightIntensity = payloadBytes[13];
            byte soundMax = payloadBytes[14];
            byte soundAvg = payloadBytes[15];

            decodedData.Add("Attempts", attempts);
            decodedData.Add("Pending", pending);
            decodedData.Add("Battery Voltage (V)", batteryVoltageValue);
            decodedData.Add("CO2 Level (ppm)", co2Level);
            decodedData.Add("Temperature (K)", temperature);
            decodedData.Add("Humidity (%)", humidityValue);
            decodedData.Add("Light Intensity (Lux)", lightIntensity);
            decodedData.Add("Sound Maximum (dB(A))", soundMax);
            decodedData.Add("Sound Average (dB(A))", soundAvg);

            return decodedData;
        }
    }
}
