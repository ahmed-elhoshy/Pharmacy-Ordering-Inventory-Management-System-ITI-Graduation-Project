namespace PharmacySystem.ApplicationLayer.Common;
public class ValidationResult
{
    public Dictionary<string, string[]> Errors { get; set; } = [];
    public bool HasErrors => Errors?.Any() == true;
}