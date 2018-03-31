using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStarEmulator.Processors
{
    public class RequestProcessor_114LCM : IProcessor
    {
        private bool _isSimulationHung = false;
        private bool _isHangPreReturn = false;
        private bool _isPaused = false;

        private bool _isAligning = false;
        private bool _isGoto = false;

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

        public void SetAligning(bool value)
        {
            _isAligning = value;
        }

        public void SetGoto(bool value)
        {
            _isGoto = value;
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
                    case 0x56: 
                        return VersionCommand(incomingBytes);
                    case 0x4B: 
                        return EchoCommand(incomingBytes);
                    case 0x6D: 
                        return ModelCommand(incomingBytes);
                    case 0x4A:
                        return CheckAligmentCommand(incomingBytes);
                    case 0x4C:
                        return CheckGotoRunningCommand(incomingBytes);
                    case 0x4D:
                        return CancelGotoCommand(incomingBytes);
                }
            }

            _isPaused = true;
            return null;
        }

        private byte[] CancelGotoCommand(byte[] incomingBytes)
        {
            if (_isGoto)
            {
                _isGoto = false;
            }

            return new byte[0];
        }

        private byte[] CheckGotoRunningCommand(byte[] incomingBytes)
        {
            if (_isGoto)
            {
                return new byte[] { 0x01 };
            }
            else
            {
                return new byte[] { 0x00 };
            }
        }

        private byte[] CheckAligmentCommand(byte[] incomingBytes)
        {
            if (_isAligning)
            {
                return new byte[] { 0x01 };
            }
            else
            {
                return new byte[] { 0x00 };
            }
        }

        private byte[] ModelCommand(byte[] incomingBytes)
        {
            return new byte[] {0x0F};
        }

        private byte[] EchoCommand(byte[] incomingBytes)
        {
            if (incomingBytes.Length == 1)
            {
                _isPaused = true;
                return null;
            }

            if (incomingBytes.Length > 2)
            {
                _isSimulationHung = true;
                _isHangPreReturn = false;
            }

            return new byte[] { incomingBytes[1] };
        }

        private byte[] VersionCommand(byte[] incomingBytes)
        {
            return new byte[] {0x05, 0x16};
        }
    }
}
