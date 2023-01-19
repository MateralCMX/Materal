using System;

namespace MateralBaseCoreVSIX
{
    [Serializable]
    public class VSIXException : Exception
    {
        public VSIXException() { }
        public VSIXException(string message) : base(message) { }
        public VSIXException(string message, Exception inner) : base(message, inner) { }
        protected VSIXException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
