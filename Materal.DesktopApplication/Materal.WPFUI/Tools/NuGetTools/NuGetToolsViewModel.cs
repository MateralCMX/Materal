using Materal.ConfigurationHelper;
using Materal.WPFCommon;
using Materal.WPFUI.Tools.NuGetTools.Model;
using System.Collections.Generic;

namespace Materal.WPFUI.Tools.NuGetTools
{
    public sealed class NuGetToolsViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// 配置模版模型组
        /// </summary>
        public List<NuGetToolsConfigTemplateModel> ConfigTemplateModels { get; set; } = new List<NuGetToolsConfigTemplateModel>();

        /// <summary>
        /// 当前选择的配置模板模型
        /// </summary>
        public NuGetToolsConfigTemplateModel SelectedConfigTemplateModel { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string ProjectAddress { get; set; }

        /// <summary>
        /// 目标地址
        /// </summary>
        public string TargetAddress { get; set; }

        /// <summary>
        /// 采用Debug版本标识
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// 采用Release版本标识
        /// </summary>
        public bool Release { get; set; }

        /// <summary>
        /// 采用NuGet包
        /// </summary>
        public bool NuGet { get; set; }

        /// <summary>
        /// 采用DLL文件
        /// </summary>
        public bool DLL { get; set; }

        /// <summary>
        /// 打开资源管理器
        /// </summary>
        public bool OpenExplorer { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            List<NuGetToolsConfigTemplateModel> models = ApplicationConfig.Configuration.GetArrayObjectValue<NuGetToolsConfigTemplateModel>("Tools:NuGetToos");
            ConfigTemplateModels.AddRange(models);
            SelectedConfigTemplateModel = ConfigTemplateModels[0];
            OnPropertyChanged(nameof(ConfigTemplateModels));
            OnPropertyChanged(nameof(SelectedConfigTemplateModel));
        }

        /// <summary>
        /// 更新数据绑定
        /// </summary>
        public void UpdateDataBind()
        {
            OnPropertyChanged(nameof(ProjectAddress));
            OnPropertyChanged(nameof(TargetAddress));
            OnPropertyChanged(nameof(Debug));
            OnPropertyChanged(nameof(Release));
            OnPropertyChanged(nameof(DLL));
            OnPropertyChanged(nameof(NuGet));
        }
    }
}
