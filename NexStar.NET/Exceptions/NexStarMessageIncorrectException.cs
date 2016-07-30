using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.NET.Exceptions
{
    public class NexStarMessageIncorrectException : Exception
    {
        public NexStarMessageIncorrectException(string message) : base(message) { }
        public NexStarMessageIncorrectException(string message, Exception innerException) : base(message, innerException) { }
    }
}
