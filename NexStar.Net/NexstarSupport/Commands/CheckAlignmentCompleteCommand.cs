using System;
using System.Collections.Generic;
using System.Text;
using NexStar.Net.Enums;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class CheckAlignmentCompleteCommand : NexStarCommand<bool>
    {
        public override bool TypedResult => Convert.ToBoolean(RawResultBytes[0]);

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('J') };
        }
    }
}
