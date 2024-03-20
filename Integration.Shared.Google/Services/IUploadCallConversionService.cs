using Integration.Shared.Google.Models;

namespace Integration.Shared.Google.Services
{
    public interface IUploadCallConversionService
    {
        Task<(bool Success, string Result, string Error)> UploadCallConversionAsync(string managerCustomerId, string orgRefreshToken, UploadCallConversionModel uploadCallConversionModel);
    }
}