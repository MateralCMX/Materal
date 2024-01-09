namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不生成实体配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotEntityConfigAttribute : Attribute { }
}
