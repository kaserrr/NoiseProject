using System;
using System.Collections.Generic;

public class FLSmartPayloadDecoder
{
    public static void Main6()
    {
        // Example payload bytes (replace this with your actual payload data)
        string payloadHex = "0A00003C0005000000000000140A0F37";
        byte[] payloadBytes = StringToByteArray(payloadHex);

        Dictionary<string, object> decodedData = DecodeFLSmartOfficeLoRaPayload(payloadBytes);

        foreach (var kvp in decodedData)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

    }

    public static Dictionary<string, object> DecodeFLSmartOfficeLoRaPayload(byte[] data)
    {
        if (data.Length != 16)
        {
            return new Dictionary<string, object> { { "Error", "Invalid payload length" } };
        }

        Dictionary<string, object> decodedData = new Dictionary<string, object>();

        // Extracting data bytes
        byte attempts = data[0];
        ushort pending = BitConverter.ToUInt16(data, 1);
        ushort batteryVoltage = BitConverter.ToUInt16(data, 3);
        float batteryVoltageValue = batteryVoltage * 0.01f;
        ushort co2Level = BitConverter.ToUInt16(data, 5);
        float temperature = BitConverter.ToUInt16(data, 7) * 0.01f;
        uint humidity = BitConverter.ToUInt32(data, 9);
        float humidityValue = humidity * 0.1f;
        byte lightIntensity = data[13];
        byte soundMax = data[14];
        byte soundAvg = data[15];

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

    public static byte[] StringToByteArray(string hex)
    {
        int numberChars = hex.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }
}
