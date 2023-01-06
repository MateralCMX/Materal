using RC.Deploy.Enums;

namespace RC.Deploy.ServiceImpl.ApplicationHandlers
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
        public static IApplicationHandler GetApplicationHandler(this ApplicationTypeEnum applicationType)
        {
            switch (applicationType)
            {
                case ApplicationTypeEnum.StaticDocument:
                    return new StaticDocumentApplicationHandler();
                case ApplicationTypeEnum.Exe:
                    return new ExeApplicationHandler();
                case ApplicationTypeEnum.DotNet:
                    return new DotNetApplicationHandler();
                default:
                    throw new ArgumentOutOfRangeException(nameof(applicationType), applicationType, null);
            }
        }
    }
}
