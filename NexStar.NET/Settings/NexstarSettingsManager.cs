using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.Net.Settings
{
    public static class NexStarSettingsManager
    {
        public static int BaudRate { get; set; }
        public static Parity ParitySetting { get; set; }
        public static StopBits StopBitsSetting { get; set; }
        public static int ReadTimeoutMS { get; set; }
        public static int WriteTimeoutMS { get; set; }
        public static bool IsRetryEnabled { get; set; }
        public static int MaxRetryCount { get; set; }

        static NexStarSettingsManager()
        {
            BaudRate = 9600;
            ParitySetting = Parity.None;
            StopBitsSetting = StopBits.One;
            ReadTimeoutMS = 4000;
            WriteTimeoutMS = 1000;
            IsRetryEnabled = false;
            MaxRetryCount = 0;
        }
    }
}
