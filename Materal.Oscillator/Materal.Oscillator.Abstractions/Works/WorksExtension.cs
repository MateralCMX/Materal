using Materal.ConvertHelper;

namespace Materal.Oscillator.Abstractions.Works
{
    public static class WorksExtension
    {
        /// <summary>
        /// 序列化响应
        /// </summary>
        /// <param name="work">响应</param>
        /// <returns></returns>
        public static string Serialize(this IWork work) => work.ToJson();
    }
}
