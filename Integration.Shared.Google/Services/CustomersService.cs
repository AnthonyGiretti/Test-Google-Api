using Integration.Shared.Google.Repositories;
using Microsoft.Extensions.Logging;

namespace Integration.Shared.Google.Services;

public class CustomersService : ICustomersService
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ILogger<CustomersService> _logger;

    public CustomersService(ICustomersRepository customersRepository,
                            ILogger<CustomersService> logger)
    {
        _customersRepository = customersRepository;
        _logger = logger;
    }


    public async Task<List<string>> GetAllCustomerIdsAsync(string orgRefreshToken)
    {
        try
        {
            return await _customersRepository.GetAllCustomerIdsAsync(orgRefreshToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling _customersRepository.GetAllCustomerIdsAsync function");
            return new List<string>();
        }
    }
}
