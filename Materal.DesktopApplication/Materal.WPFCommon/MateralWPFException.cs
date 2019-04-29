using System;
using System.Runtime.Serialization;
using Materal.Common;

namespace Materal.WPFCommon
{
    /// <summary>
    /// MateralWPF异常类
    /// </summary>
    public class MateralWPFException : MateralException
    {
        public MateralWPFException()
        {
        }

        public MateralWPFException(string message) : base(message)
        {
        }

        public MateralWPFException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
