using System.Collections.Generic;

namespace CodeCreate.Model
{
    public class ServiceImplModel
    {        /// <summary>
        /// 领域模型
        /// </summary>
        private readonly List<DomainModel> _domains;

        public ServiceImplModel(List<DomainModel> domains)
        {
            _domains = domains;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateFile(string targetPath, string subSystemName)
        {
            foreach (DomainModel domain in _domains)
            {
                domain.CreateServiceImplFile(targetPath, subSystemName);
            }
        }
    }
}
