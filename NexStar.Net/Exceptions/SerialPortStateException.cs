using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.Net.Exceptions
{
    public class SerialPortStateException : Exception
    {
        public SerialPortStateException(string message) : base(message) { }
        public SerialPortStateException(string message, Exception ex) : base(message, ex) { }
    }
}
