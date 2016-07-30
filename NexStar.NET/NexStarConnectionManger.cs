using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.NET.Exceptions;
using NexStar.NET.NexstarSupport.BaseCommandClasses;
using NexStar.NET.NexstarSupport.Commands;
using NexStar.NET.SerialMgmt;

namespace NexStar.NET
{
    public static class NexStarConnectionManager
    {
        public static Dictionary<Guid, SerialPortController> RegisteredSerialPorts { get; }

        /// <summary>
        /// Contains tools for managing connections to the nex star device, and providing factory functionality for the API object
        /// </summary>
        static NexStarConnectionManager()
        {
            RegisteredSerialPorts = new Dictionary<Guid, SerialPortController>();
        }
         
        public static Guid RegisterNexStarDevice(string nexStarPortToRegister)
        {
            Guid newPortKey = Guid.NewGuid();
            RegisteredSerialPorts.Add(newPortKey, new SerialPortController(nexStarPortToRegister));
            return newPortKey;
        }

        public static void UnregisterNexStarDevice(Guid portKey)
        {
            try
            {
                if (RegisteredSerialPorts[portKey] != null)
                {
                    RegisteredSerialPorts[portKey].CloseSerialConnection();
                }

                RegisteredSerialPorts.Remove(portKey);
            }
            catch (Exception ex)
            {
                throw new InvalidNexStarPortKeyException("The port key is not current registered.", ex);
            }
        }

        public static NexStarCommand RunNexStarCommand(Guid portKey, NexStarCommand command)
        {
            return RegisteredSerialPorts[portKey].RunCommand(command);
        }

        public static void CloseDeviceConnection(Guid portKey)
        {
            try
            {
                if (RegisteredSerialPorts[portKey] != null)
                {
                    RegisteredSerialPorts[portKey].CloseSerialConnection();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidNexStarPortKeyException("The port key is not current registered.", ex);
            }
        }

        public static string[] FindActiveSerialNexstarDevices()
        {
            List<string> livePorts = new List<string>();
            string[] portNames = SerialPort.GetPortNames();
            foreach (string portName in portNames)
            {
                SerialPortController testController = new SerialPortController(portName);
                GetVersion versionCommand = new GetVersion();
                testController.RunCommand(versionCommand);
                testController.CloseSerialConnection();

                if (versionCommand.RawResultBytes != null)
                {
                    livePorts.Add(portName);
                }
            }

            return livePorts.ToArray();
        }
    }
}
