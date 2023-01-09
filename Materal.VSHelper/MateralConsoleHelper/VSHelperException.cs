using System;

namespace MateralVSHelper
{
    [Serializable]
    public class VSHelperException : Exception
    {
        public VSHelperException() { }
        public VSHelperException(string message) : base(message) { }
        public VSHelperException(string message, Exception inner) : base(message, inner) { }
        protected VSHelperException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
