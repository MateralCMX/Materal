using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Deploy.ServiceImpl.Models;
using Materal.Model;

namespace Deploy.ServiceImpl.Manage
{
    public interface IApplicationManage
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Add(ApplicationInfoModel model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Edit(ApplicationInfoModel model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApplicationInfoModel Delete(Guid id);
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Start(Guid id);
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Stop(Guid id);
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICollection<string> GetConsoleMessage(Guid id);
        /// <summary>
        /// 全部启动
        /// </summary>
        /// <returns></returns>
        void StartAll();
        /// <summary>
        /// 全部停止
        /// </summary>
        /// <returns></returns>
        void StopAll();
        /// <summary>
        /// 获得所有信息
        /// </summary>
        /// <returns></returns>
        List<ApplicationInfoModel> GetAllList();
        /// <summary>
        /// 根据路径获取运行模型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ApplicationRuntimeModel GetRuntimeModelByPath(string path);
    }
}
