using UnityEngine;

// Game Audio System
// Manages Music and Sound Effects
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Collects AudioData
    [System.Serializable]
    struct AudioDataCollection
    {
        public AudioData[] Audios;
    }

    [Header("Properties")]
    [SerializeField] private AudioDataCollection[] audioCollections;                    // Audio Collection Array

    [Header("References")]
    [SerializeField] private GameObject musicHolder;                                    // Music Holder
    [SerializeField] private GameObject soundHolder;                                    // SFX Holder

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
        foreach (AudioDataCollection collectionData in audioCollections)
        {
            foreach (AudioData audioData in collectionData.Audios)
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
    }
    #endregion

    #region Audio Methods
    // Play Audio
    public void Play(string id)
    {
        // Find Audio in Audio Collections
        foreach (AudioDataCollection collectionData in audioCollections)
        {
            // Check within AudioData Types
            foreach (AudioData audioData in collectionData.Audios)
            {
                // Audio Found
                if (id == audioData.Id)
                {
                    audioData.Source.Play();
                    return;
                }
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }

    // Play Audio
    public void PlayOneShot(string id)
    {
        // Find Audio in Audio Collections
        foreach (AudioDataCollection collectionData in audioCollections)
        {
            // Check within AudioData Types
            foreach (AudioData audioData in collectionData.Audios)
            {
                // Audio Found
                if (id == audioData.Id)
                {
                    audioData.Source.PlayOneShot(audioData.Source.clip);
                    return;
                }
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
        foreach (AudioDataCollection collectionData in audioCollections)
        {
            // Check within AudioData Types
            foreach (AudioData audioData in collectionData.Audios)
            {
                // Audio Found
                if (id == audioData.Id)
                {
                    audioData.Source.Stop();
                    return;
                }
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
        foreach (AudioDataCollection collectionData in audioCollections)
        {
            // Check within AudioData Types
            foreach (AudioData audioData in collectionData.Audios)
            {
                // Audio Found
                if (id == audioData.Id)
                {
                    audioData.Source.pitch = amount;
                    return;
                }
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }
    #endregion
}