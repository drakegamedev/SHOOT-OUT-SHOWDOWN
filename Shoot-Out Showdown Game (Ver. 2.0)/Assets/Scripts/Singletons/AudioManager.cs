using UnityEngine;

/// <summary>
/// Game Audio System, Manages Music and Sound Effects.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("References")]
    [SerializeField] private AudioData[] audioCollections;

    [Space]
    [SerializeField] private GameObject musicHolder;
    [SerializeField] private GameObject soundHolder;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // Initialize Audio Collections
            
        foreach (AudioData audioData in audioCollections)    
        {
            switch (audioData.Type)
            {
                case AudioData.AudioType.BGM:
                    audioData.Initialize(musicHolder);
                    break;

                case AudioData.AudioType.SFX:
                    audioData.Initialize(soundHolder);
                    break;
            };
        }
        
    }
    #endregion

    #region Audio Methods
    // Play Audio
    public void Play(string id)
    {
        // Find Audio in Audio Collections
        foreach (AudioData audioData in audioCollections)
        {
            // Audio Found
            if (id == audioData.Id)
            {
                audioData.Source.Play();
                return;
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }

    // Stop Audio
    public void Stop(string id)
    {
        // Find Audio in Audio Collections
        foreach (AudioData audioData in audioCollections)
        {
            // Audio Found
            if (id == audioData.Id)
            {
                audioData.Source.Stop();
                return;
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }

    // Modify Audio Pitch
    public void ModifyPitch(string id, float amount)
    {
        // Find Audio in Audio Collections
        foreach (AudioData audioData in audioCollections)
        {
            // Audio Found
            if (id == audioData.Id)
            {
                audioData.Source.pitch = amount;
                return;
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }
    #endregion
}
