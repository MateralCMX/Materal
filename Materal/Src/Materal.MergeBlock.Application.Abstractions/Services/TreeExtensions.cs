namespace Materal.MergeBlock.Application.Abstractions.Services
{
    /// <summary>
    /// 树扩展
    /// </summary>
    public static class TreeExtensions
    {
        /// <summary>
        /// 转换为树形结构
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="treeDomains"></param>
        /// <param name="parentID"></param>
        /// <param name="action"></param>
        /// <param name="autorecode"></param>
        /// <returns></returns>
        public static List<TDto> ToTree<TDomain, TDto>(this List<TDomain> treeDomains, Guid? parentID = null, Action<TDto, TDomain>? action = null, bool autorecode = true)
            where TDomain : ITreeDomain
            where TDto : ITreeDTO<TDto>, new()
        {
            List<TDto> result = [];
            Hashtable data = [];
            Hashtable noFindParentData = [];
            foreach (TDomain domain in treeDomains)
            {
                TDto dto = new();
                if (autorecode)
                {
                    domain.CopyProperties(dto);
                }
                action?.Invoke(dto, domain);
                if (!data.ContainsKey(domain.ID))
                {
                    data.Add(domain.ID, dto);
                }
                #region 寻找父级
                if (dto.ParentID is not null)
                {
                    if (data.ContainsKey(dto.ParentID.Value))
                    {
                        if (data[dto.ParentID.Value] is TDto parentDto)
                        {
                            parentDto.Children.Add(dto);
                        }
                    }
                    else
                    {
                        if (noFindParentData.ContainsKey(dto.ParentID.Value))
                        {
                            if (noFindParentData[dto.ParentID.Value] is List<TDto> parentDtos)
                            {
                                parentDtos.Add(dto);
                            }
                        }
                        else
                        {
                            List<TDto> dtos = [dto];
                            noFindParentData.Add(dto.ParentID.Value, dtos);
                        }
                    }
                }
                #endregion
                #region 寻找子级
                if (noFindParentData.ContainsKey(dto.ID) && noFindParentData[dto.ID] is List<TDto> children)
                {
                    dto.Children = children;
                    noFindParentData.Remove(dto.ID);
                }
                #endregion
                if (domain.ParentID == parentID)
                {
                    result.Add(dto);
                }
            }
            return result;
        }
    }
}
