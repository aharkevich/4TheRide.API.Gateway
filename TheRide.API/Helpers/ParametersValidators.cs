namespace TheRide.API.Helpers;

/// <summary>
/// Validators for input parameters.
/// </summary>
internal static class ParametersValidators
{
    internal static void ValidateNotNullOrWhitespaceParameter(string parameterName, string parameterValue)
    {
        if (string.IsNullOrWhiteSpace(parameterValue))
        {
            throw new ArgumentException($"'{parameterName}' cannot be null or whitespace", parameterName);
        }
    }
    
    internal static bool IsKnownArgumentException(string? paramName) =>
        paramName == Constants.CarId;
}