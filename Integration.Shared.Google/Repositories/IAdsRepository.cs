using Integration.Shared.Domain.Models;

namespace Integration.Shared.Google.Repositories;

public interface IAdsRepository
{
    Task<string> GetCustomerManagerIdAsync(string clientCustomerId, string searchCustomerId, string orgRefreshToken);
    Task<long> GetConversionActionIdAsync(string customerId, string customerManagerId, string orgRefreshToken, string conversionActionName);
    Task<List<AdWordsClickPerformanceReportModel>> GetClickPerformanceReportAsync(string customerId, string customerManagerId, string refreshToken, DateTime reportDate);
    Task<List<AdWordsCallMetricsReportModel>> GetCallDetailReportAsync(string customerId, string customerManagerId, string refreshToken, DateTime reportDate);
}