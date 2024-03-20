namespace Integration.Shared.Google.Services;

public interface ICustomersService
{
    Task<List<string>> GetAllCustomerIdsAsync(string orgRefreshToken);
}