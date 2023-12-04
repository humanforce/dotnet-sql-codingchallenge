using CarSales.WebApi.Configuration;
using CarSales.WebApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace CarSales.WebApi.Services
{
    public class AuthService : IAuthService
    {

        private readonly IdentityProviderSettings _settings;
        public AuthService(IOptions<IdentityProviderSettings> settings) {
            _settings = settings.Value;
        }


        /// <summary>
        /// Calls Identity Provider to fetch bear token. For this have used Auth0
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<OAuthResponse> GetToken(OAuthRequest request)
        {
            var restClient = new RestClient(_settings.Authority, configureSerialization: s => s.UseNewtonsoftJson());
            var restRequest = new RestRequest("oauth/token", Method.Post);

            restRequest.AddJsonBody(request);

            var response = await restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<OAuthResponse>(response.Content);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
