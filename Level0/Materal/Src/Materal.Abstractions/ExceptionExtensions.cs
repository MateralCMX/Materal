namespace Materal.Abstractions
{
    /// <summary>
    /// 异常扩展
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="getDetailMessage"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this Exception? exception, Func<Exception, string, string>? getDetailMessage = null)
        {
            if (exception is null) return string.Empty;
            string result = exception.GetErrorMessage(null, getDetailMessage);
            return result;
        }
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="prefix"></param>
        /// <param name="getDetailMessage"></param>
        /// <returns></returns>
        private static string GetErrorMessage(this Exception? exception, string? prefix, Func<Exception, string, string>? getDetailMessage = null)
        {
            if (exception is null) return string.Empty;
            prefix ??= string.Empty;
            StringBuilder errorMessage = new();
            string? message = null;
            if (getDetailMessage is not null)
            {
                message = getDetailMessage(exception, prefix);
            }
            else if (exception is MateralException materalException)
            {
                message = materalException.GetDetailMessage(prefix);
            }
            else
            {
                try
                {
                    MethodInfo? methodInfo = exception.GetType().GetMethod("GetDetailMessage", [typeof(string)]);
                    if (methodInfo is not null && methodInfo.ReturnType == typeof(string))
                    {
                        object? detailMessage = methodInfo.Invoke(exception, [prefix]);
                        if (detailMessage is not null && detailMessage is string detailMessageString)
                        {
                            message = detailMessageString;
                        }
                    }
                }
                catch
                {
                }
                message ??= exception.Message;
            }
            errorMessage.AppendLine($"{prefix}--->{exception.GetType().FullName}: {message}");
            if (exception.InnerException is not null)
            {
                errorMessage.Append(exception.InnerException.GetErrorMessage($"\t{prefix}", getDetailMessage));
            }
            if (exception.StackTrace is null) return errorMessage.ToString();
            string[] stackTraces = exception.StackTrace.Split('\n');
            foreach (string stackTrace in stackTraces)
            {
                if (stackTrace.Last() == '\r')
                {
                    errorMessage.AppendLine($"{prefix}{stackTrace[..^1]}");
                }
                else
                {
                    errorMessage.AppendLine($"{prefix}{stackTrace}");
                }
            }
            errorMessage.AppendLine($"{prefix}--- 异常堆栈跟踪结束 ---");
            string result = errorMessage.ToString();
            return result;
        }
    }
}
