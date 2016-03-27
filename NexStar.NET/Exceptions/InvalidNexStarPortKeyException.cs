using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.NET.Exceptions
{
    public class InvalidNexStarPortKeyException : Exception
    {
        public InvalidNexStarPortKeyException(string message) : base(message) { }

        public InvalidNexStarPortKeyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
