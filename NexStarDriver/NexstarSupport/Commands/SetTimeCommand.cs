using System;
using System.Collections.Generic;
using System.Text;
using NexStar.Net.NexstarSupport.Internal;
using NexStar.Net.NexstarSupport.Models;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class SetTimeCommand : NexStarCommand<NoReturn>
    {
        private readonly TimeData _data;

        public SetTimeCommand(TimeData data)
        {
            _data = data;
        }

        public override NoReturn TypedResult => throw new NotImplementedException("This call does not return a value.");

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('H'),
                Convert.ToByte(_data.Hour),
                Convert.ToByte(_data.Minutes),
                Convert.ToByte(_data.Seconds),
                Convert.ToByte(_data.Month),
                Convert.ToByte(_data.Day),
                Convert.ToByte(_data.YearsSince2000),
                Convert.ToByte(_data.GMTOffset),
                Convert.ToByte(_data.IsDaylightSavings) };
        }
    }
}
