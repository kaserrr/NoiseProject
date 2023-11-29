using System;
using System.Collections.Generic;
using Decoders;

namespace ElsysPayloadDecoder
{
    class PayloadDecoder
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

        public static Dictionary<string, object> DecodeElsysPayload(byte[] payloadBytes)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();

            for (int i = 0; i < payloadBytes.Length; i++)
            {
                switch (payloadBytes[i])
                {
                    case TYPE_TEMP:
                        int temp = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        temp = Bin16Dec(temp);
                        obj["temperature"] = temp / 10.0;
                        i += 2;
                        break;
                    case TYPE_RH:
                        obj["humidity"] = payloadBytes[i + 1];
                        i += 1;
                        break;
                    case TYPE_ACC:
                        obj["x"] = Bin8Dec(payloadBytes[i + 1]);
                        obj["y"] = Bin8Dec(payloadBytes[i + 2]);
                        obj["z"] = Bin8Dec(payloadBytes[i + 3]);
                        i += 3;
                        break;
                    case TYPE_LIGHT:
                        obj["light"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_MOTION:
                        obj["motion"] = payloadBytes[i + 1];
                        i += 1;
                        break;
                    case TYPE_CO2:
                        obj["co2"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_VDD:
                        obj["vdd"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_ANALOG1:
                        obj["analog1"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_GPS:
                        i++;
                        obj["lat"] = (payloadBytes[i] | (payloadBytes[i + 1] << 8) | (payloadBytes[i + 2] << 16) | ((payloadBytes[i + 2] & 0x80) != 0 ? 0xFF << 24 : 0)) / 10000.0;
                        obj["long"] = (payloadBytes[i + 3] | (payloadBytes[i + 4] << 8) | (payloadBytes[i + 5] << 16) | ((payloadBytes[i + 5] & 0x80) != 0 ? 0xFF << 24 : 0)) / 10000.0;
                        i += 5;
                        break;
                    case TYPE_PULSE1:
                        obj["pulse1"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_PULSE1_ABS:
                        int pulseAbs = (payloadBytes[i + 1] << 24) | (payloadBytes[i + 2] << 16) | (payloadBytes[i + 3] << 8) | payloadBytes[i + 4];
                        obj["pulseAbs"] = pulseAbs;
                        i += 4;
                        break;
                    case TYPE_EXT_TEMP1:
                        int extTemp = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        extTemp = Bin16Dec(extTemp);
                        obj["externalTemperature"] = extTemp / 10.0;
                        i += 2;
                        break;
                    case TYPE_EXT_DIGITAL:
                        obj["digital"] = payloadBytes[i + 1];
                        i += 1;
                        break;
                    case TYPE_EXT_DISTANCE:
                        obj["distance"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_ACC_MOTION:
                        obj["accMotion"] = payloadBytes[i + 1];
                        i += 1;
                        break;
                    case TYPE_IR_TEMP:
                        int iTemp = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        iTemp = Bin16Dec(iTemp);
                        int eTemp = (payloadBytes[i + 3] << 8) | payloadBytes[i + 4];
                        eTemp = Bin16Dec(eTemp);
                        obj["irInternalTemperature"] = iTemp / 10.0;
                        obj["irExternalTemperature"] = eTemp / 10.0;
                        i += 4;
                        break;
                    case TYPE_OCCUPANCY:
                        obj["occupancy"] = payloadBytes[i + 1];
                        i += 1;
                        break;
                    case TYPE_WATERLEAK:
                        obj["waterleak"] = payloadBytes[i + 1];
                        i += 1;
                        break;
                    case TYPE_GRIDEYE:
                        byte refValue = payloadBytes[i + 1];
                        i++;
                        List<double> grideyeData = new List<double>();
                        for (int j = 0; j < 64; j++)
                        {
                            grideyeData.Add(refValue + (payloadBytes[i + 1 + j] / 10.0));
                        }
                        obj["grideye"] = grideyeData;
                        i += 64;
                        break;
                    case TYPE_PRESSURE:
                        int pressure = (payloadBytes[i + 1] << 24) | (payloadBytes[i + 2] << 16) | (payloadBytes[i + 3] << 8) | payloadBytes[i + 4];
                        obj["pressure"] = pressure / 1000.0;
                        i += 4;
                        break;
                    case TYPE_SOUND:
                        obj["soundPeak"] = payloadBytes[i + 1];
                        obj["soundAvg"] = payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_PULSE2:
                        obj["pulse2"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_PULSE2_ABS:
                        obj["pulseAbs2"] = (payloadBytes[i + 1] << 24) | (payloadBytes[i + 2] << 16) | (payloadBytes[i + 3] << 8) | payloadBytes[i + 4];
                        i += 4;
                        break;
                    case TYPE_ANALOG2:
                        obj["analog2"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    case TYPE_EXT_TEMP2:
                        int extTemp2 = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
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
                        obj["digital2"] = payloadBytes[i + 1];
                        i += 1;
                        break;
                    case TYPE_EXT_ANALOG_UV:
                        int analogUv = (payloadBytes[i + 1] << 24) | (payloadBytes[i + 2] << 16) | (payloadBytes[i + 3] << 8) | payloadBytes[i + 4];
                        obj["analogUv"] = analogUv;
                        i += 4;
                        break;
                    case TYPE_TVOC:
                        obj["tvoc"] = (payloadBytes[i + 1] << 8) | payloadBytes[i + 2];
                        i += 2;
                        break;
                    default:
                        i = payloadBytes.Length;
                        break;
                }
            }

            // Display the decoded data
            Console.WriteLine("Decoded Data:");
            foreach (var entry in obj)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            return obj;
        }
    }
}
