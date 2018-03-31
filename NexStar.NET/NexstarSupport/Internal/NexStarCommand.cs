using System;

namespace NexStar.Net.NexstarSupport.Internal
{
    public abstract class NexStarCommand<T>
    {
        public abstract T TypedResult { get; }
        
        public abstract byte[] RenderCommandBytes();

        private byte[] _rawResultBytes;
        public byte[] RawResultBytes
        {
            get
            {
                if (_rawResultBytes == null)
                {
                    throw new NullReferenceException("RawResultBytes is null when it was expected to be populated.");
                }

                return _rawResultBytes;
            }

            set => _rawResultBytes = value;
        }
    }
}
