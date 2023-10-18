using System.ComponentModel.DataAnnotations;

namespace TheRide.API.Models;

/// <summary>
/// Represents pagination model.
/// </summary>
public class PaginationModel
{
    /// <summary>
    /// A token that can be used to retrieve the next "page" of items.
    /// </summary>
    public string? PaginationToken { get; set; }

    /// <summary>
    /// Upper limit on the number of items to return per request.
    /// </summary>
    [Range(1, 1000)]
    public int Limit { get; set; } = 50;
}