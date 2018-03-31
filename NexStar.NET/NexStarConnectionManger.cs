using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.Net.NexstarSupport.Commands;
using NexStar.Net.SerialMgmt;
using NexStar.Net.Exceptions;
using NexStar.Net.NexstarSupport.BaseCommandClasses;

namespace NexStar.Net
{
    public class NexStarConnectionManager
    {
        public NexStarConnection CreateConnectionForAddress(string serialComAddress)
        {
            return new NexStarConnection(serialComAddress);
        }
        
        public string[] FindActiveSerialNexstarDevices()
        {
            List<string> livePorts = new List<string>();
            string[] portNames = SerialPort.GetPortNames();
            foreach (string portName in portNames)
            {
                SerialPortController testController = new SerialPortController(portName);
                VersionCommand versionCommandCommand = new VersionCommand();
                testController.RunCommand(versionCommandCommand);
                testController.CloseSerialConnection();

                if (versionCommandCommand.RawResultBytes != null)
                {
                    livePorts.Add(portName);
                }
            }

            return livePorts.ToArray();
        }
    }
}
