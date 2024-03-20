namespace Integration.Shared.Google.Repositories;

public class CustomersRepository : ICustomersRepository
{
    private readonly ClientManager _googleAdsClientManager;

    public CustomersRepository(ClientManager googleAdsClientManager)
    {
        _googleAdsClientManager = googleAdsClientManager;
    }

    public async Task<List<string>> GetAllCustomerIdsAsync(string orgRefreshToken)
    {
        var googleClient = _googleAdsClientManager.Create(orgRefreshToken);

        using (var customerService = googleClient.GetService(GoogleServices.CustomerService))
        {
            var result = await customerService.ListAccessibleCustomersAsync(new AdsModels.ListAccessibleCustomersRequest());

            if (result != null && result.ResourceNames.Any())
                return result.ResourceNames.Select(x => Resources.CustomerName.Parse(x).CustomerId).ToList();
            return new List<string>();
        }
    }
}
