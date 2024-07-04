using System.Collections.Generic;

namespace NexStar.Net.Enums
{
    public class TrackingMode
    {
        public static readonly TrackingMode Off = new TrackingMode(0x0, "Off");
        public static readonly TrackingMode AltAz = new TrackingMode(0x1, "Alt/Az");
        public static readonly TrackingMode EqNorth = new TrackingMode(0x2, "EQ North");
        public static readonly TrackingMode EqSouth = new TrackingMode(0x3, "EQ South");

        public readonly byte Identifier;
        public readonly string Description;
        
        private TrackingMode(byte id, string description)
        {
            Identifier = id;
            Description = description;
        }

        public static TrackingMode GetTrackingMode(int modeId)
        {
            switch (modeId)
            {
                case 0:
                    return TrackingMode.Off;
                case 1:
                    return TrackingMode.AltAz;
                case 2:
                    return TrackingMode.EqNorth;
                case 3:
                    return TrackingMode.EqSouth;
                default:
                    return TrackingMode.Off;
            }
        }

        public static List<TrackingMode> GetTrackingModes()
        {
            return new List<TrackingMode>()
            {
                Off,
                AltAz,
                EqNorth,
                EqSouth
            };
        }
    }
}
