namespace RC.Authority.Application
{
    /// <summary>
    /// Authority控制器
    /// </summary>
    [Route("AuthorityAPI/[controller]/[action]")]
    public abstract class AuthorityController : MergeBlockController
    {
    }
    /// <summary>
    /// Authority控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    [Route("AuthorityAPI/[controller]/[action]")]
    public abstract class AuthorityController<TService> : MergeBlockController<TService>
        where TService : IBaseService
    {
    }
    /// <summary>
    /// Authority控制器
    /// </summary>
    /// <typeparam name="TAddRequestModel"></typeparam>
    /// <typeparam name="TEditRequestModel"></typeparam>
    /// <typeparam name="TQueryRequestModel"></typeparam>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    /// <typeparam name="TService"></typeparam>
    [Route("AuthorityAPI/[controller]/[action]")]
    public abstract class AuthorityController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TService> : MergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TService>
        where TAddRequestModel : class, IAddRequestModel, new()
        where TEditRequestModel : class, IEditRequestModel, new()
        where TQueryRequestModel : IQueryRequestModel, new()
        where TAddModel : class, IAddServiceModel, new()
        where TEditModel : class, IEditServiceModel, new()
        where TQueryModel : IQueryServiceModel, new()
        where TDTO : class, IDTO, new()
        where TListDTO : class, IListDTO, new()
        where TService : IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
    {
    }
}
