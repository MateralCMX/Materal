using Materal.StringHelper;
using System.Text.Json;

namespace Materal.APP.WebCore.Policies
{
    public class FirstUpperNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.FirstUpper();
        }
    }
}
