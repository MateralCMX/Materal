namespace Materal.MergeBlock.Application.Abstractions.Services
{
    /// <summary>
    /// 位序扩展
    /// </summary>
    public static class IndexExtensions
    {
        /// <summary>
        /// 更改位序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domains"></param>
        /// <param name="exchangeID"></param>
        /// <param name="before"></param>
        public static void ExchangeIndex<T>(this List<T> domains, Guid exchangeID, bool before)
            where T : IIndexDomain
        {
            domains = [.. domains.OrderBy(m => m.Index)];
            var count = 0;
            int startIndex;
            int indexTemp;
            if (exchangeID == domains[0].ID)
            {
                startIndex = before ? domains.Count - 2 : domains.Count - 1;
                indexTemp = domains[startIndex].Index;
                for (int i = startIndex; i > count; i--)
                {
                    domains[i].Index = domains[i - 1].Index;
                }
            }
            else
            {
                count = domains.Count - 1;
                startIndex = before ? 0 : 1;
                indexTemp = domains[startIndex].Index;
                for (int i = startIndex; i < count; i++)
                {
                    domains[i].Index = domains[i + 1].Index;
                }
            }
            domains[count].Index = indexTemp;
        }
    }
}
