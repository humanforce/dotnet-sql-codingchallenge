using CarSales.WebApi.Models;

namespace CarSales.WebApi.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Get Access Token
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="UnauthorizedAccessException">Authentication Failed</exception>
        /// <returns></returns>
        Task<OAuthResponse> GetToken(OAuthRequest request);
    }
}
