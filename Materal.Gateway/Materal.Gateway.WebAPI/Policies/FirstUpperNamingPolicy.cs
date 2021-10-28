using System.Text.Json;
using Materal.StringHelper;

namespace Materal.Gateway.WebAPI.Policies
{
    public class FirstUpperNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.FirstUpper();
        }
    }
}
