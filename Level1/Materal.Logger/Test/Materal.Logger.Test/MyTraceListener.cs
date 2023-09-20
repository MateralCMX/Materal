using System.Diagnostics;

namespace Materal.Logger.Test
{
    public class MyTraceListener : TraceListener
    {
        public override void Write(string? message) => Console.WriteLine($"MyTraceListener->{message}");
        public override void WriteLine(string? message)=> Write($"{message}\r\n");
    }
}
