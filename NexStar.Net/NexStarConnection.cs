using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.Enums;
using NexStar.Net.NexstarSupport.BaseCommandClasses;
using NexStar.Net.NexstarSupport.Commands;
using NexStar.Net.NexstarSupport.Models;
using NexStar.Net.SerialMgmt;
using NexStar.Net.Exceptions;

namespace NexStar.Net
{
    public class NexStarConnection : IDisposable
    {
        public delegate void HandleErrorDelegate(Exception ex);
        public event HandleErrorDelegate OnError;

        private readonly SerialPortController _serialPort;

        public NexStarConnection(string serialPortAddress)
        {
            _serialPort = new SerialPortController(serialPortAddress);
        }
        
        public NexStarVersion GetNexStarVersion()
        {
            var result = _serialPort.RunCommand(new VersionCommand());

            return HandleResult(result);
        }

        public NexStarVersion GetNexStarVersionForDevice(TelescopeDevice device)
        {
            var result = _serialPort.RunCommand(new DeviceVersionCommand(device));

            return HandleResult(result);
        }

        public TelescopeModel GeTelescopeModel()
        {
            var result = _serialPort.RunCommand(new TelescopeModelCommand());

            return HandleResult(result);
        }

        public byte Echo(byte echoValue)
        {
            var result = _serialPort.RunCommand(new EchoCommand(echoValue));

            return HandleResult(result);
        }

        public void Dispose()
        {
            _serialPort.CloseSerialConnection();
        }

        private T HandleResult<T>(NexStarCommand<T> result)
        {
            if (result.CommandException != null)
            {
                OnError?.Invoke(result.CommandException);
                return default(T);
            }

            return result.TypedResult;
        }
    }
}
