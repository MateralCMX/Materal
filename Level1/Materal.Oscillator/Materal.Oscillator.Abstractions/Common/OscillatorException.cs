using Materal.Common;

namespace Materal.Oscillator.Abstractions.Common
{
    public class OscillatorException : MateralException
    {
        public OscillatorException()
        {
        }

        public OscillatorException(string message) : base(message)
        {
        }

        public OscillatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}