using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.NexstarSupport.Commands;
using NexStar.Net.SerialMgmt;
using NexStar.Net.Exceptions;

namespace NexStar.Net
{
    public class NexStarConnectionManager
    {
        public readonly List<string> ActiveNexStarDevicePortNames;
        public readonly Dictionary<Guid, NexStarConnection> Connections;

        public NexStarConnectionManager()
        {
            ActiveNexStarDevicePortNames = new List<string>();
            Connections = new Dictionary<Guid, NexStarConnection>();
        }

        public Guid CreateConnectionForAddress(string serialComAddress)
        {
            if (!ActiveNexStarDevicePortNames.Contains(serialComAddress))
            {
                throw new ArgumentException("The port address identified is not a nexstar device. Ensure that you have run RefreshActiveSerialNexstarDevices() before starting a connection.");
            }

            var identifier = Guid.NewGuid();
            Connections.Add(identifier, new NexStarConnection(serialComAddress));
            return identifier;
        }
        
        public void RefreshActiveSerialNexstarDevices()
        {
            ActiveNexStarDevicePortNames.Clear();

            string[] portNames = SerialPort.GetPortNames();
            foreach (string portName in portNames)
            {
                SerialPortController testController = new SerialPortController(portName);
                VersionCommand versionCommandCommand = new VersionCommand();
                testController.RunCommand(versionCommandCommand);
                testController.CloseSerialConnection();

                if (versionCommandCommand.RawResultBytes != null)
                {
                    ActiveNexStarDevicePortNames.Add(portName);
                }
            }
        }
    }
}
