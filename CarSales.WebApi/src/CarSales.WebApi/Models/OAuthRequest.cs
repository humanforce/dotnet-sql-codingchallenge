using Newtonsoft.Json;

namespace CarSales.WebApi.Models
{
    public class OAuthRequest
    {
        [JsonProperty("client_id")]
        public string? ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string? ClientSecret { get; set; }

        [JsonProperty("audience")]
        public string? Audience { get; set; }

        [JsonProperty("grant_type")]
        public string? GranType { get; set; }
    }
}
