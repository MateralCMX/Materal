using Materal.WPFCommon;
using System;
using System.Runtime.Serialization;

namespace Materal.WPFUserControlLib.NumberBox
{
    public class NumberBoxException : MateralWPFException
    {
        public NumberBoxException()
        {
        }

        public NumberBoxException(string message) : base(message)
        {
        }

        public NumberBoxException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NumberBoxException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
