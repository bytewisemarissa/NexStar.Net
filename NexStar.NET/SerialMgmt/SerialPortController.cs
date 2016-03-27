using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.NET.SerialMgmt
{
    class SerialPortController
    {
        SerialPort _managedPort;
        public SerialPortController(SerialPort portToManage)
        {
            _managedPort = portToManage;
        }


    }
}
