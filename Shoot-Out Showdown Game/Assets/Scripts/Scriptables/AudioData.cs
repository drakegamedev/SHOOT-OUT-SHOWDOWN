using UnityEngine;

[CreateAssetMenu(fileName = "New Audio", menuName = "Audio")]
public class AudioData : ScriptableObject
{
    // Indicates Type of Audio
    public enum AudioType
    {
        BGM,
        SFX
    };

    public string Id;                                                   // Audio ID
    public AudioClip Clip;                                              // Audio Clip
    public AudioType Type;                                              // Audio Type Indicator

    [Range(0, 256)] public int Priority;                                // Priority Level
    [Range(0f, 1f)] public float Volume;                                // Volume Amount
    [Range(0f, 3f)] public float Pitch;                                 // Pitch Value
    public bool PlayOnAwake;                                            // Checks if Audio Will be Playerd Upon Activation
    public bool Loop;                                                   // Checks if Audio Will be Looped
    [Range(0f, 3f)] public float SpatialBlend;                          // Spatial Blend Value
    public AudioSource Source { get; private set; }                     // Audio Source Component

    public bool IsInitialized => Source;                                // Indicates if Audio has been Initialized

    /// <summary>
    /// Initialize Audio Data Properties
    /// </summary>
    /// <param name="audioSourceContainer"></param>
    public void Initialize(GameObject audioSourceContainer)
    {
        Source = audioSourceContainer.AddComponent<AudioSource>();
        Source.clip = Clip;
        Source.priority = Priority;
        Source.volume = Volume;
        Source.pitch = Pitch;
        Source.playOnAwake = PlayOnAwake;
        Source.loop = Loop;
        Source.spatialBlend = SpatialBlend;
    }
}