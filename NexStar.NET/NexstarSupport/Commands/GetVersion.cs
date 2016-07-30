using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.NET.NexstarSupport.BaseCommandClasses;
using NexStar.NET.NexstarSupport.Models;

namespace NexStar.NET.NexstarSupport.Commands
{
    public class GetVersion : NexStarCommand, ICommandResult<NexStarVersion>
    {
        private static readonly byte[] GetVersionCommandBytes = new byte[] { Convert.ToByte('V') };

        public NexStarVersion TypedResult
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
            return GetVersionCommandBytes;
        }
    }
}
