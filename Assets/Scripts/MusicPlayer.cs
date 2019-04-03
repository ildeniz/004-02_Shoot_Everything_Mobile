using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        //if (FindObjectsOfType<MusicPlayer>().Length > 1) -> we use <> to get a particular type, where we specify it.
        //if (FindObjectsOfType<AudioSource>().Length > 1)
        if (FindObjectsOfType(GetType()).Length > 1)
        /*  Why GetType()? GetType() is a bit more re-useable than <> 
            b/c when we use this method of GetType it's getting the the type that 
            "this"class is, which is "MusicPlayer" in this case. */
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
