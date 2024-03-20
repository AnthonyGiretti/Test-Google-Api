namespace Integration.Shared.Google.Services.Caching
{
    public interface ICachedAdsService
    {
        Task<long> GetConversionActionIdAsync(string customerId, string customerManagerId, string orgRefreshToken, string conversionActionName);
    }
}