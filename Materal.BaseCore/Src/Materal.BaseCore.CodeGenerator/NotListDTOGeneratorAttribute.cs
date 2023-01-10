namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不生成到ListDTO
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NotListDTOGeneratorAttribute : Attribute { }
}
