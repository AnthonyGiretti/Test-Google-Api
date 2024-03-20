namespace Integration.Shared.Google.Services.Caching;

public interface ICachedCustomersService
{
    Task<List<string>> GetAllCustomerIdsAsync(string orgRefreshToken);
}