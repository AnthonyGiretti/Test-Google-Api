using AzureIntegrationServices.Shared.Domain.Interfaces;
using Google.Ads.Gax.Lib;
using Integration.Shared.Google.Models;

namespace Integration.Shared.Google.Services;

public class UploadCallConversionService : IUploadCallConversionService
{
    private readonly IUploadCallConversionRequestService _uploadCallConversionRequestService;
    private readonly ISerializationService _serializationService;
    private readonly ClientManager _clientManager;

    public UploadCallConversionService(IUploadCallConversionRequestService uploadCallConversionRequestService,
                                       ISerializationService serializationService,
                                       ClientManager clientManager)
    {
        _uploadCallConversionRequestService = uploadCallConversionRequestService;
        _serializationService = serializationService;
        _clientManager = clientManager;
    }

    public async Task<(bool Success, string Result, string Error)> UploadCallConversionAsync(string managerCustomerId,
                                                                                             string orgRefreshToken,
                                                                                             UploadCallConversionModel uploadCallConversionModel)
    {
        var client = _clientManager.Create(managerCustomerId, orgRefreshToken);
        var uploadCallConversionRequest = _uploadCallConversionRequestService.Map(uploadCallConversionModel);
        var uploadServiceClient = client.GetService(GoogleServices.ConversionUploadService);
        var response = await uploadServiceClient.UploadCallConversionsAsync(uploadCallConversionRequest);
        var result = response.Results[0];
        var success = !result.IsEmpty();
        return (success, success ? _serializationService.Serialize(result) : string.Empty, response?.PartialFailureError?.Message ?? string.Empty);
    }
}
