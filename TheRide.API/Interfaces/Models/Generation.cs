namespace TheRide.API.Interfaces.Models;

/// <summary>
/// Represent car model generation.
/// </summary>
public class Generation
{
    /// <summary>
    /// Name of the generation.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// First production year.
    /// </summary>
    public int Year { get; set; }
}