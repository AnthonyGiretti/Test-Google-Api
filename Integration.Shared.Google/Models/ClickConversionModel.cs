namespace Integration.Shared.Google.Models;

public class ClickConversionModel
{
    public string ConversionAction { get; set; }
    public double ConversionValue { get; set; }
    public string ConversionDateTime { get; set; }
    public string CurrencyCode { get; set; }
    public string Wbraid { get; set; }
    public string Gbraid { get; set; }
    public string Gclid { get; set; }
}
