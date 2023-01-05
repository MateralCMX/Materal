namespace RC.CodeGenerator
{
    /// <summary>
    /// 不生成到实体配置文件
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NotEntityConfigGeneratorAttribute : Attribute { }
}
