using Integration.Shared.Google.Repositories;
using Integration.Shared.Google.Services.Caching;
using Microsoft.Extensions.Logging;

namespace Integration.Shared.Google.Services;

public class CustomerManagerService : ICustomerManagerService
{
    private readonly ICachedCustomersService _cachedCustomersService;
    private readonly IAdsRepository _adsRepository;
    private readonly ILogger<CustomerManagerService> _logger;

    public CustomerManagerService(ICachedCustomersService cachedCustomersService, IAdsRepository adsRepository, ILogger<CustomerManagerService> logger)
    {
        _cachedCustomersService = cachedCustomersService;
        _adsRepository = adsRepository;
        _logger = logger;
    }

    public async Task<string> GetCustomerManagerIdAsync(string conversionCustomerId, string orgRefreshToken)
    {
        var customers = await _cachedCustomersService.GetAllCustomerIdsAsync(orgRefreshToken);

        foreach (var searchCustomerId in customers)
        {
            try
            {
                var managerId = await _adsRepository.GetCustomerManagerIdAsync(conversionCustomerId, searchCustomerId, orgRefreshToken);
                if (!string.IsNullOrEmpty(managerId))
                    return managerId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling _adsRepository.GetCustomerManagerIdAsync function");
            }
        }
        return string.Empty;
    }
}
