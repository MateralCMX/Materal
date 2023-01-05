using System;

namespace RC.InGenerator.Attribtues
{
    /// <summary>
    /// 不生成到DTO
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NotListDTOGeneratorAttribute : Attribute { }
}
