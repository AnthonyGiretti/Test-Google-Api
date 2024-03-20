using Integration.Shared.Domain.Interfaces;

namespace Integration.Shared.Google.Services.Caching;

public class CachedAdsService : ICachedAdsService
{
    private readonly IAdsService _adsService;
    private readonly ICacheService _cacheService;

    public CachedAdsService(IAdsService adsService,
                            ICacheService cacheService)
    {
        _adsService = adsService;
        _cacheService = cacheService;
    }

    public async Task<long> GetConversionActionIdAsync(string customerId, string customerManagerId, string orgRefreshToken, string conversionActionName)
    {
        var cacheKey = $"ConversionActionId-{customerManagerId}-{customerId}-{conversionActionName}";

        var conversionId = await _cacheService.GetAsync<long?>(cacheKey);
        if (conversionId is null)
        {
            conversionId = await _adsService.GetConversionActionIdAsync(customerId, customerManagerId, orgRefreshToken, conversionActionName);
            
            if (conversionId > 0)
                await _cacheService.UpsertAsync(cacheKey, conversionId);
        }
        return conversionId.Value;
    }
}
