using System.Diagnostics;

namespace Deploy.ServiceImpl.Models
{
    public abstract class ApplicationHandlerContext
    {
        protected ApplicationHandlerContext _next;
        /// <summary>
        /// 设置下一步
        /// </summary>
        /// <param name="next"></param>
        public void SetNext(ApplicationHandlerContext next)
        {
            _next = next;
        }
        /// <summary>
        /// 获取启动程序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract Process GetProcess(ApplicationRuntimeModel model);
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="model"></param>
        public abstract void KillProcess(ApplicationRuntimeModel model);
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="model"></param>
        /// <param name="process"></param>
        public abstract void KillProcess(ApplicationRuntimeModel model, Process process);
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="process"></param>
        public virtual void KillProcess(Process process)
        {
            if (process == null) return;
            process.Kill();
            process.WaitForExit();
            process.Dispose();
        }
    }
}
