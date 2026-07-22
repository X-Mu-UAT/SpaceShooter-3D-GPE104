using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderLink : MonoBehaviour
{
    private enum VolumeType { Music, SFX }

    [Header("Slider Setup")]
    [SerializeField] private VolumeType targetVolumeChannel;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (slider == null) return;

        // Check if the AudioManager exists in the current scene context
        if (AudioManager.Instance != null)
        {
            // 1. Initialize the slider position to match the current volume
            // Note: If you want to pull current values, you can add Get functions to your AudioManager.
            // For now, we will default them to full volume (1.0f).
            slider.value = 1f;

            // 2. Add a dynamic listener via code instead of using the Inspector drag-and-drop window
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        else
        {
            Debug.LogWarning("[VolumeSliderLink] AudioManager instance not found in this scene yet.");
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (AudioManager.Instance == null) return;

        // Route the slider data directly to the active Singleton Instance
        if (targetVolumeChannel == VolumeType.Music)
        {
            AudioManager.Instance.AdjustMusicVolumeDirect(value);
        }
        else if (targetVolumeChannel == VolumeType.SFX)
        {
            AudioManager.Instance.AdjustSFXVolume(value);
        }
    }

    private void OnDestroy()
    {
        // Clean up the code listener if the slider panel is destroyed or turned off
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}