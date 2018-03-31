using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net.NexstarSupport.Commands
{
    public class SetVariableAzmSlewCommand : NexStarCommand<NoReturn>
    {
        private static readonly byte VariableSlewPositive = 0x06;
        private static readonly byte VariableSlewNegative = 0x07;

        private readonly byte _lowOrderByte;
        private readonly byte _highOrderByte;
        private readonly bool _direction;

        public SetVariableAzmSlewCommand(int arcSecPerSec, bool? direction = null)
        {
            if (direction.HasValue)
            {
                _direction = direction.Value;
            }
            else
            {
                _direction = arcSecPerSec > 0;
            }
            
            _lowOrderByte = (byte)((arcSecPerSec * 4) / 256);
            _highOrderByte = (byte)((arcSecPerSec * 4) % 256);
        }

        public override NoReturn TypedResult => throw new NotImplementedException("This call does not return a value.");

        public override byte[] RenderCommandBytes()
        {
            return new byte[]
            {
                Convert.ToByte('P'),
                0x03,
                0x10,
                _direction ? VariableSlewPositive : VariableSlewNegative,
                _highOrderByte,
                _lowOrderByte,
                0x0,
                0x0
            };
        }
    }
}
