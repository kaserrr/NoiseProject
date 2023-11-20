using System;
using System.Collections.Generic;

class Decoder
{
    static void Main2(string[] args)
    {
        string data = "447046e66e5359b1838cb674c8713e03"; // Replace with your hexadecimal data
        Dictionary<string, object> result = ElsysPayloadDecoder.DecodeElsysPayload(data);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}


public class ElsysPayloadDecoder
{
    private const byte TYPE_TEMP = 0x01;
    private const byte TYPE_RH = 0x02;
    private const byte TYPE_ACC = 0x03;
    private const byte TYPE_LIGHT = 0x04;
    private const byte TYPE_MOTION = 0x05;
    private const byte TYPE_CO2 = 0x06;
    private const byte TYPE_VDD = 0x07;
    private const byte TYPE_ANALOG1 = 0x08;
    private const byte TYPE_GPS = 0x09;
    private const byte TYPE_PULSE1 = 0x0A;
    private const byte TYPE_PULSE1_ABS = 0x0B;
    private const byte TYPE_EXT_TEMP1 = 0x0C;
    private const byte TYPE_EXT_DIGITAL = 0x0D;
    private const byte TYPE_EXT_DISTANCE = 0x0E;
    private const byte TYPE_ACC_MOTION = 0x0F;
    private const byte TYPE_IR_TEMP = 0x10;
    private const byte TYPE_OCCUPANCY = 0x11;
    private const byte TYPE_WATERLEAK = 0x12;
    private const byte TYPE_GRIDEYE = 0x13;
    private const byte TYPE_PRESSURE = 0x14;
    private const byte TYPE_SOUND = 0x15;
    private const byte TYPE_PULSE2 = 0x16;
    private const byte TYPE_PULSE2_ABS = 0x17;
    private const byte TYPE_ANALOG2 = 0x18;
    private const byte TYPE_EXT_TEMP2 = 0x19;
    private const byte TYPE_EXT_DIGITAL2 = 0x1A;
    private const byte TYPE_EXT_ANALOG_UV = 0x1B;
    private const byte TYPE_TVOC = 0x1C;
    private const byte TYPE_DEBUG = 0x3D;

    private static int Bin16Dec(int bin)
    {
        int num = bin & 0xFFFF;
        if ((0x8000 & num) != 0)
            num = -(0x010000 - num);
        return num;
    }

    private static int Bin8Dec(int bin)
    {
        int num = bin & 0xFF;
        if ((0x80 & num) != 0)
            num = -(0x0100 - num);
        return num;
    }

    private static byte[] HexToBytes(string hex)
    {
        List<byte> bytes = new List<byte>();
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes.Add(Convert.ToByte(hex.Substring(i, 2), 16));
        }
        return bytes.ToArray();
    }

    public static Dictionary<string, object> DecodeElsysPayload(string data)
    {
        Dictionary<string, object> obj = new Dictionary<string, object>();
        byte[] bytes = HexToBytes(data);

        for (int i = 0; i < bytes.Length; i++)
        {
            switch (bytes[i])
            {
                case TYPE_TEMP:
                    int temp = (bytes[i + 1] << 8) | bytes[i + 2];
                    temp = Bin16Dec(temp);
                    obj["temperature"] = temp / 10.0;
                    i += 2;
                    break;
                case TYPE_RH:
                    obj["humidity"] = bytes[i + 1];
                    i += 1;
                    break;
                case TYPE_ACC:
                    obj["x"] = Bin8Dec(bytes[i + 1]);
                    obj["y"] = Bin8Dec(bytes[i + 2]);
                    obj["z"] = Bin8Dec(bytes[i + 3]);
                    i += 3;
                    break;
                case TYPE_LIGHT:
                    obj["light"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_MOTION:
                    obj["motion"] = bytes[i + 1];
                    i += 1;
                    break;
                case TYPE_CO2:
                    obj["co2"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_VDD:
                    obj["vdd"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_ANALOG1:
                    obj["analog1"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_GPS:
                    i++;
                    obj["lat"] = (bytes[i] | (bytes[i + 1] << 8) | (bytes[i + 2] << 16) | ((bytes[i + 2] & 0x80) != 0 ? 0xFF << 24 : 0)) / 10000.0;
                    obj["long"] = (bytes[i + 3] | (bytes[i + 4] << 8) | (bytes[i + 5] << 16) | ((bytes[i + 5] & 0x80) != 0 ? 0xFF << 24 : 0)) / 10000.0;
                    i += 5;
                    break;
                case TYPE_PULSE1:
                    obj["pulse1"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_PULSE1_ABS:
                    int pulseAbs = (bytes[i + 1] << 24) | (bytes[i + 2] << 16) | (bytes[i + 3] << 8) | bytes[i + 4];
                    obj["pulseAbs"] = pulseAbs;
                    i += 4;
                    break;
                case TYPE_EXT_TEMP1:
                    int extTemp = (bytes[i + 1] << 8) | bytes[i + 2];
                    extTemp = Bin16Dec(extTemp);
                    obj["externalTemperature"] = extTemp / 10.0;
                    i += 2;
                    break;
                case TYPE_EXT_DIGITAL:
                    obj["digital"] = bytes[i + 1];
                    i += 1;
                    break;
                case TYPE_EXT_DISTANCE:
                    obj["distance"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_ACC_MOTION:
                    obj["accMotion"] = bytes[i + 1];
                    i += 1;
                    break;
                case TYPE_IR_TEMP:
                    int iTemp = (bytes[i + 1] << 8) | bytes[i + 2];
                    iTemp = Bin16Dec(iTemp);
                    int eTemp = (bytes[i + 3] << 8) | bytes[i + 4];
                    eTemp = Bin16Dec(eTemp);
                    obj["irInternalTemperature"] = iTemp / 10.0;
                    obj["irExternalTemperature"] = eTemp / 10.0;
                    i += 4;
                    break;
                case TYPE_OCCUPANCY:
                    obj["occupancy"] = bytes[i + 1];
                    i += 1;
                    break;
                case TYPE_WATERLEAK:
                    obj["waterleak"] = bytes[i + 1];
                    i += 1;
                    break;
                case TYPE_GRIDEYE:
                    byte refValue = bytes[i + 1];
                    i++;
                    List<double> grideyeData = new List<double>();
                    for (int j = 0; j < 64; j++)
                    {
                        grideyeData.Add(refValue + (bytes[i + 1 + j] / 10.0));
                    }
                    obj["grideye"] = grideyeData;
                    i += 64;
                    break;
                case TYPE_PRESSURE:
                    int pressure = (bytes[i + 1] << 24) | (bytes[i + 2] << 16) | (bytes[i + 3] << 8) | bytes[i + 4];
                    obj["pressure"] = pressure / 1000.0;
                    i += 4;
                    break;
                case TYPE_SOUND:
                    obj["soundPeak"] = bytes[i + 1];
                    obj["soundAvg"] = bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_PULSE2:
                    obj["pulse2"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_PULSE2_ABS:
                    obj["pulseAbs2"] = (bytes[i + 1] << 24) | (bytes[i + 2] << 16) | (bytes[i + 3] << 8) | bytes[i + 4];
                    i += 4;
                    break;
                case TYPE_ANALOG2:
                    obj["analog2"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                case TYPE_EXT_TEMP2:
                    int extTemp2 = (bytes[i + 1] << 8) | bytes[i + 2];
                    extTemp2 = Bin16Dec(extTemp2);
                    if (obj.ContainsKey("externalTemperature2"))
                    {
                        if (obj["externalTemperature2"] is int)
                        {
                            int prevTemp = (int)obj["externalTemperature2"];
                            obj["externalTemperature2"] = new List<int> { prevTemp, extTemp2 / 10 };
                        }
                        else if (obj["externalTemperature2"] is List<int> tempList)
                        {
                            tempList.Add(extTemp2 / 10);
                        }
                    }
                    else
                    {
                        obj["externalTemperature2"] = extTemp2 / 10;
                    }
                    i += 2;
                    break;
                case TYPE_EXT_DIGITAL2:
                    obj["digital2"] = bytes[i + 1];
                    i += 1;
                    break;
                case TYPE_EXT_ANALOG_UV:
                    int analogUv = (bytes[i + 1] << 24) | (bytes[i + 2] << 16) | (bytes[i + 3] << 8) | bytes[i + 4];
                    obj["analogUv"] = analogUv;
                    i += 4;
                    break;
                case TYPE_TVOC:
                    obj["tvoc"] = (bytes[i + 1] << 8) | bytes[i + 2];
                    i += 2;
                    break;
                default:
                    i = bytes.Length;
                    break;
            }
        }
        return obj;
    }
}



