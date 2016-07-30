using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexStar.NET.Exceptions;

namespace NexStar.NET.NexstarSupport.Models
{
    public class NexStarVersion
    {
        public readonly short MajorRevision;
        public readonly short MinorRevision;

        public NexStarVersion()
        {
            MajorRevision = 0;
            MinorRevision = 0;
        }

        public NexStarVersion(byte[] versionMessageBytes)
        {
            if (versionMessageBytes.Length < 2)
            {
                throw new NexStarMessageIncorrectException("Version message bytes too short");
            }

            if (versionMessageBytes.Length > 2)
            {
                throw new NexStarMessageIncorrectException("Version message bytes too long");
            }

            MajorRevision = Convert.ToInt16(versionMessageBytes[0]);
            MinorRevision = Convert.ToInt16(versionMessageBytes[1]);
        }

        public override string ToString()
        {
            return $"v{MajorRevision}.{MinorRevision}";
        }
    }
}
