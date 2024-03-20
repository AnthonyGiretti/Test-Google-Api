using Integration.Shared.Domain.Models;

namespace Integration.Shared.Google.Services;

public interface IAdsService
{
    Task<long> GetConversionActionIdAsync(string conversionCustomerId, string customerManagerId, string orgRefreshToken, string conversionActionName);
    Task<List<AdWordsClickPerformanceReportModel>> GetClickPerformanceReportAsync(string customerId, string managerCustomerId, string refreshToken, DateTime reportDate);
    Task<List<AdWordsCallMetricsReportModel>> GetCallDetailReportAsync(string customerId, string customerManagerId, string refreshToken, DateTime reportDate);
}