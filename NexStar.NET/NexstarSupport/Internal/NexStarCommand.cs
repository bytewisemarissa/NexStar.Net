using System;

namespace NexStar.NET.NexstarSupport.BaseCommandClasses
{
    public abstract class NexStarCommand
    {
        public byte[] RawResultBytes;

        public Exception CommandException;
        
        public abstract byte[] RenderCommandBytes();
    }
}
