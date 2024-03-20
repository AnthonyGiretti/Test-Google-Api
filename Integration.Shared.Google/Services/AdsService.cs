using Integration.Shared.Domain.Models;
using Integration.Shared.Google.Repositories;
using Microsoft.Extensions.Logging;

namespace Integration.Shared.Google.Services;

public class AdsService : IAdsService
{
    private readonly IAdsRepository _adsRepository;
    private readonly ILogger<CustomersService> _logger;

    public AdsService(ILogger<CustomersService> logger, IAdsRepository adsRepository)
    {
        _logger = logger;
        _adsRepository = adsRepository;
    }

    public async Task<long> GetConversionActionIdAsync(string conversionCustomerId, string customerManagerId, string orgRefreshToken, string conversionActionName)
    {
        try
        {
            var conversionId = await _adsRepository.GetConversionActionIdAsync(conversionCustomerId, customerManagerId, orgRefreshToken, conversionActionName);
            if (conversionId > 0)
                return conversionId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling _adsRepository.GetConversionActionIdAsync function with token {Token}", orgRefreshToken);
        }
        return 0;
    }

    public async Task<List<AdWordsClickPerformanceReportModel>> GetClickPerformanceReportAsync(string customerId, string managerCustomerId, string refreshToken, DateTime reportDate)
    {
        try
        {
            return await _adsRepository.GetClickPerformanceReportAsync(customerId, managerCustomerId, refreshToken, reportDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling _adsRepository.GetClickPerformanceReportAsync function with token {Token}", refreshToken);
        }
        return new List<AdWordsClickPerformanceReportModel>();
    }

    public async Task<List<AdWordsCallMetricsReportModel>> GetCallDetailReportAsync(string customerId, string customerManagerId, string refreshToken, DateTime reportDate)
    {
        try
        {
            return await _adsRepository.GetCallDetailReportAsync(customerId, customerManagerId, refreshToken, reportDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling _adsRepository.GetCallDetailReportAsync function with token {Token}", refreshToken);
        }
        return new List<AdWordsCallMetricsReportModel>();
    }
}
