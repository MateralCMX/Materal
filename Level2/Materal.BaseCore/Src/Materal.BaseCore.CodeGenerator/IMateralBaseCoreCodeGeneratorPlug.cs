using Materal.BaseCore.CodeGenerator.Models;

namespace Materal.BaseCore.CodeGenerator
{
    public interface IMateralBaseCoreCodeGeneratorPlug
    {
        /// <summary>
        /// 根据Domain创建文件
        /// </summary>
        /// <param name="plugCreateFileModel"></param>
        public void CreateFileByDomain(PlugCreateFileModel plugCreateFileModel);
    }
}
