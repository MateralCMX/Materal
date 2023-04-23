namespace Materal.Oscillator.Abstractions.Answers
{
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
