using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStarEmulator.Processors
{
    public interface IProcessor
    {
        byte[] ProcessRequestBytes(byte[] incomingBytes);
        bool IsSimulationHung();
        bool IsHangPreReturn();
        bool IsPaused();
    }
}
