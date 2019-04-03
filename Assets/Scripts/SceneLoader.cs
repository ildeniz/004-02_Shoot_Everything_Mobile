using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField] float delayInSeconds = 2f;

    public void LoadGameScene()
    {
        SceneManager.LoadScene("02a Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("03 Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
