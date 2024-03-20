using Google.Ads.Gax.Config;
using Google.Ads.GoogleAds.Config;
using Google.Ads.GoogleAds.Lib;
using Integration.Shared.Google.Options;

namespace Integration.Shared.Google.Clients;

public class ClientManager
{
    private readonly GoogleOAuthOptions _options;

    public ClientManager(GoogleOAuthOptions options)
    {
        _options = options;
    }

    public GoogleAdsClient Create(string loginCustomerId, string orgRefreshToken)
    {
        return new GoogleAdsClient(new GoogleAdsConfig
        {
            LoginCustomerId = loginCustomerId,
            OAuth2RefreshToken = orgRefreshToken,
            OAuth2ClientId = _options.OAuth2ClientId,
            OAuth2ClientSecret = _options.OAuth2ClientSecret,
            DeveloperToken = _options.DeveloperToken,
            OAuth2Mode = OAuth2Flow.APPLICATION
        });
    }

    public GoogleAdsClient Create(string orgRefreshToken)
    {
        return new GoogleAdsClient(new GoogleAdsConfig
        {
            LoginCustomerId = string.Empty,
            OAuth2RefreshToken = orgRefreshToken,
            OAuth2ClientId = _options.OAuth2ClientId,
            OAuth2ClientSecret = _options.OAuth2ClientSecret,
            DeveloperToken = _options.DeveloperToken,
            OAuth2Mode = OAuth2Flow.APPLICATION
        });
    }
}
