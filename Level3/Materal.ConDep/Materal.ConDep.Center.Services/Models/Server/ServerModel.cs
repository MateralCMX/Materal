namespace Materal.ConDep.Center.Services.Models.Server
{
    public class ServerModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthAddress => $"http://{Address}/api/Health/Health";
    }
}
