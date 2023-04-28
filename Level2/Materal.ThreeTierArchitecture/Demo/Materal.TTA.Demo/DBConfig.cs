using Materal.TTA.Common.Model;

namespace Materal.TTA.Demo
{
    public static class DBConfig
    {
        public static SqliteConfigModel SqliteConfig { get; set; } = new()
        {
            Source = "TTATestDB.db"
        };
        public static SqlServerConfigModel SqlServerConfig { get; set; } = new()
        {
            Address = "175.27.194.19",
            Port = "1433",
            Name = "TTATestDB",
            UserID = "sa",
            Password = "XMJry@456",
            TrustServerCertificate = true
        };
    }
}
