using System.Text;

namespace Materal.MergeBlock.GeneratorCode
{
    /// <summary>
    /// MergeBlock生成代码插件
    /// </summary>
    public interface IMergeBlockEditGeneratorCodePlug
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task ExcuteAsync(GeneratorCodeContext context, StringBuilder content);
    }
}
