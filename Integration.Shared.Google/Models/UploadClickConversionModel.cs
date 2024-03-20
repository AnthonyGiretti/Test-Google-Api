namespace Integration.Shared.Google.Models;

public class UploadClickConversionModel
{
    public string CustomerId { get; set; }
    public IList<ClickConversionModel> Conversions { get; set; } = new List<ClickConversionModel>();
    public bool PartialFailure { get; set; }
    public bool ValidateOnly { get; set; }
}
