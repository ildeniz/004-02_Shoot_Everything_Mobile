using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    [SerializeField] float defaultMusicVolume = 0.8f;
    //[SerializeField] float defaultSFXVolume = 0.8f;

    // Use this for initialization
    void Start()
    {
        musicVolumeSlider.value = PlayerPrefsController.GetMasterVolume();
        sfxVolumeSlider.value = PlayerPrefsController.GetSFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(musicVolumeSlider.value);
        }
        /*else
        {
            Debug.LogWarning("No music player found... did you start from splash screen?");
        }*/
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(musicVolumeSlider.value);
        PlayerPrefsController.SetSFXVolume(sfxVolumeSlider.value);
        FindObjectOfType<SceneLoader>().LoadMainMenu();
    }

    public void SetDefaults()
    {
        musicVolumeSlider.value = defaultMusicVolume;
    }
}
