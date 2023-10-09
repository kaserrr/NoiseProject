using Newtonsoft.Json;

public class Location
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public int altitude { get; set; }
    public string source { get; set; }
    public int accuracy { get; set; }
}

public class Root
{
    public string gatewayID { get; set; }
    public DateTime time { get; set; }
    public object timeSinceGPSEpoch { get; set; }
    public int rssi { get; set; }
    public double loRaSNR { get; set; }
    public int channel { get; set; }
    public int rfChain { get; set; }
    public int board { get; set; }
    public int antenna { get; set; }
    public Location location { get; set; }
    public string fineTimestampType { get; set; }
    public string context { get; set; }
    public string uplinkID { get; set; }
    public string crcStatus { get; set; }
}

string json = @"{
{
    ""gatewayID"": ""AAAAAAAAACI="",
    ""time"": ""2023-10-03T11:22:11.178121Z"",
    ""timeSinceGPSEpoch"": null,
    ""rssi"": -116,
    ""loRaSNR"": -5.2,
    ""channel"": 0,
    ""rfChain"": 1,
    ""board"": 0,
    ""antenna"": 0,
    ""location"": {
        ""latitude"": 51.81031,
        ""longitude"": 4.62213,
        ""altitude"": 9,
        ""source"": ""UNKNOWN"",
        ""accuracy"": 0
    },
    ""fineTimestampType"": ""NONE"",
    ""context"": ""DQaIyw=="",
    ""uplinkID"": ""k0OrDvjtSQGQN/eQ10be2Q=="",
    ""crcStatus"": ""CRC_OK""
}

    // hier komt de ontvangen payload.
}";

Root root = JsonConvert.DeserializeObject<Root>(json);

Console.WriteLine($"Gateway ID: {root.gatewayID}");