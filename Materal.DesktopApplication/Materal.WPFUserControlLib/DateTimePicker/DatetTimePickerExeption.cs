using Materal.WPFCommon;
using System;
using System.Runtime.Serialization;

namespace Materal.WPFUserControlLib.DateTimePicker
{
    public class DatetTimePickerExeption : MateralWPFException
    {
        public DatetTimePickerExeption()
        {
        }

        public DatetTimePickerExeption(string message) : base(message)
        {
        }

        public DatetTimePickerExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatetTimePickerExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
