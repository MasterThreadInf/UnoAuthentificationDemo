using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Uno.Extensions;

namespace authen.WebSerices
{
    public class IdentityServerClient
    {
        private static HttpClient _client;
        private string _identityServerBaseAddress;
        private string _clientId;
        private string _clientSecret;
        private string _scope;


        static IdentityServerClient()
        {
            _client = new HttpClient();
        }

        public IdentityServerClient(string identityServerBaseAddress, string clientId, string clientSecret, string scope)
        {
            _identityServerBaseAddress = identityServerBaseAddress;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scope = scope;
        }
        public async Task<string> GetAccessTokenAsync()
        {
            var discoveryResponse = await _client.GetDiscoveryDocumentAsync(address: _identityServerBaseAddress);

            if (discoveryResponse.IsError)
            {
                this.Log().LogError(discoveryResponse.Error);
                throw new Exception(discoveryResponse.Error);
            }

            var tokenResponse = await _client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = _clientId,
                    ClientSecret = _clientSecret,
                    Scope = _scope
                });

            if (tokenResponse.IsError)
            {
                this.Log().LogError(tokenResponse.Error);
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }
    }
}
