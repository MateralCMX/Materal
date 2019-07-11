using System.Collections.Generic;

namespace MateralMapper.Core
{
    public interface IMateralMapper
    {
        IEnumerable<TDestination> Map<TSource,TDestination>(IEnumerable<TSource> sources);
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
