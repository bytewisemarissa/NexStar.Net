using System;

namespace NexStar.Net.NexstarSupport.BaseCommandClasses
{
    public abstract class NexStarCommand<T>
    {
        public abstract T TypedResult { get; }
        
        public abstract byte[] RenderCommandBytes();

        public byte[] RawResultBytes { get; set; }

        public Exception CommandException { get; set; }
        
    }
}
