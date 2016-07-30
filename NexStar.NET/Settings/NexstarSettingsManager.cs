using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.NET.Settings
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

        static void LoadFromAppSettings()
        {
            BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["NexStar-BaudRate"]);
            ReadTimeoutMS = Convert.ToInt32(ConfigurationManager.AppSettings["NexStar-ReadTimeoutMS"]);
            WriteTimeoutMS = Convert.ToInt32(ConfigurationManager.AppSettings["NexStar-WriteTimeoutMS"]);
            IsRetryEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["NexStar-IsRetryEnabled"]);
            MaxRetryCount = Convert.ToInt32(ConfigurationManager.AppSettings["NexStar-MaxRetryCount"]);

            switch (ConfigurationManager.AppSettings["NexStar-Parity"].ToLower())
            {
                case "even":
                    ParitySetting = Parity.Even;
                    break;
                case "mark":
                    ParitySetting = Parity.Mark;
                    break;
                case "none":
                    ParitySetting = Parity.None;
                    break;
                case "Odd":
                    ParitySetting = Parity.Odd;
                    break;
                case "Space":
                    ParitySetting = Parity.Space;
                    break;
                default:
                    ParitySetting = Parity.None;
                    break;
            }

            switch (ConfigurationManager.AppSettings["NexStar-StopBits"].ToLower())
            {
                case "none":
                    StopBitsSetting = StopBits.None;
                    break;
                case "one":
                    StopBitsSetting = StopBits.One;
                    break;
                case "onepointfive":
                    StopBitsSetting = StopBits.OnePointFive;
                    break;
                case "two":
                    StopBitsSetting = StopBits.Two;
                    break;
                default:
                    StopBitsSetting = StopBits.None;
                    break;
            }
        }
    }
}
