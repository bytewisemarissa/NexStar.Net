using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStarEmulator.Processors
{
    public class RequestProcessor_114LCM : IProcessor
    {
        public bool _isSimulationHung = false;
        public bool _isHangPreReturn = false;
        public bool _isPaused = false;

        public byte[] ProcessRequestBytes(byte[] incomingBytes)
        {
            byte[] returnBytes = ResolveCommand(incomingBytes);

            if (returnBytes == null)
            {
                return null;
            }

            byte[] terminatedBytes = new byte[returnBytes.Length + 1];
            System.Buffer.BlockCopy(returnBytes, 0, terminatedBytes, 0, returnBytes.Length);
            terminatedBytes[terminatedBytes.Length - 1] = 0x23;
            return terminatedBytes;
        }

        public bool IsSimulationHung()
        {
            return _isSimulationHung;
        }

        public bool IsHangPreReturn()
        {
            return _isHangPreReturn;
        }

        public bool IsPaused()
        {
            return _isPaused;
        }

        private byte[] ResolveCommand(byte[] incomingBytes)
        {
            if (incomingBytes == null || incomingBytes.Length == 0)
            {
                return new byte[0];
            }

            for (int i = 0; i < incomingBytes.Length; i++)
            {
                switch (incomingBytes[i])
                {
                    case 0x56: // 'V' Version Command
                        return VersionCommand(incomingBytes);
                }
            }

            _isPaused = true;
            return null;
        }

        private byte[] VersionCommand(byte[] incomingBytes)
        {
            return new byte[] {0x05, 0x16};
        }
    }
}
