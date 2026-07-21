using UnityEngine;

public enum SFXType { Shoot, TakeDamage, Die, AstronautPickup, HealthPickup }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer Channels")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Sound Clips")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip takeDamageClip;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip astronautClip;
    [SerializeField] private AudioClip healthClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AdjustMusicVolume(float val) => musicSource.volume = val;
    public void AdjustSFXVolume(float val) => sfxSource.volume = val;

    public void PlaySFX(SFXType type)
    {
        AudioClip selectedTrack = type switch
        {
            SFXType.Shoot => shootClip,
            SFXType.TakeDamage => takeDamageClip,
            SFXType.Die => dieClip,
            SFXType.AstronautPickup => astronautClip,
            SFXType.HealthPickup => healthClip,
            _ => null
        };

        if (selectedTrack != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(selectedTrack);
        }
    }
}
