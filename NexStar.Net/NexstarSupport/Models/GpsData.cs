using System;

namespace NexStar.Net.NexstarSupport.Models
{
    public class GpsData
    {
        public Int16 DegreesLatitude { get; set; }
        public Int16 MinutesLatitude { get; set; }
        public Int16 SecondsLatitude { get; set; }
        public Boolean IsNorth { get; set; }
        public Int16 DegreesLongitude { get; set; }
        public Int16 MinutesLongitude { get; set; }
        public Int16 SecondsLongitude { get; set; }
        public Boolean IsWest { get; set; }

        public GpsData() { }

        public override string ToString()
        {
            return $"{(IsNorth ? "N" : "S")}{DegreesLatitude}°{MinutesLatitude}'{SecondsLatitude}\"|{(IsWest ? "W" : "E")}{DegreesLongitude}°{MinutesLongitude}'{SecondsLongitude}\"";
        }
    }
}
