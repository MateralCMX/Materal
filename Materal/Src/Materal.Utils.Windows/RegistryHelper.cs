namespace Materal.Utils.Windows
{
    /// <summary>
    /// 注册表管理器
    /// </summary>
    public class RegistryHelper
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="registryKeys"></param>
        /// <returns></returns>
        public static bool AnyAll(string name, params RegistryKey[] registryKeys)
        {
            RegistryKey[] scope;
            if (registryKeys is null || registryKeys.Length == 0)
            {
                scope =
                [
                    Registry.ClassesRoot,
                    Registry.LocalMachine,
                    Registry.CurrentUser,
                    Registry.Users,
                    Registry.CurrentConfig
                ];
            }
            else
            {
                scope = registryKeys;
            }
            foreach (RegistryKey registryKey in scope)
            {
                RegistryKey? temp = registryKey.OpenSubKey(name);
                if (temp is not null) return true;
            }
            return false;
        }
    }
}
