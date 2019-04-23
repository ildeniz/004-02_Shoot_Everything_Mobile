using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //[SerializeField] GameObject winLabel;
    [SerializeField] GameObject failLabel;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] float delayInSeconds = 2f;
    [SerializeField] int currentLevelIndex;


    Player player;
    int numberOfBosses = 0;

    // Start is called before the first frame update
    void Start()
    {
        //winLabel.SetActive(false);
        failLabel.SetActive(false);
        player = FindObjectOfType<Player>();
    }

    public void BossSpawned()
    {
        numberOfBosses++;
    }

    public void BossKilled()
    {
        numberOfBosses--;
        if (numberOfBosses <= 0 && player.GetHealth() > 0)
        {
            Debug.Log("LEVEL " + currentLevelIndex + " BOSS DOWN AND DEAD!");
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            sceneLoader.LevelComplete();
            PlayerPrefsController.SetLevelReached(currentLevelIndex + 1);

            //winLabel.SetActive(true); //TODO some conguratulation message maybe?

            //TODO play some win music
        }
    }

    public void PlayerDied()
    {
        failLabel.SetActive(true);
        gameCanvas.SetActive(false);
        Time.timeScale = 0f;
    }

    public void SetGameCanvasTrue()
    {
        gameCanvas.SetActive(true);
    }
}
