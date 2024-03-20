using Integration.Shared.Google.Models;

namespace Integration.Shared.Google.Services;

public class UploadClickConversionRequestService : IUploadClickConversionRequestService
{
    public AdsModels.UploadClickConversionsRequest Map(UploadClickConversionModel uploadClickConversionModel)
    {
        return new()
        {
            CustomerId = uploadClickConversionModel.CustomerId,
            Conversions = { uploadClickConversionModel.Conversions.Select(x => new AdsModels.ClickConversion
            {
                ConversionAction = x.ConversionAction,
                ConversionValue = x .ConversionValue,
                ConversionDateTime = x.ConversionDateTime ?? string.Empty,
                CurrencyCode = x.CurrencyCode,
                Gclid = x.Gclid ?? string.Empty,
                Wbraid = x.Wbraid ?? string.Empty,
                Gbraid = x.Gbraid ?? string.Empty
            })},
            PartialFailure = true,
            ValidateOnly = false
        };
    }
}
