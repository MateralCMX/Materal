using System.Windows.Input;

namespace NCWM.UI.Ctrls.ConfigSetting
{
    public class ConfigSettingCommands
    {
        /// <summary>
        /// 添加配置
        /// </summary>
        public static RoutedUICommand AddConfig { get; }
        /// <summary>
        /// 保存配置组
        /// </summary>
        public static RoutedUICommand SaveConfigs { get; }
        /// <summary>
        /// 删除配置
        /// </summary>
        public static RoutedUICommand DeleteConfig { get; }
        /// <summary>
        /// 浏览目录
        /// </summary>
        public static RoutedUICommand BrowseCatalog { get; }

        static ConfigSettingCommands()
        {
            AddConfig = new RoutedUICommand(
                "添加配置",
                nameof(AddConfig),
                typeof(ConfigSettingCommands));
            SaveConfigs = new RoutedUICommand(
                "保存配置",
                nameof(SaveConfigs),
                typeof(ConfigSettingCommands));
            DeleteConfig = new RoutedUICommand(
                "删除配置",
                nameof(DeleteConfig),
                typeof(ConfigSettingCommands));
            BrowseCatalog = new RoutedUICommand(
                "浏览目录",
                nameof(BrowseCatalog),
                typeof(ConfigSettingCommands));
        }
    }
}
