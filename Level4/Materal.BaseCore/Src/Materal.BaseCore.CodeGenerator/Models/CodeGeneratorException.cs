namespace Materal.BaseCore.CodeGenerator.Models
{
    [Serializable]
    public class CodeGeneratorException : Exception
    {
        public CodeGeneratorException() { }
        public CodeGeneratorException(string message) : base(message) { }
        public CodeGeneratorException(string message, Exception inner) : base(message, inner) { }
        protected CodeGeneratorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
