namespace Materal.MergeBlock.GeneratorCode
{
    /// <summary>
    /// MergeBlock生成代码插件
    /// </summary>
    public interface IMergeBlockGeneratorCodePlug
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ExcuteAsync(GeneratorCodeContext context);
    }
}
