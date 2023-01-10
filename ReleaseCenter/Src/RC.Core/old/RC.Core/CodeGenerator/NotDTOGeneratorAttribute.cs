namespace RC.Core.CodeGenerator
{
    /// <summary>
    /// 不生成到DTO
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NotDTOGeneratorAttribute : Attribute { }
}
