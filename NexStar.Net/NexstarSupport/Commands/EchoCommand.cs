using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.Enums;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net.NexstarSupport.Commands
{
    class EchoCommand : NexStarCommand<byte>
    {
        private readonly byte _echoValue;

        public override byte TypedResult => RawResultBytes[0];

        public EchoCommand(byte echoValue)
        {
            _echoValue = echoValue;
        }

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('K'), _echoValue };
        }
    }
}
