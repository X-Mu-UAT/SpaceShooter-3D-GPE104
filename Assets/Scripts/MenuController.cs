using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject pressEnterScreen;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject audioSettingsScreen;

    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private bool gameStarted = false;

    private void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            gameStarted = true;
            pressEnterScreen.SetActive(false);
            mainMenuScreen.SetActive(true);
        }
    }

    public void OpenAudioSettings()
    {
        mainMenuScreen.SetActive(false);
        audioSettingsScreen.SetActive(true);
    }

    public void CloseAudioSettings()
    {
        audioSettingsScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void OnMusicVolumeChanged()
    {
        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }

    public void OnSFXVolumeChanged()
    {
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1); // Ensure your level scene index is 1 in Build Settings
    }

    public void QuitToDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
