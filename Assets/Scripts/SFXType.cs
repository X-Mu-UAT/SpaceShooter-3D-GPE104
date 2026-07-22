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
            return; // CRUCIAL FIX: Stops execution immediately for the duplicate object
        }
    }

    // Safety checks added below using null-conditional operators (?.)
    public void AdjustMusicVolume(float val) => musicSource?.gameObject.SetActive(true); // matching safety
    public void AdjustMusicVolumeDirect(float val) { if (musicSource != null) musicSource.volume = val; }
    public void AdjustSFXVolume(float val) { if (sfxSource != null) sfxSource.volume = val; }

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