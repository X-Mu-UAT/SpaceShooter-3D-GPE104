using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UfoProximitySound : MonoBehaviour
{
    [Header("UFO Sound Settings")]
    [Tooltip("The hum or engine sound clip for the UFO.")]
    [SerializeField] private AudioClip ufoEngineClip;

    [Tooltip("The maximum distance (in Unity units) where you can still hear the UFO.")]
    [SerializeField] private float maxHearingDistance = 50f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Configure the audio source fields programmatically for 3D Sound
        if (ufoEngineClip != null && audioSource != null)
        {
            audioSource.clip = ufoEngineClip;
            audioSource.loop = true;          // Loop the engine sound continuously
            audioSource.spatialBlend = 1.0f;  // CRITICAL: 1.0f means 100% 3D spatial audio

            // Set up the distance falloff engine properties
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.minDistance = 2f;     // Maximum volume distance threshold
            audioSource.maxDistance = maxHearingDistance;

            // Play the loop immediately
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"[UfoProximitySound] Missing engine clip on {gameObject.name}");
        }
    }
}
