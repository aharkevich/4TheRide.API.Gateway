namespace TheRide.API.Models;

/// <summary>
/// Represent error response model.
/// </summary>
public class ErrorModel
{
    /// <summary>
    /// A short, human-readable title for the general error type.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Error code from a predefined documented list of common, expected and handled errors.
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// A human-readable description of the specific error.
    /// </summary>
    public string? ErrorDetails { get; set; }
}