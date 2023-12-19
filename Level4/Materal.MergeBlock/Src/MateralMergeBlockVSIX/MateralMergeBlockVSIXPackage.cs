using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace MateralMergeBlockVSIX
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuid)]
    public sealed class MateralMergeBlockVSIXPackage : AsyncPackage
    {
        /// <summary>
        /// 包Guid
        /// </summary>
        public const string PackageGuid = "fccefe4f-19a8-470b-95e1-43321d68938f";
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress) => await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
    }
}
