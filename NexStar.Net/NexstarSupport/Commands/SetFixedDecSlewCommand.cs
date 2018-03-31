using System;
using System.Collections.Generic;
using System.Text;
using NexStar.Net.Enums;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class SetFixedDecSlewCommand : NexStarCommand<NoReturn>
    {
        private static readonly byte FixedSlewPositive = 0x24;
        private static readonly byte FixedSlewNegative = 0x25;

        private readonly byte _signValue;
        private readonly SlewRate _slewRate;

        public SetFixedDecSlewCommand(SlewRate rate, bool slewPositive)
        {
            _slewRate = rate;
            _signValue = slewPositive ? FixedSlewPositive : FixedSlewNegative;
        }

        public override NoReturn TypedResult => throw new NotImplementedException("This call does not return a value.");

        public override byte[] RenderCommandBytes()
        {
            return new byte[]
            {
                Convert.ToByte('P'),
                0x02,
                0x11,
                _signValue,
                _slewRate.RateValue,
                0x0,
                0x0,
                0x0
            };
        }
    }
}
