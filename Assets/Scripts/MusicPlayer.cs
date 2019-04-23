using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    #region If you want to use a seperate "Splash/PreLoad screen" then use this approach
    /*
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    */
    #endregion

    // Singleton approach:

    private void Awake()
    {
        SetUpSingleton();
    }

     void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
