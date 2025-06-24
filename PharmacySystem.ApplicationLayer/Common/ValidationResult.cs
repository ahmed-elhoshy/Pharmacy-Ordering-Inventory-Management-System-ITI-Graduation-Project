namespace PharmacySystem.ApplicationLayer.Common;
public class ValidationResult
{
    public Dictionary<string, List<string>> Errors { get; set; } = new();

    public bool HasErrors => Errors.Any();

    public string FirstErrorMessage => Errors.Values.SelectMany(v => v).FirstOrDefault() ?? "";

    public object ToErrorResponse() => new
    {
        message = FirstErrorMessage,
        success = false
    };
}