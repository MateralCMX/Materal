using Materal.BaseCore.CodeGenerator.Models;

namespace Materal.BaseCore.CodeGenerator
{
    public interface IMateralBaseCoreCodeGeneratorPlug
    {
        /// <summary>
        /// 插件执行
        /// </summary>
        /// <param name="model"></param>
        public void PlugExecute(DomainPlugModel model);
    }
}
