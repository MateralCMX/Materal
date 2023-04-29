namespace Materal.Oscillator.Abstractions.Answers
{
    /// <summary>
    /// 响应扩展
    /// </summary>
    public static class AnswersExtension
    {
        /// <summary>
        /// 序列化响应
        /// </summary>
        /// <param name="answer">响应</param>
        /// <returns></returns>
        public static string Serialize(this IAnswer answer) => answer.ToJson();
    }
}
