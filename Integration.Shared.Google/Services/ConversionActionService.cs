namespace Integration.Shared.Google.Services;

public class ConversionActionService : IConversionActionService
{
    public string ToConversionAction(string customerId, long conversionActionId)
    {
        return Resources.ResourceNames.ConversionAction(long.Parse(customerId), conversionActionId);
    }
}
