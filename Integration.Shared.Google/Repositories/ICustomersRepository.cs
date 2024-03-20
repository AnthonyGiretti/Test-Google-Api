namespace Integration.Shared.Google.Repositories;

public interface ICustomersRepository
{
    Task<List<string>> GetAllCustomerIdsAsync(string orgRefreshToken);
}