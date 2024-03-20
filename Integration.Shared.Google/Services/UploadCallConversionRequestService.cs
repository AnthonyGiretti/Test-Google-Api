using Google.Ads.GoogleAds.V14.Resources;
using Integration.Shared.Google.Models;

namespace Integration.Shared.Google.Services;

public class UploadCallConversionRequestService : IUploadCallConversionRequestService
{
    private readonly IDateTimeConverterService _dateTimeConverterService;

    public UploadCallConversionRequestService(IDateTimeConverterService dateTimeConverterService)
    {
        _dateTimeConverterService = dateTimeConverterService;
    }

    public AdsModels.UploadCallConversionsRequest Map(UploadCallConversionModel uploadCallConversionModel)
    {
        return new()
        {
            CustomerId = uploadCallConversionModel.CustomerId,
            Conversions = { new AdsModels.CallConversion
            {
                ConversionAction = ResourceNames.ConversionAction(long.Parse(uploadCallConversionModel.CustomerId), uploadCallConversionModel.ConversionActionId),
                CallerId = uploadCallConversionModel.CallerId,
                CallStartDateTime = _dateTimeConverterService.Convert(uploadCallConversionModel.CallStartTime),
                ConversionDateTime = _dateTimeConverterService.Convert(uploadCallConversionModel.ConversionTime),
                ConversionValue = uploadCallConversionModel.ConversionValue,
                CurrencyCode = uploadCallConversionModel.ConversionCurrencyCode
            }},
            PartialFailure = true,
            ValidateOnly = false
        };
    }
}
