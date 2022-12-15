using Materal.Common.Models;
using System.Threading.Tasks.Dataflow;

namespace Materal.Common
{
    public class ConsoleQueue
    {
        private readonly static ActionBlock<ConsoleMessageModel> writeBuffer;
        static ConsoleQueue()
        {
            writeBuffer = new(WriteMessage);
        }
        public static void WriteLine(ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(string.Empty, consoleColor);
        public static void WriteLine(bool value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(char value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(char[]? buffer, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            if (buffer == null || buffer.Length == 0)
            {
                WriteLine(consoleColor);
            }
            else
            {
                WriteLine(buffer, 0, buffer.Length, consoleColor);
            }
        }
        public static void WriteLine(char[] buffer, int index, int count, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            char[] value = new char[count];
            for (int i = 0; i < count || index + 1 < buffer.Length; i++)
            {
                value[i] = buffer[index + i];
            }
            WriteLine(value.ToString(), consoleColor);
        }
        public static void WriteLine(decimal value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(double value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(float value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(int value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(uint value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(long value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(ulong value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        public static void WriteLine(object? value, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            if (value == null)
            {
                WriteLine(consoleColor);
            }
            else
            {
                WriteLine(value.ToString(), consoleColor);
            }
        }
        public static void WriteLine(Exception? exception, ConsoleColor consoleColor = ConsoleColor.DarkRed)
        {
            if (exception == null)
            {
                WriteLine(consoleColor);
            }
            else
            {
                WriteLine(exception.GetErrorMessage(), consoleColor);
            }
        }
        public static void WriteLine(string? value, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            writeBuffer.Post(new()
            {
                Color = consoleColor,
                Message = value ?? string.Empty
            });
        }
        public static void WriteLine(string format, ConsoleColor consoleColor = ConsoleColor.Gray, params object?[]? args)
        {
            if (args == null || args.Length == 0)
            {
                WriteLine(format, consoleColor);
            }
            else
            {
                writeBuffer.Post(new()
                {
                    Color = consoleColor,
                    Message = format,
                    Args = args
                });
            }
        }
        public static void WriteLine(string format, params object?[]? args) => WriteLine(format, ConsoleColor.Gray, args);
        private static void WriteMessage(ConsoleMessageModel model)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = model.Color;
            if (model.Args != null)
            {
                Console.WriteLine(model.Message, model.Args);
            }
            else
            {
                Console.WriteLine(model.Message);
            }
            Console.ForegroundColor = oldColor;
        }
    }
}
