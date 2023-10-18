namespace TheRide.API.Interfaces.Settings;

/// <summary>
/// Configuration for models store.
/// </summary>
public class ModelsStoreSettings
{
    /// <summary>
    /// The max size allowed for list models request.
    /// </summary>
    public int MaxModelsPerRetrieval { get; set; } = 1000;

    /// <summary>
    /// The time interval to wait before canceling the cancellation token source.
    /// </summary>
    public TimeSpan CancellationTokenDelay { get; set; } = TimeSpan.FromSeconds(30);
}