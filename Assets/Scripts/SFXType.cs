using UnityEngine;

public enum SFXType { Shoot, TakeDamage, Die, AstronautPickup, HealthPickup }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip damageClip;
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

    public void SetMusicVolume(float volume) => musicSource.volume = volume;
    public void SetSFXVolume(float volume) => sfxSource.volume = volume;

    public void PlaySFX(SFXType type)
    {
        AudioClip clip = type switch
        {
            SFXType.Shoot => shootClip,
            SFXType.TakeDamage => damageClip,
            SFXType.Die => dieClip,
            SFXType.AstronautPickup => astronautClip,
            SFXType.HealthPickup => healthClip,
            _ => null
        };

        if (clip != null) sfxSource.PlayOneShot(clip);
    }
}
