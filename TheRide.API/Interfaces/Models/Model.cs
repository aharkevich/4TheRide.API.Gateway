namespace TheRide.API.Interfaces.Models;

/// <summary>
/// Represents a model of the car.
/// </summary>
public class Model
{
    /// <summary>
    /// Model identifier.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Car identifier.
    /// </summary>
    public string CarId { get; set; }
    
    /// <summary>
    /// The name of the model.
    /// </summary>
    public string Name { get; set; }
}