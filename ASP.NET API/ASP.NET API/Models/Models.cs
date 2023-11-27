using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static ASP.NET_API.Models.Models;

namespace ASP.NET_API.Models
{
    public class Models
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        
        public class UplinkData
        {
            public List<UplinkMetaData>? uplinkMetaData { get; set; }
        }

        public class UplinkMetaData
        {
            public List<RxInfo>? rxInfo { get; set; }
            public TxInfo? txInfo { get; set; }
            public PhyPayload? phyPayload { get; set; }
        }

        public class RxInfo
        {
            public string? gatewayID { get; set; }
            public string? time { get; set; }
            public string? timeSinceGPSEpoch { get; set; }
            public int? rssi { get; set; }
            public int? loRaSNR { get; set; }
            public int? channel { get; set; }
            public int? rfChain { get; set; }
            public int? board { get; set; }
            public int? antenna { get; set; }
            public Location? location { get; set; }
            public string? fineTimestampType { get; set; }
            public string? context { get; set; }
            public string? uplinkID { get; set; }
            public string? crcStatus { get; set; }
        }

        public class TxInfo
        {
            public int? frequency { get; set; }
            public string? modulation { get; set; }
            public LoRaModulationInfo? loRaModulationInfo { get; set; }
        }

        public class LoRaModulationInfo
        {
            public int? bandwidth { get; set; }
            public int? spreadingFactor { get; set; }
            public string? codeRate { get; set; }
            public bool? polarizationInversion { get; set; }
        }

        public class Location
        {
            public double? latitude { get; set; }
            public double? longitude { get; set; }
            public int? altitude { get; set; }
            public string? source { get; set; }
            public int? accuracy { get; set; }
        }

        public class PhyPayload
        {
            public Mhdr? mhdr { get; set; }
            public MacPayload? macPayload { get; set; }
            public string? mic { get; set; }
        }

        public class Mhdr
        {
            public string? mType { get; set; }
            public string? major { get; set; }
        }

        public class MacPayload
        {
            public Fhdr? fhdr { get; set; }
            public int? fPort { get; set; }
            public List<FrmPayload>? frmPayload { get; set; }
        }

        public class Fhdr
        {
            public string? devAddr { get; set; }
            public FCtrl? fCtrl { get; set; }
            public int? fCnt { get; set; }
            public object? fOpts { get; set; }
        }

        public class FCtrl
        {
            public bool? adr { get; set; }
            public bool? adrAckReq { get; set; }
            public bool? ack { get; set; }
            public bool? fPending { get; set; }
            public bool? classB { get; set; }
        }

        public class FrmPayload
        {
            public string? bytes { get; set; }
        }
    }
}

