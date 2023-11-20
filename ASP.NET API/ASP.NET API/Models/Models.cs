using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static ASP.NET_API.Models.Models;

namespace ASP.NET_API.Models
{
    public class Models
    {
        public class UplinkData
        {
            public List<UplinkMetaData> uplinkMetaData { get; set; }
        }
        public class UplinkMetaData
        {
            public List<RxInfo>? rxInfo { get; set; }
            public TxInfo? txInfo { get; set; }
            public PhyPayload? phyPayload { get; set; } // Voeg de phyPayload-eigenschap toe
        }

        public class RxInfo
        {
            public string? gatewayID { get; set; }
            public string? time { get; set; }
            public string? timeSinceGPSEpoch { get; set; }
            public int rssi { get; set; }
            public int loRaSNR { get; set; }
            public int channel { get; set; }
            public int rfChain { get; set; }
            public int board { get; set; }
            public int antenna { get; set; }
            public Location? location { get; set; }
            public string? fineTimestampType { get; set; }
            public string? context { get; set; }
            public string? uplinkID { get; set; }
            public string? crcStatus { get; set; }
        }

        public class TxInfo
        {
            public int frequency { get; set; }
            public string? modulation { get; set; }
            public LoRaModulationInfo? loRaModulationInfo { get; set; }
        }

        public class LoRaModulationInfo
        {
            public int bandwidth { get; set; }
            public int spreadingFactor { get; set; }
            public string? codeRate { get; set; }
            public bool polarizationInversion { get; set; }
        }

        public class Location
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public int altitude { get; set; }
            public string? source { get; set; }
            public int accuracy { get; set; }
        }

        public class PhyPayload
        {
            public Mhdr mhdr { get; set; }
            public MacPayload macPayload { get; set; }
            public string mic { get; set; }
        }

        public class Mhdr
        {
            public string mType { get; set; }
            public string major { get; set; }
        }

        public class MacPayload
        {
            public Fhdr? fhdr { get; set; }
            public int fPort { get; set; }
            public List<FrmPayload>? frmPayload { get; set; }
        }

        public class Fhdr
        {
            public string? devAddr { get; set; }
            public FCtrl? fCtrl { get; set; }
            public int fCnt { get; set; }
            public object? fOpts { get; set; }
        }

        public class FCtrl
        {
            public bool adr { get; set; }
            public bool adrAckReq { get; set; }
            public bool ack { get; set; }
            public bool fPending { get; set; }
            public bool classB { get; set; }
        }

        public class FrmPayload
        {
            public string? bytes { get; set; }
        }
        public static void Main(string[] args)
        {
            string json = @"{[
    {
        ""uplinkMetaData"": {
            ""rxInfo"": [
                {
                    ""gatewayID"": ""cHb/AFYFC1E="",
                    ""time"": null,
                    ""timeSinceGPSEpoch"": ""1380367326.375s"",
                    ""rssi"": -114,
                    ""loRaSNR"": 4,
                    ""channel"": 5,
                    ""rfChain"": 0,
                    ""board"": 261,
                    ""antenna"": 0,
                    ""location"": {
                        ""latitude"": 51.8156623840332,
                        ""longitude"": 4.641382694244385,
                        ""altitude"": 19,
                        ""source"": ""UNKNOWN"",
                        ""accuracy"": 0
                    },
                    ""fineTimestampType"": ""NONE"",
                    ""context"": ""zQm/PA=="",
                    ""uplinkID"": ""b0DKkS1eTHOU4CwQ8zLbdA=="",
                    ""crcStatus"": ""CRC_OK""
                }
            ],
            ""txInfo"": {
                ""frequency"": 868100000,
                ""modulation"": ""LORA"",
                ""loRaModulationInfo"": {
                    ""bandwidth"": 125,
                    ""spreadingFactor"": 7,
                    ""codeRate"": ""4/5"",
                    ""polarizationInversion"": false
                }
            }
        },
        ""phyPayload"": {
            ""mhdr"": {
                ""mType"": ""UnconfirmedDataUp"",
                ""major"": ""LoRaWANR1""
            },
            ""macPayload"": {
                ""fhdr"": {
                    ""devAddr"": ""00cd077a"",
                    ""fCtrl"": {
                        ""adr"": true,
                        ""adrAckReq"": false,
                        ""ack"": false,
                        ""fPending"": false,
                        ""classB"": false
                    },
                    ""fCnt"": 51021,
                    ""fOpts"": null
                },
                ""fPort"": 5,
                ""frmPayload"": [
                    {
                        ""bytes"": ""sAKZzDVTeGUxL3nKsGOeVw==""
                    }
                ]
            },
            ""mic"": ""0b319887""
        }
    }
]
}"; // Replace with your JSON data.

            List<UplinkMetaData> uplinkMetaDataList = JsonConvert.DeserializeObject<List<UplinkMetaData>>(json);

            // Access the 'phyPayload' property.
            PhyPayload phyPayload = uplinkMetaDataList[0].phyPayload;

            // Now you can access properties within 'phyPayload'.
            string micValue = phyPayload.mic;

            //Console.WriteLine("MIC Value: " + micValue);
            Console.WriteLine("Test");
            // You can access other properties as needed.
        }

    }
}

    