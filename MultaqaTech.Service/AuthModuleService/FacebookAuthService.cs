using DesignsAndBuild.Core.Entities.Identity.Facebook;
using Microsoft.Extensions.Options;
using MultaqaTech.Core.Services.Contract.AccountModuleContracts;
using Newtonsoft.Json;

namespace MultaqaTech.Service.AuthModuleService;

public class FacebookAuthService : IFacebookAuthService
{

    private readonly HttpClient _httpClient;
    private readonly FacebookAuthConfig _facebookAuthConfig;

    public FacebookAuthService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IOptions<FacebookAuthConfig> facebookAuthConfig)
    {
        _httpClient = httpClientFactory.CreateClient("Facebook");
        _facebookAuthConfig = facebookAuthConfig.Value;
    }


    public async Task<FacebookTokenValidationResponse> ValidateFacebookToken(string accessToken)
    {
        try
        {
            string TokenValidationUrl = _facebookAuthConfig.TokenValidationUrl;
            var url = string.Format(TokenValidationUrl, accessToken, _facebookAuthConfig.AppId, _facebookAuthConfig.AppSecret);
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                var tokenValidationResponse = JsonConvert.DeserializeObject<FacebookTokenValidationResponse>(responseAsString);

                return tokenValidationResponse;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

        return null;
    }

    public async Task<FacebookUserInfoResponse> GetFacebookUserInformation(string accessToken)
    {
        try
        {
            string userInfoUrl = _facebookAuthConfig.UserInfoUrl;
            string url = string.Format(userInfoUrl, accessToken);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var userInfoResponse = JsonConvert.DeserializeObject<FacebookUserInfoResponse>(responseAsString);
                return userInfoResponse;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

        return null;

    }

}
