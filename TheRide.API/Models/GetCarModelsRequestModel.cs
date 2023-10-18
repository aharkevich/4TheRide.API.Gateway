namespace TheRide.API.Models;

/// <summary>
/// Get car models request model.
/// </summary>
public class GetCarModelsRequestModel : PaginationModel
{
    /// <summary>
    /// Car identifier.
    /// </summary>
    public string CarId { get; set; }
}