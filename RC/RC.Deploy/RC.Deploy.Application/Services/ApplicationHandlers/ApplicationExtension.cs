namespace RC.Deploy.Application.Services.ApplicationHandlers
{
    /// <summary>
    /// 应用程序扩展类
    /// </summary>
    public static class ApplicationExtension
    {
        /// <summary>
        /// 获得应用程序处理器
        /// </summary>
        /// <param name="applicationType"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IApplicationHandler GetApplicationHandler(this ApplicationTypeEnum applicationType, IServiceProvider serviceProvider) => applicationType switch
        {
            ApplicationTypeEnum.StaticDocument => typeof(StaticDocumentApplicationHandler).Instantiation<IApplicationHandler>(serviceProvider),
            ApplicationTypeEnum.Exe => typeof(ExeApplicationHandler).Instantiation<IApplicationHandler>(serviceProvider),
            ApplicationTypeEnum.DotNet => typeof(DotNetApplicationHandler).Instantiation<IApplicationHandler>(serviceProvider),
            _ => throw new ArgumentOutOfRangeException(nameof(applicationType), applicationType, null),
        };
    }
}
