using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    //[SerializeField] float delayInSeconds = 2f;
    //[SerializeField] float waitForDOT = 2f;

    private void Awake()
    {
        FindObjectOfType<LevelController>().BossSpawned();
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();

        if (levelController != null) // to get rid of "Null Reference Exception" error
        {
            levelController.BossKilled();
        }
    }
}
