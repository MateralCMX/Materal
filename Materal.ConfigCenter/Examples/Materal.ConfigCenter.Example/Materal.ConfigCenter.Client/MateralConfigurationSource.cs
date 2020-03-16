using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Materal.ConfigCenter.Client
{

    public class MateralConfigurationSource : IConfigurationSource
    {
        private readonly Dictionary<string, string> _dictionary;

        public MateralConfigurationSource(Dictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var materalConfigurationProvider = new MateralConfigurationProvider();
            foreach ((string key, string value) in _dictionary)
            {
                materalConfigurationProvider.Set(key, value);
            }
            return materalConfigurationProvider;
        }
    }
}
