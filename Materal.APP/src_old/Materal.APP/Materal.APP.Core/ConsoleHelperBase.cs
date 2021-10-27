using AspectCore.DynamicProxy;
using Materal.NetworkHelper;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.APP.Core
{
    public static class ConsoleHelperBase
    {
        private static readonly ConcurrentQueue<ConsoleMessageModel> _consoleMessages = new ConcurrentQueue<ConsoleMessageModel>();
        private static bool _isRuning;
        /// <summary>
        /// 开始写
        /// </summary>
        /// <returns></returns>
        public static async void StartWrite()
        {
            _isRuning = true;
            while (_isRuning)
            {
                if (_consoleMessages.Count == 0)
                {
                    await Task.Delay(1000);
                    continue;
                }
                if (!_consoleMessages.TryDequeue(out ConsoleMessageModel model)) continue;
                Console.ForegroundColor = model.ConsoleColor;
                if (model.NewLine)
                {
                    Console.WriteLine(model.Message);
                }
                else
                {
                    Console.Write(model.Message);
                }
            }
        }
        /// <summary>
        /// 停止写
        /// </summary>
        public static void StopWrite()
        {
            _isRuning = false;
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void Write(string title, string message, string subTitle, ConsoleColor consoleColor)
        {
            var dateNow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var model = new ConsoleMessageModel
            {
                ConsoleColor = consoleColor,
                Message = string.IsNullOrEmpty(subTitle)
                    ? $"[{dateNow}]{title}：{message}"
                    : $"[{dateNow}]{title}[{subTitle}]：{message}",
                NewLine = false
            };
            _consoleMessages.Enqueue(model);
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void WriteLine(string title, string message, string subTitle, ConsoleColor consoleColor)
        {
            var dateNow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var model = new ConsoleMessageModel
            {
                ConsoleColor = consoleColor,
                Message = string.IsNullOrEmpty(subTitle)
                    ? $"[{dateNow}]{title}：{message}"
                    : $"[{dateNow}]{title}[{subTitle}]：{message}",
                NewLine = true
            };
            _consoleMessages.Enqueue(model);
        }
        /// <summary>
        /// 获得消息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetMessage(Exception exception)
        {
            string message = $"{exception.Message}\r\n{exception.StackTrace}\r\n";
            switch (exception)
            {
                case AggregateException aggregateException:
                {
                    message = aggregateException.InnerExceptions.Aggregate(message, (current, ex) => current + GetMessage(ex));
                    break;
                }
                case MateralHttpException httpException:
                    message = httpException.GetMessage();
                    break;
                case MateralAPPException materalAPPException:
                    message = materalAPPException.Message;
                    break;
                case AspectInvocationException aspectInvocationException:
                    message = aspectInvocationException.InnerException is MateralAPPException ?
                        aspectInvocationException.InnerException?.Message :
                        GetMessage(aspectInvocationException.InnerException);
                    break;
                default:
                {
                    Exception tempException = exception;
                    do
                    {
                        message += $"{tempException.Message}\r\n{tempException.StackTrace}\r\n";
                    } while (tempException.InnerException != null);
                    break;
                }
            }
            return message;
        }
    }
}
