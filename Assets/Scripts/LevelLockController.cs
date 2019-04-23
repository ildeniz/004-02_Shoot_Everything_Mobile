using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLockController : MonoBehaviour
{
    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        int levelReached = PlayerPrefsController.GetLevelReached();

        for (int i = 1; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void RestoreProgress()
    {
        PlayerPrefsController.SetLevelReached(0);
        FindObjectOfType<SceneLoader>().LoadChooseLevelMenu();
    }
}
