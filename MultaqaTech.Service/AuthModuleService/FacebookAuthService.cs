namespace MultaqaTech.Service.AuthModuleService;

public class FacebookAuthService : IFacebookAuthService
{

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public FacebookAuthService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("Facebook");
        _configuration = configuration;
    }


    public async Task<FacebookTokenValidationResponse> ValidateFacebookToken(string accessToken)
    {
        var appId = _configuration["Facebook:AppId"];
        var secretKey = _configuration["Facebook:AppSecret"];
        var url = _configuration["Facebook:TokenValidationUrl"];
        try
        {


            url = string.Format(url??string.Empty, accessToken, appId, secretKey);


            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                var tokenValidationResponse = JsonConvert.DeserializeObject<FacebookTokenValidationResponse>(responseAsString);

                return tokenValidationResponse ?? new FacebookTokenValidationResponse();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new FacebookTokenValidationResponse();
        }

        return new FacebookTokenValidationResponse();
    }


    public async Task<FacebookUserInfoResponse> GetFacebookUserInformation(string accessToken)
    {
        try
        {
            var userInfoUrl = _configuration["Facebook:UserInfoUrl"];

            string url = string.Format(userInfoUrl ?? string.Empty, accessToken);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var userInfoResponse = JsonConvert.DeserializeObject<FacebookUserInfoResponse>(responseAsString);
                return userInfoResponse ?? new FacebookUserInfoResponse();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new FacebookUserInfoResponse();
        }

        return new FacebookUserInfoResponse();

    }

}
