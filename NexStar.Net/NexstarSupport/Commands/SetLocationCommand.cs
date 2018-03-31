using System;
using System.Collections.Generic;
using System.Text;
using NexStar.Net.NexstarSupport.Internal;
using NexStar.Net.NexstarSupport.Models;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class SetLocationCommand : NexStarCommand<NoReturn>
    {
        private readonly GpsData _data;

        public SetLocationCommand(GpsData data)
        {
            _data = data;
        }

        public override NoReturn TypedResult => throw new NotImplementedException("This call does not return a value.");

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('W'),
                Convert.ToByte(_data.DegreesLatitude),
                Convert.ToByte(_data.MinutesLatitude),
                Convert.ToByte(_data.SecondsLatitude),
                Convert.ToByte(!_data.IsNorth),
                Convert.ToByte(_data.DegreesLongitude),
                Convert.ToByte(_data.MinutesLongitude),
                Convert.ToByte(_data.SecondsLongitude),
                Convert.ToByte(_data.IsWest) };
        }
    }
}
