using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.Domain;
using System.Collections;

namespace Materal.BaseCore.ServiceImpl
{
    public static class TreeExtensions
    {
        public static List<TDto> ToTree<TDomain, TDto>(this List<TDomain> treeDomains, Guid? parentID = null, Action<TDto, TDomain>? action = null, bool autorecode = true)
            where TDomain : ITreeDomain
            where TDto : ITreeDTO<TDto>, new()
        {
            List<TDto> result = new();
            Hashtable hashtable = new();
            foreach (TDomain domain in treeDomains)
            {
                TDto dto = new();
                if (autorecode)
                {
                    domain.CopyProperties(dto);
                }
                action?.Invoke(dto, domain);
                if (domain.ParentID is not null && hashtable.ContainsKey(domain.ParentID) && hashtable[domain.ParentID.Value] is TDto parentDto)
                {
                    parentDto.Children.Add(dto);
                }
                if (!hashtable.ContainsKey(domain.ID))
                {
                    hashtable.Add(domain.ID, dto);
                }
                if(domain.ParentID == parentID)
                {
                    result.Add(dto);
                }
            }
            return result;
        }
    }
}
