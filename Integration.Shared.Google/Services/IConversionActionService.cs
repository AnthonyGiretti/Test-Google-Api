namespace Integration.Shared.Google.Services;

public interface IConversionActionService
{
    string ToConversionAction(string customerId, long conversionActionId);
}