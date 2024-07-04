using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.Net.Enums
{
    public class TelescopeDevice
    {
        public static readonly TelescopeDevice AzmRaMotor = new TelescopeDevice(0x10, "AZM/RA Motor");
        public static readonly TelescopeDevice AltDecMotor = new TelescopeDevice(0x11, "ALT/DEC Motor");
        public static readonly TelescopeDevice GpsUnit = new TelescopeDevice(0xB0, "GPS Unit");
        public static readonly TelescopeDevice Rtc = new TelescopeDevice(0xB2, "RTC (CGE Only)");

        public readonly byte DeviceId;
        public readonly string Description;

        private TelescopeDevice(byte deviceId, string descriptor)
        {
            DeviceId = deviceId;
            Description = descriptor;
        }
    }
}
