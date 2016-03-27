using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.NET.Exceptions;

namespace NexStar.NET
{
    public static class NexStarConnectionManager
    {
        private static Dictionary<Guid, SerialPort> _registeredSerialPorts;

        /// <summary>
        /// Contains tools for managing connections to the nex star device, and providing factory functionality for the API object
        /// </summary>
        static NexStarConnectionManager()
        {
            _registeredSerialPorts = new Dictionary<Guid, SerialPort>();
        }
         
        public static Guid RegisterNexStarDevice(SerialPort nexStarPortToRegister)
        {
            Guid newPortKey = Guid.NewGuid();
            _registeredSerialPorts.Add(newPortKey, nexStarPortToRegister);
            return newPortKey;
        }

        public static void UnregisterNexStarDevice(Guid portKey)
        {
            try
            {
                _registeredSerialPorts.Remove(portKey);
            }
            catch (Exception ex)
            {
                throw new InvalidNexStarPortKeyException("The port key is not current registered.", ex);
            }
        }
    }
}
