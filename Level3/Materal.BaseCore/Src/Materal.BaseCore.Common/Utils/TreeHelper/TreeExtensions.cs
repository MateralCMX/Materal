using Materal.ConvertHelper;

namespace Materal.BaseCore.Common.Utils.TreeHelper
{
    public static class TreeExtensions
    {
        public static List<TDto> ToTree<TDomain, TDto>(this List<TDomain> treeDomains, Guid? parentID = null, Action<TDto, TDomain>? action = null)
            where TDomain : ITreeDomain
            where TDto : ITreeDTO<TDto>, new()
        {
            List<TDto> result = new();
            List<TDomain> data = treeDomains.Where(m => m.ParentID == parentID).ToList();
            foreach (TDomain domain in data)
            {
                TDto dto = new();
                domain.CopyProperties(dto);
                action?.Invoke(dto, domain);
                dto.Children = ToTree(treeDomains, domain.ID, action);
                result.Add(dto);
            }
            return result;
        }
    }
}
