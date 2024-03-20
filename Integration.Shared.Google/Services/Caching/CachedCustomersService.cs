using Integration.Shared.Domain.Interfaces;

namespace Integration.Shared.Google.Services.Caching;

public class CachedCustomersService : ICachedCustomersService
{
    private readonly ICustomersService _customersService;
    private readonly ICacheService _cacheService;

    public CachedCustomersService(ICustomersService customersService, ICacheService cacheService)
    {
        _customersService = customersService;
        _cacheService = cacheService;
    }

    public async Task<List<string>> GetAllCustomerIdsAsync(string orgRefreshToken)
    {
        var cacheKey = "AccessibleCustomers";

        var accessibleCustomers = await _cacheService.GetAsync<List<string>>(cacheKey);
        if (accessibleCustomers == null || !accessibleCustomers.Any())
        {
            accessibleCustomers = await _customersService.GetAllCustomerIdsAsync(orgRefreshToken);

            if (accessibleCustomers is not null && accessibleCustomers.Any())
                await _cacheService.UpsertAsync(cacheKey, accessibleCustomers);
        }
        return accessibleCustomers;
    }
}
