using System;
using System.Collections.Generic;
using System.Text;
using NexStar.Net.NexstarSupport.Internal;
using NexStar.Net.NexstarSupport.Models;

namespace NexStar.Net.NexstarSupport.Commands
{
    class GetLocationCommand : NexStarCommand<GpsData>
    {
        public override GpsData TypedResult => new GpsData(RawResultBytes);

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('w') };
        }
    }
}
