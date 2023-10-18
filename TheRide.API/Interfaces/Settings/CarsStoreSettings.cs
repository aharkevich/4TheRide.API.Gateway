namespace TheRide.API.Interfaces.Settings;

/// <summary>
/// Configuration for cars store.
/// </summary>
public class CarsStoreSettings
{
    /// <summary>
    /// The max size allowed for list cars request.
    /// </summary>
    public int MaxCarsPerRetrieval { get; set; } = 1000;

    /// <summary>
    /// The time interval to wait before canceling the cancellation token source.
    /// </summary>
    public TimeSpan CancellationTokenDelay { get; set; } = TimeSpan.FromSeconds(30);
}