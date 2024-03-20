namespace Integration.Shared.Google.Models;

public class UploadCallConversionModel
{
    public string CustomerId { get; set; }
    public long ConversionActionId { get; set; }
    public string CallerId { get; set; }
    public string CallStartTime { get; set; }
    public string ConversionCurrencyCode { get; set; }
    public string ConversionTime { get; set; }
    public double ConversionValue { get; set; }
    public string ConversionName { get; set; }
}