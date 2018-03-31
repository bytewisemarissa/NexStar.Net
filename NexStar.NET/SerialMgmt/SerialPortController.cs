using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.Settings;
using NexStar.Net.NexstarSupport.Commands;
using NexStar.Net.NexstarSupport.Internal;

namespace NexStar.Net.SerialMgmt
{
    public class SerialPortController
    {
        private readonly SerialPort _managedPort;
        private readonly object _syncLock;
        private readonly string _stopCharacter;

        public SerialPortController(string portToManage)
        {
            _syncLock = new object();

            _managedPort = BuildNexStarSerialPort();
            _managedPort.PortName = portToManage;
            _stopCharacter = "#";
        }

        public NexStarCommand<T> RunCommand<T>(NexStarCommand<T> command, int retryCount = 0)
        {
            if (command == null)
            {
                throw new NullReferenceException("Command parameter can not be null.");
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
                    command.RawResultBytes = Encoding.UTF8.GetBytes(_managedPort.ReadTo(_stopCharacter));
                    return command;
                }
                catch (TimeoutException)
                {
                    if (NexStarSettingsManager.IsRetryEnabled && retryCount < NexStarSettingsManager.MaxRetryCount)
                    {
                        return RunCommand(command, retryCount + 1);
                    }

                    throw;
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

        private static SerialPort BuildNexStarSerialPort()
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
