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
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IApplicationHandler GetApplicationHandler(this ApplicationTypeEnum applicationType) => applicationType switch
        {
            ApplicationTypeEnum.StaticDocument => new StaticDocumentApplicationHandler(),
            ApplicationTypeEnum.Exe => new ExeApplicationHandler(),
            ApplicationTypeEnum.DotNet => new DotNetApplicationHandler(),
            _ => throw new ArgumentOutOfRangeException(nameof(applicationType), applicationType, null),
        };
    }
}
