using ApplicationService.BLL.Interfaces;
using System.Net.Http.Headers;

namespace ApplicationService.Handlers
{
    public class AuthTokenHandler(ITokenProvider tokenProvider, string downstreamName) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await tokenProvider.GetAccessTokenAsync(downstreamName, cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
