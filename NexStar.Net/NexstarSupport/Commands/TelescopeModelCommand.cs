using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.Enums;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class TelescopeModelCommand : NexStarCommand<TelescopeModel>
    {
        public override TelescopeModel TypedResult => TelescopeModel.GetTelescopeModelById(RawResultBytes[0]);

        public override byte[] RenderCommandBytes()
        {
            return new byte[] { Convert.ToByte('m') };
        }
    }
}
