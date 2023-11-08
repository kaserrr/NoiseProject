using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

public class RxInfo
{
    public string GatewayID { get; set; }
    public object Time { get; set; }
    public string TimeSinceGPSEpoch { get; set; }
    public int Rssi { get; set; }
    public int LoRaSNR { get; set; }
    public int Channel { get; set; }
    public int RfChain { get; set; }
    public int Board
    {
        get; set;
    }
    public int Antenna { get; set; }
    public Location Location { get; set; }
    public string FineTimestampType { get; set; }
    public string Context { get; set; }
    public string UplinkID { get; set; }
    public string CrcStatus { get; set; }
}

public class LoRaModulationInfo
{
    public int Bandwidth { get; set; }
    public int SpreadingFactor { get; set; }
    public string CodeRate { get; set; }
    public bool PolarizationInversion { get; set; }
}

public class TxInfo
{
    public int Frequency { get; set; }
    public string Modulation { get; set; }
    public LoRaModulationInfo LoRaModulationInfo { get; set; }
}

public class FCtrl
{
    public bool Adr { get; set; }
    public bool AdrAckReq { get; set; }
    public bool Ack { get; set; }
    public bool FPending { get; set; }
    public bool ClassB { get; set; }
}

public class Fhdr
{
    public string DevAddr { get; set; }
    public FCtrl FCtrl { get; set; }
    public int FCnt { get; set; }
    public object FOpts { get; set; }
}

public class FrmPayload
{
    public string Bytes { get; set; }
}

public class MacPayload
{
    public Fhdr Fhdr { get; set; }
    public int FPort { get; set; }
    public List<FrmPayload> FrmPayload { get; set; }
    public string Mic { get; set; }
}

public class Mhdr
{
    public string MType { get; set; }
    public string Major { get; set; }
}

public class PhyPayload
{
    public Mhdr Mhdr { get; set; }
    public MacPayload MacPayload { get; set; }
}

public class UplinkMetaData
{
    public List<RxInfo> RxInfo { get; set; }
    public TxInfo TxInfo { get; set; }
}

public class RootObject
{
    public UplinkMetaData UplinkMetaData { get; set; }
    public PhyPayload PhyPayload { get; set; }
}