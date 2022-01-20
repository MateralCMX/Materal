using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MPB.Domain
{
    public class DomainModel
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; private set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string[] Namespaces => string.IsNullOrWhiteSpace(Namespace) ? null : Namespace.Split(".");
        /// <summary>
        /// 相对命名空间
        /// </summary>
        private string _relativeNamespace = null;
        /// <summary>
        /// 相对命名空间
        /// </summary>
        public string RelativeNamespace
        {
            get
            {
                if (_relativeNamespace != null) return _relativeNamespace;
                if (Namespaces == null) return null;
                for (int i = 2; i < Namespaces.Length; i++)
                {
                    _relativeNamespace += $"{Namespaces[i]}.";
                }
                _relativeNamespace = _relativeNamespace.Substring(0, _relativeNamespace.Length -1);
                return _relativeNamespace;
            }
        }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string ProjectName => Namespaces?[0];
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; private set; }
        /// <summary>
        /// 属性
        /// </summary>
        public List<PropertyModel> Properties { get; private set; } = new List<PropertyModel>();
        /// <summary>
        /// 状态
        /// </summary>
        private DomainHandleStatusEnum _status = DomainHandleStatusEnum.ReadNameSpace;
        /// <summary>
        /// 文件
        /// </summary>
        public FileInfo FileInfo { get; private set; }
        public DomainModel(string csFilePath)
        {
            FileInfo = new FileInfo(csFilePath);
        }
        public DomainModel(FileInfo csFileInfo)
        {
            FileInfo = csFileInfo;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitAsync()
        {
            if (FileInfo == null || !FileInfo.Exists) return false;
            if (!FileInfo.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase)) return false;
            string[] cSharpCodes = await File.ReadAllLinesAsync(FileInfo.FullName);
            for (int i = 0; i < cSharpCodes.Length; i++)
            {
                switch (_status)
                {
                    case DomainHandleStatusEnum.ReadNameSpace:
                        HandlerNameSpace(cSharpCodes, i);
                        break;
                    case DomainHandleStatusEnum.ReadName:
                        HandlerName(cSharpCodes, i);
                        break;
                    case DomainHandleStatusEnum.ReadProperty:
                        HandlerProperty(cSharpCodes, i);
                        break;
                }
            }
            return true;
        }
        /// <summary>
        /// 处理命名空间
        /// </summary>
        /// <param name="cSharpCodes"></param>
        /// <param name="index"></param>
        private void HandlerNameSpace(string[] cSharpCodes, int index)
        {
            const string key = "namespace ";
            string cSharpCode = cSharpCodes[index].Trim();
            if (!cSharpCode.StartsWith(key)) return;
            Namespace = cSharpCode.Substring(key.Length);
            var blankIndex = Namespace.IndexOf(" ");
            if (blankIndex > -1)
            {
                Namespace = Namespace.Substring(0, blankIndex);
            }
            _status = DomainHandleStatusEnum.ReadName;
        }
        /// <summary>
        /// 处理名称
        /// </summary>
        /// <param name="cSharpCodes"></param>
        /// <param name="index"></param>
        private void HandlerName(string[] cSharpCodes, int index)
        {
            const string key = "public class ";
            string cSharpCode = cSharpCodes[index].Trim();
            if (!cSharpCode.StartsWith(key)) return;
            Name = cSharpCode.Substring(key.Length);
            var blankIndex = Name.IndexOf(" ");
            if (blankIndex > -1)
            {
                Name = Name.Substring(0, blankIndex);
            }
            #region 获取注释
            Annotation = GetAnnotation(cSharpCodes, index);
            #endregion
            _status = DomainHandleStatusEnum.ReadProperty;
        }
        /// <summary>
        /// 处理属性
        /// </summary>
        /// <param name="cSharpCodes"></param>
        /// <param name="index"></param>
        private void HandlerProperty(string[] cSharpCodes, int index)
        {
            const string startKey = "public ";
            const string endKey = " { get; set; }";
            string cSharpCode = cSharpCodes[index].Trim();
            if (!cSharpCode.StartsWith(startKey) || !cSharpCode.EndsWith(endKey)) return;
            var property = new PropertyModel();
            cSharpCode = cSharpCode.Substring(startKey.Length);
            var blankIndex = cSharpCode.IndexOf(" ");
            property.Type = cSharpCode.Substring(0, blankIndex);
            cSharpCode = cSharpCode.Substring(property.Type.Length + 1);
            blankIndex = cSharpCode.IndexOf(" ");
            property.Name = cSharpCode.Substring(0, blankIndex);
            property.Annotation = GetAnnotation(cSharpCodes, index);
            Properties.Add(property);
        }
        /// <summary>
        /// 获得注释
        /// </summary>
        /// <param name="cSharpCodes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetAnnotation(string[] cSharpCodes, int index)
        {
            const string annotationKey = "/// <summary>";
            const string annotationKey2 = "/// ";
            int annotationIndex = index;
            while (true)
            {
                annotationIndex--;
                if (annotationIndex < 0) break;
                string cSharpCode = cSharpCodes[annotationIndex].Trim();
                if (!cSharpCode.StartsWith(annotationKey)) continue;
                cSharpCode = cSharpCodes[annotationIndex + 1].Trim();
                return cSharpCode.Substring(annotationKey2.Length);
            }
            return null;
        }
    }
    /// <summary>
    /// Domain处理状态
    /// </summary>
    internal enum DomainHandleStatusEnum
    {
        /// <summary>
        /// 读取命名空间
        /// </summary>
        ReadNameSpace = 0,
        /// <summary>
        /// 读取名称
        /// </summary>
        ReadName = 1,
        /// <summary>
        /// 读取属性
        /// </summary>
        ReadProperty = 2
    }
}
