using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.NexstarSupport.BaseCommandClasses;
using NexStar.Net.NexstarSupport.Models;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class VersionCommand : NexStarCommand<NexStarVersion>
    {
        public override NexStarVersion TypedResult
        {
            get
            {
                if (base.RawResultBytes == null)
                {
                    return null;
                }

                return new NexStarVersion(base.RawResultBytes);
            }
        }

        public override byte[] RenderCommandBytes()
        {
            return new[] { Convert.ToByte('V') };
        }
    }
}
