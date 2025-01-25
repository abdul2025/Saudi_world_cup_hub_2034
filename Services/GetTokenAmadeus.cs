using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaudiWorldCupHub.Services
{
    public class GetTokenAmadeus
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tokenUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";

        public GetTokenAmadeus(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            using (var client = new HttpClient())
            {
                var requestData = new Dictionary<string, string>
                {
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret },
                    { "grant_type", "client_credentials" }
                };

                var requestContent = new FormUrlEncodedContent(requestData);

                try
                {

                    
                    var response = await client.PostAsync(_tokenUrl, requestContent);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Optionally, parse the JSON response to extract the token
                    var tokenData = System.Text.Json.JsonDocument.Parse(responseContent);
                    var accessToken = tokenData.RootElement.GetProperty("access_token").GetString();

                    return accessToken;
                }
                catch (Exception ex)
                {
                    // Log the error or handle it as needed
                    throw new Exception("Failed to retrieve access token from Amadeus API", ex);
                }
            }
        }
    }
}
