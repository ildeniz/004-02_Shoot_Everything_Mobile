using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField] float delayInSeconds = 2f;
    private int currentSceneIndex;


    public void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        #region For Splash/Preload Screen
        /*if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitAndLoad());
        }*/
        #endregion
    }

    public void LoadGameScene(int levelIndex)
    {
        SceneManager.LoadScene("Level " + levelIndex);
        //SceneManager.LoadScene("Level 1");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("01a Main Menu");
    }

    public void LoadOptionstMenu()
    {
        SceneManager.LoadScene("01b Options Menu");
    }

    public void LoadChooseLevelMenu()
    {
        SceneManager.LoadScene("01c Choose Level");
    }
    

    public void RetryLevel()
    {
        FindObjectOfType<GameSession>().ResetGame();
        FindObjectOfType<LevelController>().SetGameCanvasTrue();
        FindObjectOfType<GameSession>().SetScore();
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f;
    }

    public void LevelComplete()
    {
        StartCoroutine(WaitAndLoadGameOver());
    }

    IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(delayInSeconds); //TODO wait till end of boss death animation
        SceneManager.LoadScene("Level Complete");
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
