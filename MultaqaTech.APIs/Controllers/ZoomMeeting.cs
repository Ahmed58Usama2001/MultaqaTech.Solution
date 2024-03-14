using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MultaqaTech.APIs.Dtos.ZoomDtos;

namespace MultaqaTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomMeetingController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public ZoomMeetingController(IHttpClientFactory httpClientFactory, string apiKey, string apiSecret)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }

        [HttpGet("{meetingId}")]
        public async Task<IActionResult> GetMeeting(string meetingId)
        {
            try
            {
                var accessToken = await GetAccessToken();
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri("https://api.zoom.us/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await httpClient.GetAsync($"meetings/{meetingId}");

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Failed to retrieve meeting details.");
                }

                var content = await response.Content.ReadAsStringAsync();
                var meeting = JsonConvert.DeserializeObject(content);

                return Ok(meeting);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error retrieving meeting details: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeeting(CreateMeetingRequestDto request)
        {
            try
            {
                var accessToken = await GetAccessToken();
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri("https://api.zoom.us/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("users/me/meetings", data);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Failed to create meeting.");
                }

                var content = await response.Content.ReadAsStringAsync();
                var createdMeeting = JsonConvert.DeserializeObject(content);

                return Ok(createdMeeting);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error creating meeting: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        private async Task<string> GetAccessToken()
        {
            
            string apiKey = "yJBYbJAERgmlZlOssQFepQ";
            string apiSecret = "zoGp4HTahEpTH2s7LGKnKiDOEruMd1tF";

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSecret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("iss", apiKey),
            new Claim("exp", DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds().ToString())
        }),
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent($"grant_type=client_credentials&assertion={tokenString}", Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync("https://zoom.us/oauth/token", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to retrieve access token from Zoom API.");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponseDto>(responseContent);

                return tokenResponse.access_token;
            }
        }


        
    }
}
