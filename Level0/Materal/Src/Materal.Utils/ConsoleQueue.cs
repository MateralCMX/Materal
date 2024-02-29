using Materal.Utils.Model;
using System.Threading.Tasks.Dataflow;

namespace Materal.Utils
{
    /// <summary>
    /// 控制台队列
    /// </summary>
    public class ConsoleQueue
    {
        private static ActionBlock<ConsoleMessageModel>? _writeBuffer;
        static ConsoleQueue() => Start();
        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            if (_writeBuffer is not null) return;
            _writeBuffer = new(WriteMessage);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public static async Task ShutdownAsync()
        {
            if (_writeBuffer is null) return;
            _writeBuffer.Complete();
            await _writeBuffer.Completion;
            _writeBuffer = null;
        }
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="consoleColor"></param>
        public static void WriteLine(ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(string.Empty, consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(bool value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(char value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(char[]? buffer, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            if (buffer is null || buffer.Length == 0)
            {
                WriteLine(consoleColor);
            }
            else
            {
                WriteLine(buffer, 0, buffer.Length, consoleColor);
            }
        }
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(char[] buffer, int index, int count, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            char[] value = new char[count];
            for (int i = 0; i < count || index + 1 < buffer.Length; i++)
            {
                value[i] = buffer[index + i];
            }
            WriteLine(value.ToString(), consoleColor);
        }
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(decimal value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(double value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(float value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(int value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(uint value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(long value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(ulong value, ConsoleColor consoleColor = ConsoleColor.Gray) => WriteLine(value.ToString(), consoleColor);
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(object? value, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            if (value is null)
            {
                WriteLine(consoleColor);
            }
            else
            {
                WriteLine(value.ToString(), consoleColor);
            }
        }
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(Exception? exception, ConsoleColor consoleColor = ConsoleColor.DarkRed)
        {
            if (exception is null)
            {
                WriteLine(consoleColor);
            }
            else
            {
                WriteLine(exception.GetErrorMessage(), consoleColor);
            }
        }
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(string? value, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            _writeBuffer?.Post(new()
            {
                Color = consoleColor,
                Message = value ?? string.Empty
            });
        }
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="format"></param>
        /// <param name="consoleColor"></param>
        /// <param name="args"></param>
        public static void WriteLine(string format, ConsoleColor consoleColor = ConsoleColor.Gray, params object?[]? args)
        {
            if (args is null || args.Length == 0)
            {
                WriteLine(format, consoleColor);
            }
            else
            {
                _writeBuffer?.Post(new()
                {
                    Color = consoleColor,
                    Message = format,
                    Args = args
                });
            }
        }
        /// <summary>
        /// 写一行消息
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteLine(string format, params object?[]? args) => WriteLine(format, ConsoleColor.Gray, args);
        /// <summary>
        /// 写消息
        /// </summary>
        /// <param name="model"></param>
        private static void WriteMessage(ConsoleMessageModel model)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = model.Color;
            if (model.Args is not null)
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
