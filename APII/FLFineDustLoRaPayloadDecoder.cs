using System;
using System.Collections.Generic;
using System.Linq;

class FLFineDustPayloadDecoder
{
    static void Main4()
    {
        // Example usage
        string rawPayloadHex = "031EC501F30284034C04AC71C201";
        var decodedData = DecodeFLFineDustPayload(rawPayloadHex);

        // Print the decoded data
        foreach (var entry in decodedData)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    static Dictionary<string, object> DecodeFLFineDustPayload(string payloadHex)
    {
        // Convert hex string to byte array
        byte[] payloadBytes = Enumerable.Range(0, payloadHex.Length)
                                         .Where(x => x % 2 == 0)
                                         .Select(x => Convert.ToByte(payloadHex.Substring(x, 2), 16))
                                         .ToArray();

        if (payloadBytes.Length != 14)
        {
            return new Dictionary<string, object> { { "Error", "Invalid payload length" } };
        }

        Dictionary<string, object> decodedData = new Dictionary<string, object>();

        // Extracting data bytes
        byte attemptsPending = payloadBytes[0];
        byte batteryVoltage = payloadBytes[1];
        ushort pm1Concentration = BitConverter.ToUInt16(payloadBytes, 2);
        ushort pm2_5Concentration = BitConverter.ToUInt16(payloadBytes, 4);
        ushort pm4Concentration = BitConverter.ToUInt16(payloadBytes, 6);
        ushort pm10Concentration = BitConverter.ToUInt16(payloadBytes, 8);
        ushort temperature = BitConverter.ToUInt16(payloadBytes, 10);
        ushort humidity = BitConverter.ToUInt16(payloadBytes, 12);

        // Applying factors and units
        float batteryVoltageValue = batteryVoltage / 10.0f;
        float pm1ConcentrationValue = pm1Concentration;
        float pm2_5ConcentrationValue = pm2_5Concentration / 10.0f;
        float pm4ConcentrationValue = pm4Concentration / 10.0f;
        float pm10ConcentrationValue = pm10Concentration / 10.0f;
        float temperatureValue = temperature / 100.0f;
        float humidityValue = humidity / 10.0f;

        decodedData.Add("Attempts Pending", attemptsPending);
        decodedData.Add("Battery Voltage (V)", batteryVoltageValue);
        decodedData.Add("PM 1.0 Concentration (ug/m3)", pm1ConcentrationValue);
        decodedData.Add("PM 2.5 Concentration (ug/m3)", pm2_5ConcentrationValue);
        decodedData.Add("PM 4.0 Concentration (ug/m3)", pm4ConcentrationValue);
        decodedData.Add("PM 10 Concentration (ug/m3)", pm10ConcentrationValue);
        decodedData.Add("Temperature (K)", temperatureValue);
        decodedData.Add("Humidity (%)", humidityValue);

        return decodedData;
    }
}
