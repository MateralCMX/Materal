namespace Materal.MergeBlock.GeneratorCode.Attributers
{
    /// <summary>
    /// 映射控制器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MapperControllerAttribute(MapperType type) : Attribute
    {
        /// <summary>
        /// Http方法
        /// </summary>
        public MapperType Type { get; private set; } = type;
        /// <summary>
        /// 是否允许匿名访问
        /// </summary>
        public bool IsAllowAnonymous { get; set; } = false;
    }
    /// <summary>
    /// 映射类型
    /// </summary>
    public enum MapperType
    {
        /// <summary>
        /// Get
        /// </summary>
        Get,
        /// <summary>
        /// Post
        /// </summary>
        Post,
        /// <summary>
        /// Put
        /// </summary>
        Put,
        /// <summary>
        /// Delete
        /// </summary>
        Delete,
        /// <summary>
        /// Patch
        /// </summary>
        Patch,
    }
}
