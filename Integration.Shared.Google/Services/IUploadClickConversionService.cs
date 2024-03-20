using Integration.Shared.Google.Models;

namespace Integration.Shared.Google.Services;

public interface IUploadClickConversionService
{
    Task<(bool Success, string Result, string Error)> UploadClickConversionAsync(string managerCustomerId, string orgRefreshToken, UploadClickConversionModel uploadClickConversionModel);
}