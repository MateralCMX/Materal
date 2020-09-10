using Materal.APP.Core;
using System;

namespace Authority.Common
{
    public class AuthorityException : MateralAPPException
    {
        public AuthorityException() { }

        public AuthorityException(string message) : base(message) { }

        public AuthorityException(string message, Exception innerException) : base(message, innerException) { }
    }
}
