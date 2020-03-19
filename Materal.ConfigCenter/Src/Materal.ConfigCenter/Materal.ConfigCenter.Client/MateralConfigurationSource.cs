using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Materal.ConfigCenter.Client
{

    public class MateralConfigurationSource : IConfigurationSource
    {
        private readonly string _namespaceName;
        private readonly Dictionary<string, string> _dictionary;

        public MateralConfigurationSource(Dictionary<string, string> dictionary, string namespaceName)
        {
            _dictionary = dictionary;
            _namespaceName = namespaceName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var materalConfigurationProvider = new MateralConfigurationProvider(_namespaceName);
            foreach ((string key, string value) in _dictionary)
            {
                materalConfigurationProvider.Set(key, value);
            }
            return materalConfigurationProvider;
        }
    }
}
