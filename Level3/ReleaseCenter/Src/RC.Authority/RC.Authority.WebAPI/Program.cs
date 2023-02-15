using Materal.Common;
using RC.Authority.Services;

namespace RC.Authority.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            #region ����Ĭ���û�
            IUserService? userService = MateralServices.GetService<IUserService>();
            await userService.AddDefaultUserAsync();
            userService = null;
            #endregion
            await base.InitAsync(args, services, app);
        }
    }
}