using AutoMapper;
using Materal.Common;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WeChatService.DataTransmitModel.Application;
using WeChatService.Domain;
using WeChatService.Domain.Repositories;
using WeChatService.EFRepository;
using WeChatService.Service;
using WeChatService.Service.Model.Application;
namespace WeChatService.ServiceImpl
{
    /// <summary>
    /// 应用服务
    /// </summary>
    public sealed class ApplicationServiceImpl : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        private readonly IWeChatServiceUnitOfWork _weChatServiceUnitOfWork;
        public ApplicationServiceImpl(IApplicationRepository applicationRepository, IMapper mapper, IWeChatServiceUnitOfWork weChatServiceUnitOfWork)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _weChatServiceUnitOfWork = weChatServiceUnitOfWork;
        }
        public async Task AddApplicationAsync(AddApplicationModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (string.IsNullOrEmpty(model.AppID)) throw new InvalidOperationException("AppID为空");
            if (string.IsNullOrEmpty(model.AppSecret)) throw new InvalidOperationException("AppSecret为空");
            if (await _applicationRepository.CountAsync(m => m.Name == model.Name) > 0) throw new InvalidOperationException("名称重复");
            if (await _applicationRepository.CountAsync(m => m.AppID == model.AppID) > 0) throw new InvalidOperationException("AppID重复");
            if (await _applicationRepository.CountAsync(m => m.WeChatToken == model.WeChatToken) > 0) throw new InvalidOperationException("WeChatToken重复");
            var application = model.CopyProperties<Application>();
            _weChatServiceUnitOfWork.RegisterAdd(application);
            await _weChatServiceUnitOfWork.CommitAsync();
        }
        public async Task EditApplicationAsync(EditApplicationModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (string.IsNullOrEmpty(model.AppID)) throw new InvalidOperationException("AppID为空");
            if (string.IsNullOrEmpty(model.AppSecret)) throw new InvalidOperationException("AppSecret为空");
            if (await _applicationRepository.CountAsync(m => m.ID != model.ID && m.Name == model.Name) > 0) throw new InvalidOperationException("名称重复");
            if (await _applicationRepository.CountAsync(m => m.ID != model.ID && m.AppID == model.AppID) > 0) throw new InvalidOperationException("AppID重复");
            if (await _applicationRepository.CountAsync(m => m.ID != model.ID && m.WeChatToken == model.WeChatToken) > 0) throw new InvalidOperationException("WeChatToken重复");
            Application applicationFromDB = await _applicationRepository.FirstOrDefaultAsync(model.ID);
            if (applicationFromDB == null) throw new InvalidOperationException("应用不存在");
            model.CopyProperties(applicationFromDB);
            applicationFromDB.UpdateTime = DateTime.Now;
            _weChatServiceUnitOfWork.RegisterEdit(applicationFromDB);
            await _weChatServiceUnitOfWork.CommitAsync();
        }
        public async Task DeleteApplicationAsync(Guid id)
        {
            Application applicationFromDB = await _applicationRepository.FirstOrDefaultAsync(id);
            if (applicationFromDB == null) throw new InvalidOperationException("应用不存在");
            _weChatServiceUnitOfWork.RegisterDelete(applicationFromDB);
            await _weChatServiceUnitOfWork.CommitAsync();
        }
        public async Task<ApplicationDTO> GetApplicationInfoAsync(Guid id)
        {
            Application applicationFromDB = await _applicationRepository.FirstOrDefaultAsync(id);
            if (applicationFromDB == null) throw new InvalidOperationException("应用不存在");
            return _mapper.Map<ApplicationDTO>(applicationFromDB);
        }

        public async Task<ApplicationDTO> GetApplicationInfoAsync(string appID, Guid userID)
        {
            Application applicationFromDB = await _applicationRepository.FirstOrDefaultAsync(m=>m.AppID == appID && m.UserID == userID);
            if (applicationFromDB == null) throw new InvalidOperationException("应用不存在");
            return _mapper.Map<ApplicationDTO>(applicationFromDB);
        }

        public async Task<(List<ApplicationListDTO> result, PageModel pageModel)> GetApplicationListAsync(QueryApplicationFilterModel filterModel)
        {
            Expression<Func<Application, bool>> expression = m => true;
            if (filterModel.UserID.HasValue)
            {
                expression = expression.And(m => filterModel.UserID == m.UserID);
            }
            if (!string.IsNullOrEmpty(filterModel.AppID))
            {
                expression = expression.And(m => m.AppID == filterModel.AppID);
            }
            if (!string.IsNullOrEmpty(filterModel.WeChatToken))
            {
                expression = expression.And(m => m.WeChatToken == filterModel.WeChatToken);
            }
            if (!string.IsNullOrEmpty(filterModel.Name))
            {
                expression = expression.And(m => EF.Functions.Like(m.Name, $"%{filterModel.Name}%"));
            }
            (List<Application> applicationsFromDB, PageModel pageModel) = await _applicationRepository.PagingAsync(expression, filterModel);
            return (_mapper.Map<List<ApplicationListDTO>>(applicationsFromDB), pageModel);
        }
    }
}
