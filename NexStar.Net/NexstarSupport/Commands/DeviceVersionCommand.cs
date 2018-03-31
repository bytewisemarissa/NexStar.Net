using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.Enums;
using NexStar.Net.NexstarSupport.BaseCommandClasses;
using NexStar.Net.NexstarSupport.Models;

namespace NexStar.Net.NexstarSupport.Commands
{
    class DeviceVersionCommand : NexStarCommand<NexStarVersion>
    {
        private readonly TelescopeDevice _targetDevice;

        public DeviceVersionCommand(TelescopeDevice targetDevice)
        {
            _targetDevice = targetDevice;
        }

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
            return new byte[] { Convert.ToByte('P'), 0x01, _targetDevice.DeviceId, 0xFE, 0x0, 0x0, 0x0, 0x2 };
        }
    }
}
