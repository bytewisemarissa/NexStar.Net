using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.Enums;
using NexStar.Net.NexstarSupport.Commands;
using NexStar.Net.NexstarSupport.Models;
using NexStar.Net.SerialMgmt;
using NexStar.Net.Exceptions;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net
{
    public class NexStarConnection : IDisposable
    {
        private readonly SerialPortController _serialPort;

        public NexStarConnection(string serialPortAddress)
        {
            _serialPort = new SerialPortController(serialPortAddress);
        }

        #region Miscellaneous Commands API Implementation
        public NexStarVersion GetNexStarVersion()
        {
            return _serialPort.RunCommand(new VersionCommand()).TypedResult;
        }

        public NexStarVersion GetNexStarVersionForDevice(TelescopeDevice device)
        {
            return _serialPort.RunCommand(new DeviceVersionCommand(device)).TypedResult;
        }

        public TelescopeModel GeTelescopeModel()
        {
            return _serialPort.RunCommand(new TelescopeModelCommand()).TypedResult;
        }

        public byte Echo(byte echoValue)
        {
            return _serialPort.RunCommand(new EchoCommand(echoValue)).TypedResult;
        }

        public bool CheckIfAlignmentIsComplete()
        {
            return _serialPort.RunCommand(new CheckAlignmentCompleteCommand()).TypedResult;
        }

        public bool CheckIfGotoIsRunning()
        {
            return _serialPort.RunCommand(new CheckGotoRunningCommand()).TypedResult;
        }

        public void CancelGoto()
        {
            _serialPort.RunCommand(new CancelGotoCommand());
        }
        #endregion

        #region Time and Location API Implementation
        public GpsData GetLocation()
        {
            return _serialPort.RunCommand(new GetLocationCommand()).TypedResult;
        }

        public void SetLocation(GpsData data)
        {
            _serialPort.RunCommand(new SetLocationCommand(data));
        }

        public TimeData GetTime()
        {
            return _serialPort.RunCommand(new GetTimeCommand()).TypedResult;
        }

        public void SetTime(TimeData data)
        {
            _serialPort.RunCommand(new SetTimeCommand(data));
        }
        #endregion

        #region Slewing Commands API Implementation

        public void StopVariableAzmSlew()
        {
            _serialPort.RunCommand(new SetVariableAzmSlewCommand(0, true));
            _serialPort.RunCommand(new SetVariableAzmSlewCommand(0, false));
        }

        public void StartVariableAzmSlew(int arcSecondsPerSecond)
        {
            if (arcSecondsPerSecond == 0)
            {
                StopVariableAzmSlew();
                return;
            }

            _serialPort.RunCommand(new SetVariableAzmSlewCommand(arcSecondsPerSecond));
        }

        public void StopVariableDecSlew()
        {
            _serialPort.RunCommand(new SetVariableDecSlewCommand(0, true));
            _serialPort.RunCommand(new SetVariableDecSlewCommand(0, false));
        }

        public void StartVariableDecSlew(int arcSecondsPerSecond)
        {
            if (arcSecondsPerSecond == 0)
            {
                StopVariableDecSlew();
                return;
            }

            _serialPort.RunCommand(new SetVariableDecSlewCommand(arcSecondsPerSecond));

        }

        public void SetFixedAzmSlew(SlewRate rate, bool slewPositive)
        {
            _serialPort.RunCommand(new SetFixedAzmSlewCommand(rate, slewPositive));
        }

        public void SetFixedDecSlew(SlewRate rate, bool slewPositive)
        {
            _serialPort.RunCommand(new SetFixedDecSlewCommand(rate, slewPositive));
        }
        #endregion

        public void Dispose()
        {
            _serialPort.CloseSerialConnection();
        }
    }
}
