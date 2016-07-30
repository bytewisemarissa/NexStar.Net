using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.NET.NexstarSupport.Commands;
using NexStar.NET.NexstarSupport.BaseCommandClasses;
using NexStar.NET.Settings;

namespace NexStar.NET.SerialMgmt
{
    public class SerialPortController
    {
        private SerialPort _managedPort;
        private readonly object _syncLock;
        private static readonly string StopCharacter = "#";
        public SerialPortController(string portToManage)
        {
            _syncLock = new object();

            _managedPort = BuildNexStarSerialPort();
            _managedPort.PortName = portToManage;
        }

        public NexStarCommand RunCommand(NexStarCommand command, int retryCount = 0)
        {
            if (command == null)
            {
                command.RawResultBytes = null;
                return command;
            }

            byte[] commandBytes = command.RenderCommandBytes();

            lock (_syncLock)
            {
                try
                {

                    if (!_managedPort.IsOpen)
                    {
                        _managedPort.Open();
                    }

                    _managedPort.Write(commandBytes, 0, commandBytes.Count());
                    command.RawResultBytes = Encoding.UTF8.GetBytes(_managedPort.ReadTo(StopCharacter));
                    return command;
                }
                catch (TimeoutException ex)
                {
                    if (NexStarSettingsManager.IsRetryEnabled && retryCount < NexStarSettingsManager.MaxRetryCount)
                    {
                        return RunCommand(command, retryCount + 1);
                    }

                    command.CommandException = ex;
                    return command;
                }
                catch (UnauthorizedAccessException ex)
                {
                    command.CommandException = ex;
                    return command;
                }
            }
        }

        public void CloseSerialConnection()
        {
            lock (_syncLock)
            {
                if (_managedPort.IsOpen)
                {
                    _managedPort.Close();
                }
            }
        }

        public static SerialPort BuildNexStarSerialPort()
        {
            SerialPort returnValue = new SerialPort()
            {
                BaudRate = NexStarSettingsManager.BaudRate,
                ReadTimeout = NexStarSettingsManager.ReadTimeoutMS,
                WriteTimeout = NexStarSettingsManager.WriteTimeoutMS,
                StopBits = NexStarSettingsManager.StopBitsSetting,
                Parity = NexStarSettingsManager.ParitySetting
            };

            return returnValue;
        }
    }
}
