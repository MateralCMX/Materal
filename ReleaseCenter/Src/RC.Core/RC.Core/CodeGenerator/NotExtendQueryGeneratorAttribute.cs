namespace RC.Core.CodeGenerator
{
    /// <summary>
    /// 不生成扩展查询
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NotExtendQueryGeneratorAttribute : Attribute
    {
    }
}
