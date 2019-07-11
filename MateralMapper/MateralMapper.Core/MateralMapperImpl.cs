using System.Collections.Generic;
using System.Linq;

namespace MateralMapper.Core
{
    public class MateralMapperImpl : IMateralMapper
    {
        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sources)
        {
            return sources.Select(ExpressionGenericMapper<TSource, TDestination>.ConvertTo);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return ExpressionGenericMapper<TSource, TDestination>.ConvertTo(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return ExpressionGenericMapper<TSource, TDestination>.ConvertTo(source, destination);
        }
    }
}
