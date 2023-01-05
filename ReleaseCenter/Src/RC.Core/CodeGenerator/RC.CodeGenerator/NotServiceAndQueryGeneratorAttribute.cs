namespace RC.CodeGenerator
{
    /// <summary>
    /// 不生成服务代码
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotServiceAndQueryGeneratorAttribute : Attribute { }
}
