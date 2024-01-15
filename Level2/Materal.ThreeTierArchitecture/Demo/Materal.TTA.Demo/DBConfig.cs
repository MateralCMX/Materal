using Materal.TTA.MySqlEFRepository;
using Materal.TTA.SqliteEFRepository;
using Materal.TTA.SqlServerEFRepository;

namespace Materal.TTA.Demo
{
    public static class DBConfig
    {
        public static SqliteConfigModel SqliteConfig { get; set; } = new()
        {
            Source = "D:\\Project\\Materal\\DataBase\\Materal.ThreeTierArchitecture\\TTATestDB.db"
        };
        public static SqlServerConfigModel SqlServerConfig { get; set; } = new()
        {
            Address = "127.0.0.1",
            Port = "1433",
            Name = "TTATestDB",
            UserID = "sa",
            Password = "Materal@1234",
            TrustServerCertificate = true
        };
        public static MySqlConfigModel MySqlConfig { get; set; } = new()
        {
            Address = "127.0.0.1",
            Port = "3306",
            Name = "TTATestDB",
            UserID = "root",
            Password = "Materal@1234",
        };
    }
}
