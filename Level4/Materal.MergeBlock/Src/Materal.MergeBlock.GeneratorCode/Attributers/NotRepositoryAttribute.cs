namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不生成仓储
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotRepositoryAttribute : Attribute { }
}
