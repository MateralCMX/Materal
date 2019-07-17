using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Materal.ConfigurationHelper;
using Materal.ConvertHelper;
using Materal.FileHelper;
using Materal.WPFCommon;
using NCWM.Model;

namespace NCWM.UI.Windows.ConfigSetting
{
    public class ConfigSettingViewModel : NotifyPropertyChanged
    {
        private ConfigModel _selectConfig;
        /// <summary>
        /// Config列表
        /// </summary>
        public ObservableCollection<ConfigModel> Configs { get;}
        /// <summary>
        /// 当前选择的Config
        /// </summary>
        public ConfigModel SelectConfig
        {
            get => _selectConfig;
            set
            {
                _selectConfig = value;
                OnPropertyChanged();
            }
        }
        public ConfigSettingViewModel()
        {
            Configs = new ObservableCollection<ConfigModel>(ApplicationConfig.Configs);
            if (Configs.Count > 0)
            {
                SelectConfig = Configs[0];
            }
        }
        /// <summary>
        /// 添加配置
        /// </summary>
        public void AddConfig()
        {
            var newConfig = new ConfigModel
            {
                Name = "新配置",
                DotNetCoreVersion = 2.2f
            };
            Configs.Add(newConfig);
            SelectConfig = newConfig;
        }
        /// <summary>
        /// 删除配置
        /// </summary>
        public void DeleteConfig()
        {
            Configs.Remove(SelectConfig);
            SelectConfig = Configs.Count > 0 ? Configs[0] : null;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        public void SaveConfig()
        {
            ApplicationConfig.Configs = Configs.ToList();
            var config = new AppSettingsModel
            {
                Title = ApplicationConfig.TitleConfig,
                Configs = ApplicationConfig.Configs
            };
            TextFileManager.WriteText(ApplicationConfig.ConfigFilePath, config.ToJson());
        }
        /// <summary>
        /// 更改选择的配置路径
        /// </summary>
        /// <param name="path"></param>
        public void ChangeSelectConfigPath(string path)
        {
            if (SelectConfig == null) throw new InvalidOperationException("请选择一个配置");
            SelectConfig.Path = path;
            OnPropertyChanged(nameof(SelectConfig));
        }
    }
}
