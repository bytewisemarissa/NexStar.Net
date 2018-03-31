using System;
using System.Collections.Generic;
using System.Text;
using NexStar.Net.NexstarSupport.Internal;
using NexStar.Net.NexstarSupport.Models;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class GetTimeCommand : NexStarCommand<TimeData>
    {
        public override TimeData TypedResult => new TimeData(RawResultBytes);

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('h') };
        }
    }
}
