using System;
using System.Collections.Generic;
using System.Text;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class CancelGotoCommand : NexStarCommand<NoReturn>
    {
        public override NoReturn TypedResult => throw new NotImplementedException("This call does not return a value.");

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('M') };
        }
    }
}
