using Deploy.Enums;
using Deploy.ServiceImpl.Models;
using System;

namespace Deploy.ServiceImpl.Manage
{
    public static class ApplicationHandlerHelper
    {
        public static IApplicationHandler GetApplicationHandler(ApplicationTypeEnum applicationType)
        {
            switch (applicationType)
            {
                case ApplicationTypeEnum.StaticDocument:
                    return new StaticDocumentApplicationHandler();
                case ApplicationTypeEnum.Exe:
                    return new ExeApplicationHandler();
                case ApplicationTypeEnum.DotNet:
                    return new DotNetApplicationHandler();
                case ApplicationTypeEnum.NodeJS:
                    return new NodeJSApplicationHandler();
                //case ApplicationTypeEnum.Java:
                //    return new JavaApplicationHandler();
                default:
                    throw new ArgumentOutOfRangeException(nameof(applicationType), applicationType, null);
            }
        }
    }
}
