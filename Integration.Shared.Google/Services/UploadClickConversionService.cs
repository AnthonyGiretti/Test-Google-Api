using AzureIntegrationServices.Shared.Domain.Interfaces;
using Google.Ads.Gax.Lib;
using Integration.Shared.Google.Models;

namespace Integration.Shared.Google.Services;

public class UploadClickConversionService : IUploadClickConversionService
{
    private readonly IUploadClickConversionRequestService _uploadClickConversionRequestService;
    private readonly ISerializationService _serializationService;
    private readonly ClientManager _clientManager;

    public UploadClickConversionService(IUploadClickConversionRequestService uploadClickConversionRequestService,
                                        ISerializationService serializationService,
                                        ClientManager clientManager)
    {
        _uploadClickConversionRequestService = uploadClickConversionRequestService;
        _serializationService = serializationService;
        _clientManager = clientManager;
    }

    public async Task<(bool Success, string Result, string Error)> UploadClickConversionAsync(string managerCustomerId,
                                                                                              string orgRefreshToken,
                                                                                              UploadClickConversionModel uploadClickConversionModel)
    {
        var client = _clientManager.Create(managerCustomerId, orgRefreshToken);
        var uploadClickConversionRequest = _uploadClickConversionRequestService.Map(uploadClickConversionModel);
        var uploadServiceClient = client.GetService(GoogleServices.ConversionUploadService);
        var response = await uploadServiceClient.UploadClickConversionsAsync(uploadClickConversionRequest);
        var result = response.Results[0];
        var success = !result.IsEmpty();
        return (success, success ? _serializationService.Serialize(result) : string.Empty, response?.PartialFailureError?.Message ?? string.Empty);
    }
}
