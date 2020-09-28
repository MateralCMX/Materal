using Materal.Common;
using System;

namespace Materal.BlockchainHelper
{
    public class MateralBlockchainException : MateralException
    {
        public MateralBlockchainException()
        {
        }

        public MateralBlockchainException(string message) : base(message)
        {
        }

        public MateralBlockchainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
