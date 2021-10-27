using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client
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
            foreach (KeyValuePair<string, string> item in _dictionary)
            {
                materalConfigurationProvider.Set(item.Key, item.Value);
            }
            return materalConfigurationProvider;
        }
    }
}
