namespace Integration.Shared.Google.Services;

public interface ICustomerManagerService
{
    Task<string> GetCustomerManagerIdAsync(string conversionCustomerId, string orgRefreshToken);
}