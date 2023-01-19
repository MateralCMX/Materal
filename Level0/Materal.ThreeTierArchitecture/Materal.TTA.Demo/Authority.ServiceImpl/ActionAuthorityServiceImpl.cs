using Authority.DataTransmitModel.ActionAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Domain.Repositories.Views;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.ActionAuthority;
using AutoMapper;
using Materal.Common;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Authority.Domain.Views;

namespace Authority.ServiceImpl
{
    /// <summary>
    /// 功能权限服务
    /// </summary>
    public sealed class ActionAuthorityServiceImpl : IActionAuthorityService
    {
        private readonly IActionAuthorityRepository _actionAuthorityRepository;
        private readonly IUserOwnedActionAuthorityRepository _userOwnedActionAuthorityRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public ActionAuthorityServiceImpl(IActionAuthorityRepository actionAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _actionAuthorityRepository = actionAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
        }
        public async Task AddActionAuthorityAsync(AddActionAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.ActionGroupCode)) throw new InvalidOperationException("功能组标识为空");
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _actionAuthorityRepository.CountAsync(m => m.Code == model.Code && m.ActionGroupCode == model.ActionGroupCode) > 0) throw new InvalidOperationException("同一个功能组下只允许存在一个唯一的代码");
            var actionAuthority = model.CopyProperties<ActionAuthority>();
            _authorityUnitOfWork.RegisterAdd(actionAuthority);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task EditActionAuthorityAsync(EditActionAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.ActionGroupCode)) throw new InvalidOperationException("功能组标识为空");
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _actionAuthorityRepository.CountAsync(m => m.ID != model.ID && m.Code == model.Code && m.ActionGroupCode == model.ActionGroupCode) > 0) throw new InvalidOperationException("同一个功能组下只允许存在一个唯一的代码");
            ActionAuthority actionAuthorityFromDB = await _actionAuthorityRepository.FirstOrDefaultAsync(model.ID);
            if (actionAuthorityFromDB == null) throw new InvalidOperationException("功能权限不存在");
            model.CopyProperties(actionAuthorityFromDB);
            _authorityUnitOfWork.RegisterEdit(actionAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task DeleteActionAuthorityAsync(Guid id)
        {
            ActionAuthority actionAuthorityFromDB = await _actionAuthorityRepository.FirstOrDefaultAsync(id);
            if (actionAuthorityFromDB == null) throw new InvalidOperationException("功能权限不存在");
            _authorityUnitOfWork.RegisterDelete(actionAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<ActionAuthorityDTO> GetActionAuthorityInfoAsync(Guid id)
        {
            ActionAuthority actionAuthorityFromDB = await _actionAuthorityRepository.FirstOrDefaultAsync(id);
            if (actionAuthorityFromDB == null) throw new InvalidOperationException("功能权限不存在");
            return _mapper.Map<ActionAuthorityDTO>(actionAuthorityFromDB);
        }
        public async Task<(List<ActionAuthorityListDTO> result, PageModel pageModel)> GetActionAuthorityListAsync(QueryActionAuthorityFilterModel filterModel)
        {
            Expression<Func<ActionAuthority, bool>> expression = m => true;
            if (!string.IsNullOrEmpty(filterModel.ActionGroupCode))
            {
                expression = expression.And(m => m.ActionGroupCode == filterModel.ActionGroupCode);
            }
            if (!string.IsNullOrEmpty(filterModel.Code))
            {
                expression = expression.And(m => m.Code == filterModel.Code);
            }
            if (!string.IsNullOrEmpty(filterModel.Name))
            {
                expression = expression.And(m => EF.Functions.Like(m.Name, $"%{filterModel.Name}%"));
            }
            (List<ActionAuthority> actionAuthoritiesFromDB, PageModel pageModel) = await _actionAuthorityRepository.PagingAsync(expression, filterModel);
            return (_mapper.Map<List<ActionAuthorityListDTO>>(actionAuthoritiesFromDB), pageModel);
        }
        public async Task<List<ActionAuthorityListDTO>> GetUserOwnedActionAuthorityListAsync(Guid userID, string actionGroupCode)
        {
            List<UserOwnedActionAuthority> userOwnedActionAuthorities = await _userOwnedActionAuthorityRepository.WhereAsync(m => m.UserID == userID && m.ActionGroupCode == actionGroupCode).ToList();
            return _mapper.Map<List<ActionAuthorityListDTO>>(userOwnedActionAuthorities);
        }
    }
}
