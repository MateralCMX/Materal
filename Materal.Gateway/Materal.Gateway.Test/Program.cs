using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ocelot.Configuration.File;

namespace Materal.Gateway.Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "http://127.0.0.1:8899/administration",
                Policy =
                {
                    ValidateIssuerName = false,
                    ValidateEndpoints = false
                }
            });
            if (!disco.IsError)
            {
                TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "admin",
                    ClientSecret = "MateralGateWay",
                    Scope = "admin",
                    GrantType = "client_credentials"
                });
                if (!tokenResponse.IsError)
                {
                    string token = tokenResponse.AccessToken;
                    Console.WriteLine(token);
                    //FileConfiguration configuration
                }
                else
                {
                    Console.WriteLine(tokenResponse.Error);
                }
            }
            else
            {
                Console.WriteLine(disco.Error);
            }
            Console.ReadKey();
        }
    }
}
