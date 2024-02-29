using CommunityToolkit.Mvvm.ComponentModel;
using System.Text;

namespace CodeToStringCode
{
    public partial class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(StringCode))]
        private string _projectName = "MMB";
        /// <summary>
        /// 模块名称
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(StringCode))]
        private string _moduleName = "Demo";
        /// <summary>
        /// Domain名称
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(StringCode))]
        private string _domainName = "User";
        /// <summary>
        /// Domain注释
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(StringCode))]
        private string _domainAnnotation = "用户";
        /// <summary>
        /// 代码内容名称
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(StringCode))]
        private string _codeContentName = "codeContent";
        /// <summary>
        /// 代码
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(StringCode))]
        private string _code = string.Empty;
        /// <summary>
        /// 字符串代码
        /// </summary>
        public string StringCode => GetStringCode();
        /// <summary>
        /// 获取字符串代码
        /// </summary>
        /// <returns></returns>
        private string GetStringCode()
        {
            if (Code is null || string.IsNullOrWhiteSpace(Code)) return string.Empty;
            string[] codes = Code.Split('\n');
            StringBuilder stringBuilder = new();
            foreach (string code in codes)
            {
                string codeContent = code;
                if (codeContent.Length > 0 && codeContent.Last() == '\r')
                {
                    codeContent = code[..^1];
                }
                if (codeContent is not null && !string.IsNullOrWhiteSpace(codeContent))
                {
                    codeContent = codeContent.Replace("{", "{{");
                    codeContent = codeContent.Replace("}", "}}");
                    codeContent = codeContent.Replace("\"", "\\\"");
                    codeContent = codeContent.Replace(ProjectName, "{_projectName}");
                    codeContent = codeContent.Replace(ModuleName, "{_moduleName}");
                    codeContent = codeContent.Replace(DomainName, "{domain.Name}");
                    codeContent = codeContent.Replace(DomainAnnotation, "{domain.Annotation}");
                }
                stringBuilder.AppendLine($"{CodeContentName}.AppendLine($\"{codeContent}\");");
            }
            return stringBuilder.ToString();
        }
    }
}
