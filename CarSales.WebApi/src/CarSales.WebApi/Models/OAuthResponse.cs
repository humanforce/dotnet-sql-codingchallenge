using Newtonsoft.Json;

namespace CarSales.WebApi.Models
{
    public class OAuthResponse
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("scope")]
        public string? Scope { get; set; }

        [JsonProperty("expires_in")]
        public int? ExpiredIn { get; set; }

        [JsonProperty("token_type")]
        public string? TokenType { get; set; }
    }
}
