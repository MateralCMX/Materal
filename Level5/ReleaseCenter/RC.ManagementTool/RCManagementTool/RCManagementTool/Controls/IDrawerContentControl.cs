namespace RCManagementTool.Controls
{
    public interface IDrawerContentControl
    {
        /// <summary>
        /// 宽度
        /// </summary>
        double Width { get; set; }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();
    }
    public interface IDrawerContentControl<T> : IDrawerContentControl
    {
        /// <summary>
        /// 宽度
        /// </summary>
        T ViewModel { get; }
    }
}
