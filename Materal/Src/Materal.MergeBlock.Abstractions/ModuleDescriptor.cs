using System.Runtime.CompilerServices;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 模块描述
    /// </summary>
    public class ModuleDescriptor(Type type, IMergeBlockModule instance, string pluginName)
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName { get; } = pluginName;
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; } = type;
        /// <summary>
        /// 实例
        /// </summary>
        public IMergeBlockModule Instance { get; } = instance;
        /// <summary>
        /// 依赖
        /// </summary>
        private readonly List<ModuleDescriptor> _dependencies = [];
        /// <summary>
        /// 依赖
        /// </summary>
        public IReadOnlyList<ModuleDescriptor> Dependencies => _dependencies.AsReadOnly();
        /// <summary>
        /// 添加依赖
        /// </summary>
        /// <param name="module"></param>
        public void AddDependency(ModuleDescriptor module) => _dependencies.Add(module);
        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();
        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is ModuleDescriptor moduleDescriptor && moduleDescriptor.Type == Type;
        /// <inheritdoc/>
        public override string ToString()
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new(4, 3);
            interpolatedStringHandler.AppendFormatted(PluginName);
            interpolatedStringHandler.AppendLiteral(".");
            interpolatedStringHandler.AppendFormatted(Instance?.Name);
            interpolatedStringHandler.AppendLiteral(" <");
            interpolatedStringHandler.AppendFormatted(Type?.Name);
            interpolatedStringHandler.AppendLiteral(">");
            return interpolatedStringHandler.ToStringAndClear();
        }
    }
}
