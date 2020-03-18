using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Materal.ConfigCenter.ControllerCore;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.Namespace;
using Materal.ConfigCenter.ProtalServer.PresentationModel.Namespace;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.Controllers
{
    public class NamespaceController : ConfigCenterBaseController
    {
        private readonly INamespaceService namespaceService;
        public NamespaceController(INamespaceService namespaceService)
        {
            this.namespaceService = namespaceService;
        }
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddNamespace(AddNamespaceModel model)
        {
            try
            {
                await namespaceService.AddNamespaceAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改命名空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditNamespace(EditNamespaceModel model)
        {
            try
            {
                await namespaceService.EditNamespaceAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteNamespace(Guid id)
        {
            try
            {
                await namespaceService.DeleteNamespaceAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得命名空间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<NamespaceDTO>> GetNamespaceInfo(Guid id)
        {
            try
            {
                NamespaceDTO result = await namespaceService.GetNamespaceInfoAsync(id);
                return ResultModel<NamespaceDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<NamespaceDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<NamespaceDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得命名空间列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<NamespaceListDTO>> GetNamespaceList(QueryNamespaceFilterModel filterModel)
        {
            try
            {
                (List<NamespaceListDTO> result, PageModel pageModel) = await namespaceService.GetNamespaceListAsync(filterModel);
                return PageResultModel<NamespaceListDTO>.Success(result, pageModel, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return PageResultModel<NamespaceListDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return PageResultModel<NamespaceListDTO>.Fail(ex.Message);
            }
        }
    }
}
