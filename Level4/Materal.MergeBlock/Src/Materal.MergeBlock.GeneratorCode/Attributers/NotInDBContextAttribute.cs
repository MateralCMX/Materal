namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不在DBContext中生成
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class NotInDBContextAttribute : Attribute { }
}
