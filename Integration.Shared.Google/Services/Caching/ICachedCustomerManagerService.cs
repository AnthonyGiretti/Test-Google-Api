namespace Integration.Shared.Google.Services.Caching;

public interface ICachedCustomerManagerService
{
    Task<string> GetCustomerManagerIdAsync(string conversionCustomerId, string orgRefreshToken);
}