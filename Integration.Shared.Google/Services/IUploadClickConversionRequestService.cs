using Google.Ads.GoogleAds.V14.Services;
using Integration.Shared.Google.Models;

namespace Integration.Shared.Google.Services;

public interface IUploadClickConversionRequestService
{
    UploadClickConversionsRequest Map(UploadClickConversionModel uploadClickConversionModel);
}