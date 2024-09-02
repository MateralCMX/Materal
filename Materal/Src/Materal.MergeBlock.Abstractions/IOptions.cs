namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 实现此空接口以自动从配置文件中读取对应的节点并绑定注入到容器，
    /// 之后可在构造函数中通过 Microsoft.Extensions.Options.IOptions<![CDATA[<TOption>]]> 注入
    /// </summary>
    public interface IOptions
    {
    }
}
