using Materal.BaseCore.CodeGenerator.Models;
using System;

namespace MateralBaseCoreVSIX
{
    [Serializable]
    public class VSIXException : CodeGeneratorException
    {
        public VSIXException() { }
        public VSIXException(string message) : base(message) { }
        public VSIXException(string message, Exception inner) : base(message, inner) { }
        protected VSIXException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
