using Integration.Shared.Domain.Interfaces;

namespace Integration.Shared.Google.Services.Caching;

public class CachedCustomerManagerService : ICachedCustomerManagerService
{
    private readonly ICustomerManagerService _customerManagerService;
    private readonly ICacheService _cacheService;

    public CachedCustomerManagerService(ICustomerManagerService customerManagerService, ICacheService cacheService)
    {
        _customerManagerService = customerManagerService;
        _cacheService = cacheService;
    }

    public async Task<string> GetCustomerManagerIdAsync(string conversionCustomerId, string orgRefreshToken)
    {
        var cacheKey = $"CustomerManagerId-{conversionCustomerId}";

        var customerManagerId = await _cacheService.GetAsync<string>(cacheKey);
        if (string.IsNullOrEmpty(customerManagerId))
        {
            customerManagerId = await _customerManagerService.GetCustomerManagerIdAsync(conversionCustomerId, orgRefreshToken);

            if (!string.IsNullOrEmpty(customerManagerId))
                await _cacheService.UpsertAsync(cacheKey, customerManagerId);
        }
        return customerManagerId;
    }
}
